using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Library.API.data;
using Library.API.models;
using Library.API.dto;
using Library.API.dtos;
using NuGet.Packaging;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDbContext _context;

        public BooksController(LibraryDbContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Getbooks(
    [FromQuery(Name = "authorIds")] string authorIdsCsv,
    [FromQuery(Name = "categoryIds")] string categoryIdsCsv)
        {
            if (_context.books == null)
            {
                return NotFound();
            }

            IQueryable<Book> query = _context.books;

            // Parsowanie identyfikatorów autorów
            List<Guid> authorIds = ParseGuidList(authorIdsCsv);
            if (authorIds != null && authorIds.Any())
            {
                query = query.Where(b => b.authors.Any(a => authorIds.Contains(a.author_id)));
            }

            // Parsowanie identyfikatorów kategorii
            List<Guid> categoryIds = ParseGuidList(categoryIdsCsv);
            if (categoryIds != null && categoryIds.Any())
            {
                query = query.Where(b => b.categories.Any(c => categoryIds.Contains(c.category_id)));
            }

            var books = await query
                .Select(b => new BookDto
                {
                    book_id = b.book_id,
                    title = b.title,
                    isbn = b.isbn,
                    publisher = b.publisher,
                    yearOfPublication = b.yearOfPublication,
                    cover = b.cover,
                    description = b.description,
                    pages = b.pages,
                    authors = b.authors.Select(a => new AuthorDto
                    {
                        author_id = a.author_id,
                        firstName = a.firstName,
                        lastName = a.lastName
                    }).ToList(),
                    categories = b.categories.Select(c => new BookCategoryDto
                    {
                        category_id = c.category_id,
                        name = c.name,
                    }).ToList(),
                    book_instances = b.book_instances.Select(bi => new Book_InstanceDto
                    {
                        book_instance_id = bi.book_instance_id,
                        bookshelf = bi.bookshelf.bookshelf_id != null ? new BookshelfDto
                        {
                            bookshelf_id = bi.bookshelf.bookshelf_id,
                            name = bi.bookshelf.name,
                            floor = bi.bookshelf.floor
                        } : null,
                        status = new StatusDto
                        {
                            status_id = bi.status.status_id,
                            name = bi.status.name
                        },
                    }).ToList()
                }).ToListAsync();

            return Ok(books);
        }


        [HttpGet("bookshelfBooks/{bookshelf_id}")]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetBookshelfBooks(Guid bookshelf_id)
        {
            try
            {
                var books = await _context.books
                    .Where(b => b.book_instances.Any(bi => bi.bookshelf.bookshelf_id == bookshelf_id))
                    .Select(b => new BookDto
                    {
                        book_id = b.book_id,
                        title = b.title,
                        isbn = b.isbn,
                        publisher = b.publisher,
                        yearOfPublication = b.yearOfPublication,
                        cover = b.cover,
                        description = b.description,
                        pages = b.pages,
                        authors = b.authors.Select(a => new AuthorDto
                        {
                            author_id = a.author_id,
                            firstName = a.firstName,
                            lastName = a.lastName
                        }).ToList(),
                        categories = b.categories.Select(c => new BookCategoryDto
                        {
                            category_id = c.category_id,
                            name = c.name,
                        }).ToList(),
                        book_instances = b.book_instances.Select(bi => new Book_InstanceDto
                        {
                            book_instance_id = bi.book_instance_id,
                            bookshelf = bi.bookshelf.bookshelf_id != null ? new BookshelfDto
                            {
                                bookshelf_id = bi.bookshelf.bookshelf_id,
                                name = bi.bookshelf.name,
                                floor = bi.bookshelf.floor
                            } : null,
                            status = new StatusDto
                            {
                                status_id = bi.status.status_id,
                                name = bi.status.name
                            },
                        }).ToList()
                    }).ToListAsync();

                if (books == null || books.Count == 0)
                {
                    return NotFound("No books found in the specified bookshelf.");
                }

                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving books: {ex.Message}");
            }
        }
    

    private List<Guid> ParseGuidList(string csvList)
        {
            if (string.IsNullOrEmpty(csvList))
            {
                return null;
            }

            return csvList.Split(',')
                .Select(id => Guid.TryParse(id, out var guid) ? guid : Guid.Empty)
                .Where(guid => guid != Guid.Empty)
                .ToList();
         }


        [HttpGet("Search/{searchedText}")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchBooks(string searchedText)
        {
            if (_context.books == null)
            {
                return NotFound();
            }
            searchedText = searchedText.ToLower();
            var books = await _context.books.Where(b => b.title.ToLower().Contains(searchedText) || b.description.ToLower().Contains(searchedText)
             || b.authors.Any(a => a.firstName.ToLower().Contains(searchedText)) || b.authors.Any(a => a.lastName.ToLower().Contains(searchedText)) || b.categories.Any(c => c.name.ToLower().Contains(searchedText)))
                .Select(b => new BookDto
                {
                    book_id = b.book_id,
                    title = b.title,
                    isbn = b.isbn,
                    publisher = b.publisher,
                    yearOfPublication = b.yearOfPublication,
                    cover = b.cover,
                    description = b.description,
                    pages = b.pages,
                    authors = b.authors.Select(a => new AuthorDto
                    {
                        author_id = a.author_id,
                        firstName = a.firstName,
                        lastName = a.lastName

                    }).ToList(),
                    categories = b.categories.Select(c => new BookCategoryDto
                    {
                        category_id = c.category_id,
                        name = c.name,
                    }).ToList(),
                    book_instances = b.book_instances.Select(bi => new Book_InstanceDto
                    {
                        book_instance_id = bi.book_instance_id,
                        bookshelf = bi.bookshelf.bookshelf_id != null ? new BookshelfDto
                        {
                            bookshelf_id = bi.bookshelf.bookshelf_id,
                            name = bi.bookshelf.name,
                            floor = bi.bookshelf.floor
                        } : null,
                        status = new StatusDto
                        {
                            status_id = bi.status.status_id,
                            name = bi.status.name
                        },
                    }).ToList()
                }).ToListAsync();

            return Ok(books);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(Guid id)
        {
          if (_context.books == null)
          {
              return NotFound();
          }
            var book = await _context.books
                .Where(b => b.book_id == id)
                .Select(b => new BookDto
                {
                    book_id = b.book_id,
                    title = b.title,
                    isbn = b.isbn,
                    publisher = b.publisher,
                    yearOfPublication = b.yearOfPublication,
                    cover = b.cover,
                    description = b.description,
                    pages = b.pages,
                    authors = b.authors.Select(a => new AuthorDto
                    {
                        author_id = a.author_id,
                        firstName = a.firstName,
                        lastName = a.lastName

                    }).ToList(),
                    categories = b.categories.Select(c => new BookCategoryDto
                    {
                        category_id = c.category_id,
                        name = c.name,
                    }).ToList(),
                    book_instances = b.book_instances.Select(bi => new Book_InstanceDto
                    {
                        book_instance_id = bi.book_instance_id,
                        bookshelf = bi.bookshelf.bookshelf_id != null ? new BookshelfDto
                        {
                            bookshelf_id = bi.bookshelf.bookshelf_id,
                            name = bi.bookshelf.name,
                            floor = bi.bookshelf.floor
                        } : null,
                        status = new StatusDto
                        {
                            status_id = bi.status.status_id,
                            name = bi.status.name
                        },
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutBook(Guid id, BookDto bookDto)
        {
            var existingBook = await _context.books.Include(b => b.authors).Include(b => b.categories)
                 .FirstOrDefaultAsync(b => b.book_id == bookDto.book_id);

            if (existingBook == null)
            {
                return NotFound($"Book with ID {bookDto.book_id} not found.");
            }

            existingBook.title = bookDto.title;
            existingBook.isbn = bookDto.isbn;
            existingBook.publisher = bookDto.publisher;
            existingBook.yearOfPublication = bookDto.yearOfPublication;
            existingBook.cover = bookDto.cover;
            existingBook.description = bookDto.description;
            existingBook.pages = bookDto.pages;

            List<Author> newAuthors = bookDto.authors.Select(author => new Author
            {
                author_id = author.author_id,
                firstName = author.firstName,
                lastName = author.lastName
            }).ToList();

            List<Category> newCategories = bookDto.categories.Select(category => new Category
            {
                category_id = category.category_id,
                name = category.name
            }).ToList();

            foreach (var author in existingBook.authors)
            {
                if (!newAuthors.Contains(author))
                    existingBook.authors.Remove(author);
            }
            foreach (var newAuthor in newAuthors)
            {
                var existingAuthor = _context.authors.FirstOrDefault(a => a.author_id == newAuthor.author_id);

                if (existingAuthor == null)
                {
                    _context.authors.Attach(newAuthor);
                    existingBook.authors.Add(newAuthor);
                }
                else
                {
                    existingBook.authors.Add(existingAuthor);
                }
            }

            foreach (var category in existingBook.categories.ToList())
            {
                if (!newCategories.Contains(category))
                    existingBook.categories.Remove(category);
            }

            foreach (var newCategory in newCategories)
            {
                var existingCategory = _context.categories.FirstOrDefault(c => c.category_id == newCategory.category_id);

                if (existingCategory == null)
                {
                    _context.categories.Attach(newCategory);
                    existingBook.categories.Add(newCategory);
                }
                else
                {
                    existingBook.categories.Add(existingCategory);
                }
            }


            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Book>> PostBook(BookDto bookDto)
        {
            if (_context.books == null)
            {
                return Problem("Entity set 'LibraryDbContext.books'  is null.");
            }
            var book = new Book
            {
                title = bookDto.title,
                isbn = bookDto.isbn,
                publisher = bookDto.publisher,
                yearOfPublication = bookDto.yearOfPublication,
                cover = bookDto.cover,
                description = bookDto.description,
                pages = bookDto.pages
            };

            _context.books.Add(book);

            if (bookDto.authors != null && bookDto.authors.Any())
            {
                var authorIds = bookDto.authors.Select(authorDto => authorDto.author_id).ToList();
                var authors = _context.authors.Where(author => authorIds.Contains(author.author_id)).ToList();

                if (book.authors == null)
                {
                    book.authors = new List<Author>();
                }

                book.authors.AddRange(authors);
            }

            if (bookDto.categories != null && bookDto.categories.Any())
            {
                var categoryIds = bookDto.categories.Select(categoryDto => categoryDto.category_id).ToList();
                var categories = _context.categories.Where(category => categoryIds.Contains(category.category_id)).ToList();

                if (book.categories == null)
                {
                    book.categories = new List<Category>();
                }

                book.categories.AddRange(categories);
            }


            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = Guid.NewGuid() }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            if (_context.books == null)
            {
                return NotFound();
            }
            var book = await _context.books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(Guid id)
        {
            return (_context.books?.Any(e => e.book_id == id)).GetValueOrDefault();
        }


    }
}
