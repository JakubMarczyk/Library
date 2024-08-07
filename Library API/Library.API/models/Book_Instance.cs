using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.API.models
{
    public class Book_Instance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid book_instance_id { get; set; }
        public Guid book_id_fk { get; set; }
        public Book? book { get; set; }
        public Guid? bookshelf_id_fk { get; set; }
        public Bookshelf? bookshelf { get; set; }
        public int status_id_fk { get; set; }
        public Status? status { get; set; }
        public virtual ICollection<Borrow>? borrows { get; set; }
    }
}
