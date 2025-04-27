using Domain.Constants;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Builders;

public class CustomerBuilder : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasMany(c => c.Orders);
        
        builder.Property(c => c.FirstName)
            .HasMaxLength(Constants.Customer.FirstNameMaxLength)
            .IsRequired();
        builder.Property(c => c.LastName)
            .HasMaxLength(Constants.Customer.LastNameMaxLength)
            .IsRequired();
        builder.Property(c => c.Address)
            .HasMaxLength(Constants.Customer.AddressMaxLength)
            .IsRequired();
        builder.Property(c => c.ZipCode)
            .HasMaxLength(Constants.Customer.ZipCodeMaxLength)
            .IsRequired();
    }
}