using Restaurant.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Restaurant.Database.Data.Configurtion
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            builder
                .HasOne(ur => ur.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(r => r.RoleId);

            builder
               .HasOne(ur => ur.User)
               .WithMany(u => u.Roles)
               .HasForeignKey(u => u.UserId);
        }
    }
}
