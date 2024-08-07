namespace Library.API.dtos
{
    public class ReturnDto
    {
        public Guid user_id { get; set; }
        public Guid book_instance_id { get; set; }
        public Guid bookshelf_id { get; set; }
    }
}
