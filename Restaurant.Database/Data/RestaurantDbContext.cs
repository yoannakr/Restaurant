using Restaurant.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Restaurant.Database.Data
{
    public class RestaurantDbContext : DbContext
    {
        public DbSet<Image> Images { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Table> Tables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(DataSettings.Connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
            => builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
    }
}
