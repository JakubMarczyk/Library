using System.ComponentModel.DataAnnotations;

namespace Library.API.models
{
    public class Spot
    {
        [Key]
        public int spot_id { get; set; }
        public string name { get; set; }
        public int floor { get; set; }
        public string? description { get; set; }
        public virtual ICollection<Book_Instance>? book_instances { get; set; }

    }
}
