using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Builders;

public class ProductBuilder : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products",
            table => table.HasCheckConstraint("CK_Product_Min_Length", $"length({nameof(Product.Name)} >= {Constants.Product.NameMinLength})"));
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name)
            .HasMaxLength(Constants.Product.NameMaxLength)
            .IsRequired();

        builder.Property(p => p.Price)
            .IsRequired();
    }
}