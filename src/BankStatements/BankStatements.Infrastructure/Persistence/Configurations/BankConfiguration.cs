using BankStatements.Domain.BankAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankStatements.Infrastructure.Persistence.Configurations;

public class BankConfiguration : IEntityTypeConfiguration<Bank>
{
    public void Configure(EntityTypeBuilder<Bank> builder)
    {
        builder
            .Property(b => b.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .HasOne(b => b.Scheme)
            .WithOne(b => b.Bank)
            .HasForeignKey<BankScheme>(b => b.BankId);
        builder.Ignore(p => p.DomainEvents);
    }
}