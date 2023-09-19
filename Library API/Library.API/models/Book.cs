using System.ComponentModel.DataAnnotations;

namespace Library.API.models
{
    public class Book
    {
        [Key]
        public int book_id { get; set; }
        public string title { get; set; }
        public string? isbn { get; set; }
        public string? publisher { get; set; }
        public int? yearOfPublication { get; set; }
        public string cover { get; set; }
        public string? description { get; set; }
        public int? pages { get; set; }
        public virtual ICollection<Author>? authors { get; set; }
        public virtual ICollection<Category>? categories { get; set; }
        public virtual ICollection<Book_Instance>? book_instances { get; set; }
    }
}
