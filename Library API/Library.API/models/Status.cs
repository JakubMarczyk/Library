using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.API.models
{
    public class Status
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int status_id { get; set; }
        public string name { get; set; }
        public virtual ICollection<Book_Instance>? book_instances { get; set; }
    }
}
