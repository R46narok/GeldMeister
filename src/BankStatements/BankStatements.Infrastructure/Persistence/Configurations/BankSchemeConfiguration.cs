using BankStatements.Domain.BankAggregate;
using BankStatements.Domain.BankAggregate.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankStatements.Infrastructure.Persistence.Configurations;

public class BankSchemeConfiguration : IEntityTypeConfiguration<BankScheme>
{
    public void Configure(EntityTypeBuilder<BankScheme> builder)
    {
        // builder
        //     .HasOne(s => s.Bank)
        //     .WithOne(b => b.Scheme);
        builder.HasMany(s => s.Properties);
        builder.Property(s => s.FileType)
            .HasConversion(
                f => f.ToString(),
                f => (FileType) Enum.Parse(typeof(FileType), f));
        
        builder.Ignore(p => p.DomainEvents);
    }
}