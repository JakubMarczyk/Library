using Library.API.dto;

namespace Library.API.dtos
{
    public class Book_InstanceDto
    {
        public Guid book_instance_id { get; set; }
        public BookshelfDto? bookshelf { get; set; }
        public StatusDto? status { get; set; }
    }
}
