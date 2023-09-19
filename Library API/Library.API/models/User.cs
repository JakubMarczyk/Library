using System.ComponentModel.DataAnnotations;

namespace Library.API.models
{
    public class User
    {
        [Key]
        public int user_id { get; set; }
        public string nickname { get; set; }
        public string email { get; set; }
        public string avatar { get; set; }
        public bool is_admin { get; set; }
        public bool notifications { get; set; }
        public Password password { get; set; }
        public virtual ICollection<Borrow>? borrowed { get; set; }
    }
}
