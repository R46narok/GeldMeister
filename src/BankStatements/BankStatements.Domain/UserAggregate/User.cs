using BankStatements.Domain.UserAggregate.Events;

namespace BankStatements.Domain.UserAggregate;

public class User : AuditableEntityBase, IAggregateRoot
{
    public string ExternalId { get; private init; } = string.Empty;
    
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