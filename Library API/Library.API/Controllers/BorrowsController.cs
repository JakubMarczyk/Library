using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.API.data;
using Library.API.models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Library.API.dtos;
using LibraryAPI.dtos;
using Library.API.dto;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowsController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BorrowsController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/Borrows
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Borrow>>> Getborrows()
        {
          if (_context.borrows == null)
          {
              return NotFound();
          }
            return await _context.borrows.ToListAsync();
        }


        // GET: api/Borrowed
        [HttpGet("User/Borrowed/{user_id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<IEnumerable<BorrowedBookDto>>> GetUserBorrowed(Guid user_id)
        {
            if (_context.borrows == null)
            {
                return NotFound();
            }

            var userBorrowedBooks = await _context.borrows
                .Where(b => b.user_id_fk == user_id && b.returnedTime == null)
                .Select(b => new BorrowedBookDto
                {
                    borrow_id = b.borrow_id,
                    borrowTime = b.borrowTime,
                    returnTime = b.returnTime,
                    extended = b.extended,
                    book_instance_id_fk = b.book_instance_id_fk,
                    book_id_fk = b.book_instance.book_id_fk,
                    title = b.book_instance.book.title,
                    isbn = b.book_instance.book.isbn,
                    publisher = b.book_instance.book.publisher,
                    yearOfPublication = b.book_instance.book.yearOfPublication,
                    cover = b.book_instance.book.cover,
                    description = b.book_instance.book.description,
                    pages = b.book_instance.book.pages,
                    authors = b.book_instance.book.authors
                                    .Select(ba => new AuthorDto
                                    {
                                        author_id = ba.author_id,
                                        firstName = ba.firstName,
                                        lastName = ba.lastName
                                    })
                                    .ToList(),
                    categories = b.book_instance.book.categories
                                       .Select(bc => new BookCategoryDto
                                       {
                                           category_id = bc.category_id,
                                           name = bc.name
                                       })
                                       .ToList()
                })
                .ToListAsync();

            return userBorrowedBooks;
        }

        // GET: api/Borrowed
        [HttpGet("User/Borrowed/Book/{borrow_id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<IEnumerable<BorrowedBookDto>>> GetUserBorrowedBook(Guid borrow_id)
        {
            if (_context.borrows == null)
            {
                return NotFound();
            }

            var userBorrowedBook = await _context.borrows
                .Where(b => b.borrow_id == borrow_id)
                .Select(b => new BorrowedBookDto
                {
                    borrow_id = b.borrow_id,
                    borrowTime = b.borrowTime,
                    returnTime = b.returnTime,
                    extended = b.extended,
                    book_instance_id_fk = b.book_instance_id_fk,
                    book_id_fk = b.book_instance.book_id_fk,
                    title = b.book_instance.book.title,
                    isbn = b.book_instance.book.isbn,
                    publisher = b.book_instance.book.publisher,
                    yearOfPublication = b.book_instance.book.yearOfPublication,
                    cover = b.book_instance.book.cover,
                    description = b.book_instance.book.description,
                    pages = b.book_instance.book.pages,
                    authors = b.book_instance.book.authors
                                    .Select(ba => new AuthorDto
                                    {
                                        author_id = ba.author_id,
                                        firstName = ba.firstName,
                                        lastName = ba.lastName
                                    })
                                    .ToList(),
                    categories = b.book_instance.book.categories
                                       .Select(bc => new BookCategoryDto
                                       {
                                           category_id = bc.category_id,
                                           name = bc.name
                                       }).ToList()
                }).FirstOrDefaultAsync();

            if (userBorrowedBook == null)
            {
                return NotFound();
            }
            return Ok(userBorrowedBook);
        }

        [HttpGet("User/Borrows/{user_id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<IEnumerable<Borrow>>> GetUserBorrows(Guid user_id)
        {
            var userBorrows = await _context.borrows
            .Where(b => b.user_id_fk == user_id)
            .ToListAsync();

            return userBorrows;
        }

        // GET: api/Borrows/5
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Borrow>> GetBorrow(Guid id)
        {
          if (_context.borrows == null)
          {
              return NotFound();
          }
            var borrow = await _context.borrows.FindAsync(id);

            if (borrow == null)
            {
                return NotFound();
            }

            return borrow;
        }

        // PUT: api/Borrows/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutBorrow(Guid id, Borrow borrow)
        {
            if (id != borrow.borrow_id)
            {
                return BadRequest();
            }

            _context.Entry(borrow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowExists(id))
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


        // DELETE: api/Borrows/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBorrow(Guid id)
        {
            if (_context.borrows == null)
            {
                return NotFound();
            }
            var borrow = await _context.borrows.FindAsync(id);
            if (borrow == null)
            {
                return NotFound();
            }

            _context.borrows.Remove(borrow);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BorrowExists(Guid id)
        {
            return (_context.borrows?.Any(e => e.borrow_id == id)).GetValueOrDefault();
        }

        [HttpPost("BorrowBook")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<Borrow>> BorrowBook(BorrowDto borrowDto)
        {
            try
            {
                var bookInstance = await _context.book_instances
                    .Include(bi => bi.book) 
                    .FirstOrDefaultAsync(bi => bi.book_instance_id == borrowDto.book_instance_id);

                if (bookInstance == null || bookInstance.status_id_fk != 1)
                {
                    return BadRequest("Książka jest niedostępna do wypożyczenia.");
                }

                var user = await _context.users.FindAsync(borrowDto.user_id);
                if (user == null)
                {
                    return BadRequest("Użytkownik nie istnieje.");
                }

                Borrow borrow = new Borrow
                {
                    user_id_fk = borrowDto.user_id,
                    book_instance_id_fk = borrowDto.book_instance_id,
                    borrowTime = DateTimeOffset.UtcNow,
                    returnTime = DateTimeOffset.UtcNow.AddDays(14),
                    extended = false
                };
                bookInstance.status_id_fk = 2;

                var status = await _context.statuses.FindAsync(bookInstance.status_id_fk);
                if (status == null)
                {
                    return NotFound("Status not found");
                }

                bookInstance.bookshelf_id_fk = null;
                bookInstance.bookshelf = null;

                _context.borrows.Add(borrow);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBorrow), new { id = Guid.NewGuid() }, borrow);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Wystąpił błąd podczas wypożyczania książki.");
            }
        }


        [HttpPut("ReturnBook/")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> ReturnBook(ReturnDto returnDto)
        {
            var borrow = await _context.borrows.FirstOrDefaultAsync(b => b.user_id_fk == returnDto.user_id && b.book_instance_id_fk == returnDto.book_instance_id && b.returnedTime == null);
            if (borrow == null)
            {
                return BadRequest();
            }

            borrow.returnedTime = DateTimeOffset.UtcNow;

            var bookInstance = await _context.book_instances.FindAsync(returnDto.book_instance_id);
            if (bookInstance == null || bookInstance.status_id_fk != 2)
            {
                return BadRequest("Książka nie jest wypożyczona.");
            }

            bookInstance.status_id_fk = 1;
            bookInstance.bookshelf_id_fk = returnDto.bookshelf_id;

            _context.Entry(borrow).State = EntityState.Modified;
            _context.Entry(bookInstance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BorrowExists(borrow.borrow_id))
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


        [HttpPut("TransferBook")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> TransferBook(TransferDto transferDto)
        {
            try
            {
                
                var existingBorrow = await _context.borrows.FirstOrDefaultAsync(b =>
                    b.borrow_id == transferDto.borrow_id &&
                    b.returnedTime == null);

                if (existingBorrow == null)
                {
                    return BadRequest("Wypożyczenie nie istnieje.");
                }

                
                var existingBorrowForNewUser = await _context.borrows.FirstOrDefaultAsync(b =>
                    b.borrow_id == transferDto.borrow_id &&
                    b.user_id_fk == transferDto.user_id);

                
                if (existingBorrowForNewUser != null)
                {
                    return Conflict("Książka jest już wypożyczona przez nowego użytkownika.");
                }

                
                existingBorrow.user_id_fk = transferDto.user_id;

                
                await _context.SaveChangesAsync();

                
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

            [HttpPut("ExtendReturnTime/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> ExtendReturnTime(Guid id)
        {
            var borrow = await _context.borrows.FirstOrDefaultAsync(b => b.borrow_id == id);

            if (borrow == null)
            {
                return NotFound();
            }

            if (borrow.extended)
            {
                return BadRequest("Return time has already been extended.");
            }

            borrow.returnTime = borrow.returnTime.AddDays(7);
            borrow.extended = true;

            await _context.SaveChangesAsync();

            return NoContent();
        }

    }

}
