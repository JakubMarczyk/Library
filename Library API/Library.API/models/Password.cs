using System.ComponentModel.DataAnnotations;

namespace Library.API.models
{
    public class Password
    {
        [Key]
        public int password_id { get; set; }
        public string salt { get; set; }
        public string hash { get; set; }
        public int rounds { get; set; }
        public int user_id_fk { get; set; }
        public User user { get; set; }
    }
}
