using BankStatements.Domain.BankAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankStatements.Infrastructure.Persistence.Configurations;

public class TransactionFieldConfiguration : IEntityTypeConfiguration<TransactionField>
{
    public void Configure(EntityTypeBuilder<TransactionField> builder)
    {
        builder
            .HasOne(f => f.Transaction)
            .WithMany(f => f.TransactionFields);
        
        builder
            .Property(f => f.Value)
            .IsRequired();
        
        builder.Ignore(p => p.DomainEvents);
    }
}