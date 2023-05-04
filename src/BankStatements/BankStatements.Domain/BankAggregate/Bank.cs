using System.ComponentModel.DataAnnotations;
using BankStatements.Domain.BankAggregate.Events;

namespace BankStatements.Domain.BankAggregate;

public class Bank : AuditableEntityBase, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    
    public BankScheme? Scheme { get; private set; }
    
    private Bank()
    {
        
    }

    public static Bank? Create(string name)
    {
        if (string.IsNullOrEmpty(name)) return null;

        var bank = new Bank
        {
            Id = Guid.NewGuid(),
            Name = name
        };
        
        bank.Raise(new BankCreatedDomainEvent(Guid.NewGuid(), bank.Id));

        return bank;
    }
    
    public void ChangeName(string name)
    {
        Raise(new BankNameChangedDomainEvent(Guid.NewGuid(), Id));
        Name = name;
    }

    public void AddScheme(BankScheme? scheme)
    {
        Scheme = scheme;
    }
}