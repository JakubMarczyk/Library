using Library.API.models;

namespace Library.API.dto
{
    public class SpotDto
    {
        public int spot_id { get; set; }
        public string name { get; set; }
        public int floor { get; set; }
        public string? description { get; set; }
    }
}
