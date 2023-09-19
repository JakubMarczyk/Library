using Library.API.dto;
using Library.API.models;

namespace Library.API.dtos
{
    public class AddBookDto
    {
        public int book_id { get; set; }
        public string title { get; set; }
        public string? isbn { get; set; }
        public string? publisher { get; set; }
        public int? yearOfPublication { get; set; }
        public string cover { get; set; }
        public string? description { get; set; }
        public int? pages { get; set; }
        public virtual ICollection<AuthorDto>? authors { get; set; }
        public virtual ICollection<BookCategoryDto>? categories { get; set; }
    }
}
