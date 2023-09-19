using System.ComponentModel.DataAnnotations;

namespace Library.API.models
{
    public class Status
    {
        [Key]
        public int status_id { get; set; }
        public string name { get; set; }
        public virtual ICollection<Book_Instance>? book_instances { get; set; }
    }
}
