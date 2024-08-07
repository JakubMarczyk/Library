using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.API.models
{
    public class Bookshelf
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid bookshelf_id { get; set; }
        public string name { get; set; }
        public int floor { get; set; }
        public virtual ICollection<Book_Instance>? book_instances { get; set; }

    }
}
