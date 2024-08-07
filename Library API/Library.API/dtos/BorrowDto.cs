namespace Library.API.dtos
{
    public class BorrowDto
    {
        public Guid user_id { get; set; }
        public Guid book_instance_id { get; set; }
    }
}
