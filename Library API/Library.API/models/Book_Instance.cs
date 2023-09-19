using System.ComponentModel.DataAnnotations;

namespace Library.API.models
{
    public class Book_Instance
    {
        [Key]
        public int book_instance_id { get; set; }
        public int book_id_fk { get; set; }
        public Book? book { get; set; }
        public int spot_id_fk { get; set; }
        public Spot? spot { get; set; }
        public int status_id_fk { get; set; }
        public Status? status { get; set; }
        public virtual ICollection<Borrow>? borrows { get; set; }
    }
}
