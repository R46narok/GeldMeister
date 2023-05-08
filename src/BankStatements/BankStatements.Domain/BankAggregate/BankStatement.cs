using BankStatements.Domain.UserAggregate;

namespace BankStatements.Domain.BankAggregate;

public class BankStatement : AuditableEntityBase
{
    public Bank Bank { get; private init; } = null!;
    // public User User { get; private init; } = null!;

    // public Guid TransactionId { get; private init; } 

    private BankStatement()
    {
        
    }

    public static BankStatement? Create(Bank bank, User user)
    {
        var statement = new BankStatement
        {
            Bank = bank,
            // User = user
        };

        
        return statement;
    }
}