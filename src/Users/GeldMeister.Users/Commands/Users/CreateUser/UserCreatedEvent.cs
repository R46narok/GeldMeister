using GeldMeister.Common.Integration;

namespace GeldMeister.Users.Commands.Users.CreateUser;

public class UserCreatedEvent : IIntegrationEvent
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}