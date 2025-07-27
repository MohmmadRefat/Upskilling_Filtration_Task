using Microsoft.EntityFrameworkCore;

namespace Upskilling_Task.Models
{
    public class LibraryContext: DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(b => b.Name)
                    .IsRequired();
                entity.Property(b => b.Description)
                .IsRequired();

                entity.Property(b=>b.Price)
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValue(0);
                
                entity.Property(b=>b.Stock)
                    .HasDefaultValue(0);


            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(c => c.Name)
                    .IsRequired();

                entity.Property(c => c.Description)
                    .IsRequired();
                    
            });

        }
    }
    
}

