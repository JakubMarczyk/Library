using Library.API.models;

namespace Library.API.dto
{
    public class Book_InstanceDto
    {
        public int book_instance_id { get; set; }
        public SpotDto? spot { get; set; }
        public StatusDto? status { get; set; }
    }
}
