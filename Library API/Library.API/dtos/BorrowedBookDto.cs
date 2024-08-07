using Library.API.dto;
using Library.API.dtos;

namespace LibraryAPI.dtos
{
    public class BorrowedBookDto
    {
        public Guid borrow_id { get; set; }
        public DateTimeOffset borrowTime { get; set; }
        public DateTimeOffset returnTime { get; set; }
        public bool extended { get; set; }
        public Guid book_instance_id_fk { get; set; }
        public Guid book_id_fk { get; set; }
        public string title { get; set; }
        public string? isbn { get; set; }
        public string? publisher { get; set; }
        public int? yearOfPublication { get; set; }
        public string? cover { get; set; }
        public string? description { get; set; }
        public int? pages { get; set; }
        public virtual ICollection<AuthorDto>? authors { get; set; }
        public virtual ICollection<BookCategoryDto>? categories { get; set; }
    }
}
