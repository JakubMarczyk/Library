using Library.API.models;

namespace Library.API.interfaces
{
    public class GetBookInstances
    {
        public int book_instance_id { get; set; }
        public Spot? spot { get; set; }
        public Status? status { get; set; }
    }
}
