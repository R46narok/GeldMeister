using BankStatements.Domain.BankAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankStatements.Infrastructure.Persistence.Configurations;

public class BankSchemePropertyConfiguration : IEntityTypeConfiguration<BankSchemeProperty>
{
    public void Configure(EntityTypeBuilder<BankSchemeProperty> builder)
    {
        builder.HasOne(p => p.Scheme);
        builder.Ignore(p => p.DomainEvents);
    }
}