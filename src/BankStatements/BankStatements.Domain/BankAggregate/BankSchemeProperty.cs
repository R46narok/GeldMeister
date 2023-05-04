
using BankStatements.Domain.BankAggregate.Enums;

namespace BankStatements.Domain.BankAggregate;

public class BankSchemeProperty : AuditableEntityBase
{
    public BankScheme Scheme { get; private set; }
    public string Name { get; private set; }
    public DataType Type { get; private set; }
    

    private BankSchemeProperty()
    {
        
    }

    public static BankSchemeProperty Create(BankScheme scheme, string name, DataType type)
    {
        var property = new BankSchemeProperty
        {
            Id = Guid.NewGuid(),
            Scheme = scheme,
            Name = name,
            Type = type
        };

        return property;
    }

    public void ChangeName(string name)
    {
        Name = name;
    }

    public void ChangeType(DataType type)
    {
        Type = type;
    }
}