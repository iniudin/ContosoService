namespace ContosoService.Features.Items;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

        builder.HasOne(e => e.Parent)
            .WithMany(e => e.Variants)
            .HasForeignKey(e => e.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(e => e.ParentId);
    }
}