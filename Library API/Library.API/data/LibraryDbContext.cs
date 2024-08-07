using Library.API.models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace Library.API.data
{
    public class LibraryDbContext : DbContext
    {
        internal object AuthorBook;

        public LibraryDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Book> books { get; set; }
        public DbSet<Author> authors { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Book_Instance> book_instances { get; set; }
        public DbSet<Bookshelf> bookshelfs { get; set; }
        public DbSet<Status> statuses { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Password> passwords { get; set; }
        public DbSet<Borrow> borrows { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Book>()
            .HasMany(b => b.authors)
            .WithMany(a => a.books);

            modelBuilder.Entity<Book>()
            .HasMany(b => b.categories)
            .WithMany(c => c.books);

            modelBuilder.Entity<Book_Instance>()
            .HasOne<Book>(bi => bi.book)
            .WithMany(b => b.book_instances)
            .HasForeignKey(bi => bi.book_id_fk);

            modelBuilder.Entity<Book_Instance>()
            .HasOne<Bookshelf>(bi => bi.bookshelf)
            .WithMany(s => s.book_instances)
            .HasForeignKey(bi => bi.bookshelf_id_fk);

            modelBuilder.Entity<Book_Instance>()
            .HasOne<Status>(bi => bi.status)
            .WithMany(s => s.book_instances)
            .HasForeignKey(bi => bi.status_id_fk);

            modelBuilder.Entity<User>()
            .HasOne(u => u.password)
            .WithOne(p => p.user)
            .HasForeignKey<Password>(p => p.user_id_fk)
            .IsRequired();

            modelBuilder.Entity<Borrow>()
            .HasOne<Book_Instance>(b => b.book_instance)
            .WithMany(bi => bi.borrows)
            .HasForeignKey(b => b.book_instance_id_fk);

            modelBuilder.Entity<Borrow>()
            .HasOne<User>(b => b.user)
            .WithMany(u => u.borrowed)
            .HasForeignKey(b => b.user_id_fk);

            modelBuilder.Entity<Book>()
            .Property(b => b.cover)
            .HasDefaultValue("https://justfunfacts.com/wp-content/uploads/2021/03/black.jpg");

            modelBuilder.Entity<Category>()
            .Property(c => c.image)
            .HasDefaultValue("https://justfunfacts.com/wp-content/uploads/2021/03/black.jpg");

            base.OnModelCreating(modelBuilder);
        }
    }
}