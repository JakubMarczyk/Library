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

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookshelvesController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BookshelvesController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/Bookshelves
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bookshelf>>> GetBookshelfs()
        {
          if (_context.bookshelfs == null)
          {
              return NotFound();
          }
            return await _context.bookshelfs.ToListAsync();
        }

        // GET: api/Bookshelves/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bookshelf>> GetBookshelf(Guid id)
        {
          if (_context.bookshelfs == null)
          {
              return NotFound();
          }
            var bookshelf = await _context.bookshelfs.FindAsync(id);

            if (bookshelf == null)
            {
                return NotFound();
            }

            return bookshelf;
        }

        // PUT: api/Bookshelves/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutBookshelf(Guid id, Bookshelf bookshelf)
        {
            if (id != bookshelf.bookshelf_id)
            {
                return BadRequest();
            }

            _context.Entry(bookshelf).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookshelfExists(id))
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

        // POST: api/Bookshelves
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Bookshelf>> PostBookshelf(Bookshelf bookshelf)
        {
          if (_context.bookshelfs == null)
          {
              return Problem("Entity set 'LibraryDbContext.Bookshelfs'  is null.");
          }
            _context.bookshelfs.Add(bookshelf);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookshelf", new { id = Guid.NewGuid() }, bookshelf);
        }

        // DELETE: api/Bookshelves/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBookshelf(Guid id)
        {
            if (_context.bookshelfs == null)
            {
                return NotFound();
            }
            var bookshelf = await _context.bookshelfs.FindAsync(id);
            if (bookshelf == null)
            {
                return NotFound();
            }

            _context.bookshelfs.Remove(bookshelf);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool BookshelfExists(Guid id)
        {
            return (_context.bookshelfs?.Any(e => e.bookshelf_id == id)).GetValueOrDefault();
        }
    }
}
