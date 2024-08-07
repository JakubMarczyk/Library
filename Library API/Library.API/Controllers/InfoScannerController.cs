using Library.API.data;
using Library.API.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
public class ScanDto
{
    public string Type { get; set; }
    public Guid Id { get; set; }
}

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoScannerController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public InfoScannerController(LibraryDbContext context)
        {
            _context = context;
        }

        [HttpPost("Scan")]
        public async Task<ActionResult<bool>> ScanEntity(ScanDto scanDto)
        {
            try
            {
                switch (scanDto.Type.ToLower())
                {
                    case "book":
                        var bookExists = await _context.book_instances.AnyAsync(b => b.book_instance_id == scanDto.Id);
                        return Ok(bookExists);
                    case "category":
                        var categoryExists = await _context.categories.AnyAsync(c => c.category_id == scanDto.Id);
                        return Ok(categoryExists);
                    case "bookshelf":
                        var bookshelfExists = await _context.bookshelfs.AnyAsync(b => b.bookshelf_id == scanDto.Id);
                        return Ok(bookshelfExists);
                    default:
                        return BadRequest("Invalid type specified.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }


}
