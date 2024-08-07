using Library.API.dtos;
using Library.API.models;

namespace Library.API.dto
{
    public class BookDto
    {
        public Guid book_id { get; set; }
        public string title { get; set; }
        public string? isbn { get; set; }
        public string? publisher { get; set; }
        public int? yearOfPublication { get; set; }
        public string? cover { get; set; }
        public string? description { get; set; }
        public int? pages { get; set; }
        public virtual ICollection<AuthorDto>? authors { get; set; }
        public virtual ICollection<BookCategoryDto>? categories { get; set; }
        public virtual ICollection<Book_InstanceDto>? book_instances { get; set; }
    }
}