using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.API.models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid user_id { get; set; }
        public string nickname { get; set; }
        public string email { get; set; }
        public bool is_admin { get; set; }
        public Password password { get; set; }
        public virtual ICollection<Borrow>? borrowed { get; set; }
    }
}
