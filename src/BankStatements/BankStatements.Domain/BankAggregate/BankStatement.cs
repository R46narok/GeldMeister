using BankStatements.Domain.UserAggregate;

namespace BankStatements.Domain.BankAggregate;

public class BankStatement : AuditableEntityBase
{
    public Bank Bank { get; private set; }
    public User User { get; private set; }
    
}