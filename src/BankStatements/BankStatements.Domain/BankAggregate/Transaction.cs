using BankStatements.Domain.BankAggregate.Events;

namespace BankStatements.Domain.BankAggregate;

public class Transaction : AuditableEntityBase
{
    private readonly HashSet<TransactionField> _transactionFields = new();
    
    public IReadOnlySet<TransactionField> TransactionFields => _transactionFields;
    public BankStatement Statement { get; private init; } = null!;
    public byte[] Salt { get; private init; }
    // public string Name { get; private set; }

    private Transaction()
    {
        
    }

    public static Transaction? Create(BankStatement statement, byte[] salt)
    {
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            Statement = statement,
            Salt = salt
        };

        transaction.Raise(new TransactionCreatedDomainEvent(Guid.NewGuid(), transaction.Id));
        
        return transaction;
    }

    public void AddField(TransactionField field)
    {
        _transactionFields.Add(field);
    }
    
}