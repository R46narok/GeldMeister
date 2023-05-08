using BankStatements.Domain.BankAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankStatements.Infrastructure.Persistence.Configurations;

public class BankStatementConfiguration : IEntityTypeConfiguration<BankStatement>
{
    public void Configure(EntityTypeBuilder<BankStatement> builder)
    {
        builder.Ignore(x => x.DomainEvents);
    }
}