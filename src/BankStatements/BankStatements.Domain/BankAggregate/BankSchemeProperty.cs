using System.ComponentModel.DataAnnotations;

namespace BankStatements.Domain.BankAggregate;

public class BankSchemeProperty : AuditableEntityBase
{
    [Required] public BankScheme Scheme { get; private set; }

    private BankSchemeProperty()
    {
        
    }

    public static BankSchemeProperty Create(BankScheme scheme)
    {
        var property = new BankSchemeProperty
        {
            Id = Guid.NewGuid(),
            Scheme = scheme
        };

        return property;
    }
}