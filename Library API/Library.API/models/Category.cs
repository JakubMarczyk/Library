using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.API.models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid category_id { get; set; }
        public string name { get; set; }
        public string? image { get; set; }
        public virtual ICollection<Book>? books { get; set; }
    }
}

