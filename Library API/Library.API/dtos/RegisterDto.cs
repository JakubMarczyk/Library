using Library.API.models;

namespace Library.API.dtos
{
    public class RegisterDto
    {
        public string nickname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
