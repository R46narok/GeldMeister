namespace BankStatements.Domain.BankAggregate;

public class TransactionField : AuditableEntityBase
{
    public BankSchemeProperty Property { get; private init; } 
    public Transaction Transaction { get; private init; }
    public byte[] Value { get; private set; } 
        

    private TransactionField()
    {
        
    }

    public static TransactionField? Create(Transaction transaction, BankSchemeProperty property, byte[] value)
    {
        var field = new TransactionField
        {
            Id = Guid.NewGuid(),
            Property = property,
            Value = value,
            Transaction = transaction
        };

        return field;
    }
}