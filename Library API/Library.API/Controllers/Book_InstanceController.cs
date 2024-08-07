using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.API.data;
using Library.API.models;
using Library.API.dtos;
using LibraryAPI.dtos;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Book_InstanceController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public Book_InstanceController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/Book_Instance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book_Instance>>> Getbook_instances()
        {
          if (_context.book_instances == null)
          {
              return NotFound();
          }
            return await _context.book_instances.ToListAsync();
        }

        // GET: api/Book_Instance/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book_Instance>> GetBook_Instance(Guid id)
        {
          if (_context.book_instances == null)
          {
              return NotFound();
          }
            var book_Instance = await _context.book_instances.FindAsync(id);

            if (book_Instance == null)
            {
                return NotFound();
            }

            return book_Instance;
        }

        // GET: api/Book_Instance/5
        [HttpGet("getBookId/{id}")]
        public async Task<ActionResult<Guid>> GetBookId(Guid id)
        {
            if (_context.book_instances == null)
            {
                return NotFound();
            }
            var book_Instance = await _context.book_instances.FindAsync(id);

            if (book_Instance == null)
            {
                return NotFound();
            }

            return book_Instance.book_id_fk;
        }

        // PUT: api/Book_Instance/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutBook_Instance(Guid id, [FromBody] New_Book_InstanceDto bookInstanceDto)
        {
            // Pobranie obiektu Book_Instance z bazy danych
            var book_Instance = await _context.book_instances
                .Include(bi => bi.book)
                .Include(bi => bi.bookshelf)
                .Include(bi => bi.status)
                .FirstOrDefaultAsync(bi => bi.book_instance_id == id);

            if (book_Instance == null)
            {
                return NotFound();
            }

            book_Instance.bookshelf_id_fk = bookInstanceDto.bookshelf_id_fk;

            var status = await _context.statuses.FindAsync(bookInstanceDto.status_id_fk);
            if (status == null)
            {
                return NotFound("Status not found");
            }
            book_Instance.status_id_fk = status.status_id;

            if (status.status_id != 1)
            {
                book_Instance.bookshelf_id_fk = null;
            }

            _context.Entry(book_Instance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Book_InstanceExists(id))
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

        // POST: api/Book_Instance
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> PostBook_Instance(New_Book_InstanceDto bookInstanceDto)
        {
            if (bookInstanceDto.status_id_fk != 1)
            {
                bookInstanceDto.bookshelf_id_fk = null;
            }

            var book_Instance = new Book_Instance
            {
                book_id_fk = bookInstanceDto.book_id_fk,
                bookshelf_id_fk = bookInstanceDto.bookshelf_id_fk,
                status_id_fk = bookInstanceDto.status_id_fk
            };


            var book = await _context.books.FindAsync(book_Instance.book_id_fk);
            if (book == null)
            {
                return NotFound("Book not found");
            }
            book_Instance.book = book;

            if (bookInstanceDto.bookshelf_id_fk.HasValue)
            {
                var bookshelf = await _context.bookshelfs.FindAsync(bookInstanceDto.bookshelf_id_fk);
                if (bookshelf != null)
                {
                    book_Instance.bookshelf = bookshelf;
                }
            }

            var status = await _context.statuses.FindAsync(book_Instance.status_id_fk);
            if (status == null)
            {
                return NotFound("Status not found");
            }
            book_Instance.status = status;

            _context.book_instances.Add(book_Instance);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBook_Instance), new { id = Guid.NewGuid() }, book_Instance);
        }

        // DELETE: api/Book_Instance/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook_Instance(Guid id)
        {
            if (_context.book_instances == null)
            {
                return NotFound();
            }
            var book_Instance = await _context.book_instances.FindAsync(id);
            if (book_Instance == null)
            {
                return NotFound();
            }

            _context.book_instances.Remove(book_Instance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Book_InstanceExists(Guid id)
        {
            return (_context.book_instances?.Any(e => e.book_instance_id == id)).GetValueOrDefault();
        }
    }
}
