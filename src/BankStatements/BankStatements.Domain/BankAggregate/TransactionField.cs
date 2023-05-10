namespace BankStatements.Domain.BankAggregate;

public class TransactionField : AuditableEntityBase
{
    public BankSchemeProperty Property { get; private set; } 
    public byte[] Value { get; private set; } 
    public Transaction Transaction { get; set; }

    private TransactionField()
    {
        
    }

    public static TransactionField? Create(BankSchemeProperty property, byte[] value)
    {
        var field = new TransactionField
        {
            Property = property,
            Value = value
        };

        return field;
    }
}