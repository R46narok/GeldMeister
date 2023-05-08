
namespace BankStatements.Domain.BankAggregate;

public class BankStatement : AuditableEntityBase
{
    public Bank Bank { get; private init; } = null!;
    public Guid UserId { get; private init; } 
    // public Guid TransactionId { get; private init; } 

    private BankStatement()
    {
        
    }

    public static BankStatement? Create(Bank bank, Guid userId)
    {
        var statement = new BankStatement
        {
            Bank = bank,
            UserId = userId
        };

        
        return statement;
    }
}