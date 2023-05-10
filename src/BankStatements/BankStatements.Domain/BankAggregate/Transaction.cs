namespace BankStatements.Domain.BankAggregate;

public class Transaction : AuditableEntityBase
{
    private readonly HashSet<TransactionField> _transactionFields = new();
    
    public IReadOnlySet<TransactionField> TransactionFields => _transactionFields;
    public BankStatement Statement { get; private set; } = null!;

    private Transaction()
    {
        
    }

    public static Transaction? Create(BankStatement statement)
    {
        // var transaction = 

        return null;
    }
}