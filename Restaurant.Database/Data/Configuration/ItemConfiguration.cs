using Restaurant.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Restaurant.Database.Data.Configurtion
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder
               .HasOne(itm => itm.Image)
               .WithOne(img => img.Item)
               .HasForeignKey<Item>(itm => itm.ImageId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
