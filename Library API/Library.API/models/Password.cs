using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.API.models
{
    public class Password
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid password_id { get; set; }
        public string salt { get; set; }
        public string hash { get; set; }
        public Guid user_id_fk { get; set; }
        public User user { get; set; }
    }
}
