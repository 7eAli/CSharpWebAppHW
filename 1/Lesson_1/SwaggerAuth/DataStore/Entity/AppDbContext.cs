using Microsoft.EntityFrameworkCore;

namespace SwaggerAuth.DataStore.Entity
{
    public class AppDbContext : DbContext
    {
        private static string _connectionString;
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public AppDbContext()
        {

        }

        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString)
                          .UseLazyLoadingProxies();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Login).IsUnique();

                entity.Property(e => e.Password).HasMaxLength(255).IsRequired();

                entity.HasOne(x => x.Role).WithMany(x => x.Users);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(x => x.RoleId);
                entity.HasIndex(x => x.Name).IsUnique();
            });
        }
    }
}
