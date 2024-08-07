namespace Library.API.dtos
{
    public class CategoriesDto
    {
        public Guid category_id { get; set; }
        public string name { get; set; }
        public string? image { get; set; }
    }
}
