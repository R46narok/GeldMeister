using BankStatements.Domain.BankAggregate;
using BankStatements.Domain.UserAggregate.Events;

namespace BankStatements.Domain.UserAggregate;

public class User : AuditableEntityBase, IAggregateRoot
{
    private readonly HashSet<BankStatement> _bankStatements = new();
    public string ExternalId { get; private init; } = string.Empty;
    public IReadOnlySet<BankStatement> BankStatements => _bankStatements;

    private User()
    {
        
    }

    public static User? Create(string externalId)
    {
        if (string.IsNullOrEmpty(externalId)) return null;

        var user = new User
        {
            Id = Guid.NewGuid(),
            ExternalId = externalId,
            CreatedOn = DateTime.Now,
            LastModifiedOn = DateTime.Now
        };
        
        user.Raise(new UserCreatedDomainEvent(Guid.NewGuid(), user.Id));

        return user;
    }
}