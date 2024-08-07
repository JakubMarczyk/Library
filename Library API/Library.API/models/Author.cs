using Npgsql.Internal.TypeHandlers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.API.models
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid author_id { get; set; }
        public string firstName { get; set; }
        public string? lastName { get; set; }
        public virtual ICollection<Book>? books { get; set; }
    }
}
