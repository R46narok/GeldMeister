using System.ComponentModel.DataAnnotations;
using BankStatements.Domain.BankAggregate.Enums;

namespace BankStatements.Domain.BankAggregate;

public class BankScheme : AuditableEntityBase
{
    private readonly HashSet<BankSchemeProperty> _properties = new();

    public Bank Bank { get; private init; }
    public Guid BankId { get; private set; }
    public FileType FileType { get; private set; }
    // public bool HasHeader { get; private set; }

    public IReadOnlySet<BankSchemeProperty> Properties => _properties;

    private BankScheme()
    {
        
    }

    public static BankScheme Create(Bank bank, FileType fileType)
    {
        var scheme = new BankScheme
        {
            Id = Guid.NewGuid(),
            Bank = bank,
            FileType = fileType
        };

        return scheme;
    }

    public void AddProperty(BankSchemeProperty property)
    {
        _properties.Add(property);
    }
}