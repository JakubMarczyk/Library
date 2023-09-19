using System.ComponentModel.DataAnnotations;

namespace Library.API.models
{
    public class Borrow
    {
        [Key]
        public int borrow_id { get; set; }
        public int book_instance_id_fk { get; set; }
        public Book_Instance? book_instance { get; set; }
        public int user_id_fk { get; set; }
        public User? user { get; set; }
        public DateTime borrowTime { get; set; }
        public DateTime returnTime { get; set; }
        public DateTime? returnedTime { get; set; }
    }
}
