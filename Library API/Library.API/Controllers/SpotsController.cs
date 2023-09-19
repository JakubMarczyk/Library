using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.API.data;
using Library.API.models;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotsController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public SpotsController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/Spots
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Spot>>> Getspots()
        {
          if (_context.spots == null)
          {
              return NotFound();
          }
            return await _context.spots.ToListAsync();
        }

        // GET: api/Spots/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Spot>> GetSpot(int id)
        {
          if (_context.spots == null)
          {
              return NotFound();
          }
            var spot = await _context.spots.FindAsync(id);

            if (spot == null)
            {
                return NotFound();
            }

            return spot;
        }

        // PUT: api/Spots/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpot(int id, Spot spot)
        {
            if (id != spot.spot_id)
            {
                return BadRequest();
            }

            _context.Entry(spot).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpotExists(id))
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

        // POST: api/Spots
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Spot>> PostSpot(Spot spot)
        {
          if (_context.spots == null)
          {
              return Problem("Entity set 'LibraryDbContext.spots'  is null.");
          }
            _context.spots.Add(spot);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpot", new { id = spot.spot_id }, spot);
        }

        // DELETE: api/Spots/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpot(int id)
        {
            if (_context.spots == null)
            {
                return NotFound();
            }
            var spot = await _context.spots.FindAsync(id);
            if (spot == null)
            {
                return NotFound();
            }

            _context.spots.Remove(spot);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpotExists(int id)
        {
            return (_context.spots?.Any(e => e.spot_id == id)).GetValueOrDefault();
        }
    }
}
