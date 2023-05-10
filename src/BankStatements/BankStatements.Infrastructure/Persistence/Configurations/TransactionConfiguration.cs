using BankStatements.Domain.BankAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankStatements.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder
            .HasMany(t => t.TransactionFields)
            .WithOne(t => t.Transaction)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .Property(t => t.Salt)
            .IsRequired();

        builder.Ignore(p => p.DomainEvents);
    }
}