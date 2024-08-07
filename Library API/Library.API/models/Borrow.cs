using System.ComponentModel.DataAnnotations;

namespace Library.API.models
{
    public class Borrow
    {
        [Key]
        public Guid borrow_id { get; set; }
        public Guid book_instance_id_fk { get; set; }
        public Book_Instance? book_instance { get; set; }
        public Guid user_id_fk { get; set; }
        public User? user { get; set; }
        public DateTimeOffset borrowTime { get; set; }
        public DateTimeOffset returnTime { get; set; }
        public DateTimeOffset? returnedTime { get; set; }
        public bool extended { get; set; }
    }
}
