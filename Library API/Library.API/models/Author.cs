using System.ComponentModel.DataAnnotations;

namespace Library.API.models
{
    public class Author
    {
        [Key]
        public int author_id { get; set; }
        public string firstName { get; set; }
        public string? lastName { get; set; }
        public virtual ICollection<Book>? books { get; set; }
    }
}
