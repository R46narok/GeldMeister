using GeldMeister.Common.Integration;

namespace GeldMeister.Users.Commands.Users.DeleteUser;

public class UserDeletedEvent : IIntegrationEvent
{
    public string Id { get; set; }
    public string UserName { get; set; } 
    
    public UserDeletedEvent(string userName, string id)
    {
        UserName = userName;
        Id = id;
    }
}