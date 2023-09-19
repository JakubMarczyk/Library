using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.API.data;
using Library.API.models;
using System.Collections.Generic;
using Library.API.dto;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Book_InstancesController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public Book_InstancesController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/Book_Instances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book_Instance>>> Getbook_instances()
        {
          if (_context.book_instances == null)
          {
              return NotFound();
          }
            var bookInstances = await _context.book_instances
                .Select(bi => new Book_InstanceDto
                {
                    book_instance_id = bi.book_instance_id,
                    spot = new SpotDto
                    {
                        spot_id = bi.spot.spot_id,
                        name = bi.spot.name,
                        floor = bi.spot.floor,
                        description = bi.spot.description
                    },
                    status = new StatusDto
                    {
                        status_id = bi.status.status_id,
                        name = bi.status.name
                    },
                })
                .ToListAsync();

            return Ok(bookInstances);
        }

        // GET: api/Book_Instances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book_Instance>> GetBook_Instance(int id)
        {
          if (_context.book_instances == null)
          {
              return NotFound();
          }
            var book_Instance = await _context.book_instances
                .Where(bi => bi.book_instance_id == id)
                .Select(bi => new Book_InstanceDto
                {
                    book_instance_id = bi.book_instance_id,
                    spot = new SpotDto
                    {
                        spot_id = bi.spot.spot_id,
                        name = bi.spot.name,
                        floor = bi.spot.floor,
                        description = bi.spot.description
                    },
                    status = new StatusDto
                    {
                        status_id = bi.status.status_id,
                        name = bi.status.name
                    },
                })
                .FirstOrDefaultAsync();

            if (book_Instance == null)
            {
                return NotFound();
            }

            return Ok(book_Instance);
        }

        // PUT: api/Book_Instances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook_Instance(int id, Book_Instance book_Instance)
        {
            if (id != book_Instance.book_instance_id)
            {
                return BadRequest();
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

        // POST: api/Book_Instances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book_Instance>> PostBook_Instance(Book_Instance book_Instance)
        {
          if (_context.book_instances == null)
          {
              return Problem("Entity set 'LibraryDbContext.book_instances'  is null.");
          }
            _context.book_instances.Add(book_Instance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook_Instance", new { id = book_Instance.book_instance_id }, book_Instance);
        }

        // DELETE: api/Book_Instances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook_Instance(int id)
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

        private bool Book_InstanceExists(int id)
        {
            return (_context.book_instances?.Any(e => e.book_instance_id == id)).GetValueOrDefault();
        }
    }
}
