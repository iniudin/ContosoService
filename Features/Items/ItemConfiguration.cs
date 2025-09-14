using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContosoService.Features.Items;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("items");
        builder.HasKey(e => e.Id);
        builder.HasIndex(e => e.Sku)
            .IsUnique();
        builder.Property(e => e.Sku)
            .HasDefaultValueSql("'SKU-' || lpad(nextval('item_sku_seq')::text, 10, '0')");

    }
}