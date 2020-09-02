using Restaurant.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Restaurant.Database.Data.Configuration
{
    public class ItemCategoryConfiguration : IEntityTypeConfiguration<ItemCategory>
    {
        public void Configure(EntityTypeBuilder<ItemCategory> builder)
        {
            builder
                .HasKey(itc => new { itc.ItemId, itc.CategoryId });

            builder
                .HasOne(itc => itc.Category)
                .WithMany(c => c.Items)
                .HasForeignKey(c => c.CategoryId);

            builder
               .HasOne(itc => itc.Item)
               .WithMany(it => it.Categories)
               .HasForeignKey(it => it.ItemId);
        }
    }
}
