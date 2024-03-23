using Lesson_3_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson_3_App.Db
{
    public class AppDbContext : DbContext
    {
        //"Host=localhost;Username=postgres;Password=example;Database=Asp_les_1"
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public string _connectionString;
        public AppDbContext()
        {

        }

        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.HasKey(x => x.Id).HasName("ProductID");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
                      .HasColumnName("ProductName")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.Description)
                      .HasColumnName("Description")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.Cost)
                      .HasColumnName("Cost")
                      .IsRequired();

                entity.HasOne(x => x.Category).WithMany(c => c.Products).HasForeignKey(q => q.CategoryId).HasConstraintName("CategoryToProduct");
                entity.HasMany(x => x.Storages).WithOne(c => c.Product).HasForeignKey(q => q.ProductId).HasConstraintName("StoragesToProduct");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");

                entity.HasKey(x => x.Id).HasName("CategoryId");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
                      .HasColumnName("CategoryName")
                      .HasMaxLength(255)
                      .IsRequired();
            });

            modelBuilder.Entity<Storage>(entity =>
            {
                entity.ToTable("Storages");

                entity.HasKey(x => x.Id).HasName("StorageId");
                entity.HasIndex(x => x.Name).IsUnique();

                entity.Property(e => e.Name)
                      .HasColumnName("StorageName")
                      .HasMaxLength(255)
                      .IsRequired();

                entity.Property(e => e.Amount)
                      .HasColumnName("Amount");
            });
        }
    }
}
