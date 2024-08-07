using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.API.data;
using Library.API.models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Library.API.dtos;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using LibraryAPI.dtos;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LibraryDbContext _context;
        private readonly IConfiguration _configuration;

        public UsersController(LibraryDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: api/Users
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> Getusers()
        {
          if (_context.users == null)
          {
              return NotFound();
          }
            return await _context.users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {
          if (_context.users == null)
          {
              return NotFound();
          }
            var user = await _context.users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDto()
            {
                nickname = user.nickname,
                email = user.email,
                isAdmin = user.is_admin
            };

            return userDto;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.user_id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.users == null)
          {
              return Problem("Entity set 'LibraryDbContext.users'  is null.");
          }
            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.user_id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (_context.users == null)
            {
                return NotFound();
            }
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return (_context.users?.Any(e => e.user_id == id)).GetValueOrDefault();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto userDto)
        {

            var existingUser = await _context.users.FirstOrDefaultAsync(u => u.email == userDto.email);
            if (existingUser != null)
            {
                return Conflict("User with this email already exists");
            }

            var passwordSalt = GenerateSalt();
            var passwordHash = GenerateHash(userDto.password, passwordSalt);

            User user = new User
            {
                user_id = Guid.NewGuid(),
                nickname = userDto.nickname,
                email = userDto.email,
                is_admin = false,
                password = new Password
                {
                    salt = passwordSalt,
                    hash = passwordHash
                }
            };


            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto userDto)
        {
            var user = await _context.users.Include(u => u.password).FirstOrDefaultAsync(u => u.email == userDto.Email);
            if (user == null)
            {
                return Unauthorized("Błędny email");
            }

            if (!VerifyPasswordHash(userDto.Password, user.password.hash, user.password.salt))
            {
                return Unauthorized("Błędne Password");
            }
            bool isAdmin = user.is_admin;

            var token = GenerateJwtToken(user.user_id.ToString(), isAdmin);
            return Ok(new { Token = token });
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private string GenerateHash(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPasswordHash(string password, string hash, string salt)
        {
            var hashedPassword = GenerateHash(password, salt);
            return hashedPassword == hash;
        }

        private string GenerateJwtToken(string userId, bool isAdmin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);
            var audience = _configuration["JwtSettings:Audience"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var expiryHours = Convert.ToDouble(_configuration["JwtSettings:ExpiryHours"]);
            var expiration = DateTime.UtcNow.AddHours(expiryHours);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User")
        }),
                Expires = expiration,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        [HttpPut("{id}/changePassword")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> ChangePassword(Guid id, [FromBody] ChangePasswordDto model)
        {
            var user = await _context.users.Include(u => u.password).FirstOrDefaultAsync(u => u.user_id == id);
            if (user == null)
            {
                return NotFound();
            }

            if (!VerifyPasswordHash(model.password, user.password.hash, user.password.salt))
            {
                return Unauthorized("Błędne hasło");
            }

            var newSalt = GenerateSalt();
            var newHash = GenerateHash(model.newPassword, newSalt);

            user.password.salt = newSalt;
            user.password.hash = newHash;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}/deleteAccount")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteAccount(Guid id, [FromBody] DeleteAccountDto model)
        {
            var user = await _context.users.Include(u => u.password).FirstOrDefaultAsync(u => u.user_id == id);
            if (user == null)
            {
                return NotFound("Nie znaleziono użytkownika");
            }

            var loggedUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (loggedUserId != id.ToString())
            {
                return Unauthorized("Nie masz uprawnień do usunięcia tego konta");
            }

            if (!VerifyPasswordHash(model.password, user.password.hash, user.password.salt))
            {
                return Unauthorized("Błędne hasło");
            }

            _context.users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
