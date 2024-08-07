namespace Library.API.dto
{
    public class AuthorDto
    {
        public Guid author_id { get; set; }
        public string firstName { get; set; }
        public string? lastName { get; set; }
    }
}