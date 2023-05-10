
using System.Transactions;

namespace BankStatements.Domain.BankAggregate;

public class BankStatement : AuditableEntityBase
{
    private readonly HashSet<Transaction> _transactions = new();

    public Bank Bank { get; private init; } = null!;
    public Guid UserId { get; private init; }
    public IReadOnlySet<Transaction> Transactions => _transactions;

    private BankStatement()
    {
        
    }

    public static BankStatement? Create(Bank bank, Guid userId)
    {
        var statement = new BankStatement
        {
            Id = Guid.NewGuid(),
            Bank = bank,
            UserId = userId
        };

        
        return statement;
    }
}