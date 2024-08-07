namespace LibraryAPI.dtos
{
    public class New_Book_InstanceDto
    {
        public Guid book_id_fk { get; set; }
        public Guid? bookshelf_id_fk { get; set; }
        public int status_id_fk { get; set; }
    }
}
