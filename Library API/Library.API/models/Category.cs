using System.ComponentModel.DataAnnotations;

namespace Library.API.models
{
    public class Category
    {
        [Key]
        public int category_id { get; set; }
        public string name { get; set; }
        public string? image { get; set; }
        public virtual ICollection<Book>? books { get; set; }
    }
}

