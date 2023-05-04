using GeldMeister.Common.Domain;
using Microsoft.AspNetCore.Identity;

namespace GeldMeister.Users.Data.Entities;

public sealed class User : IdentityUser, IEntity<string>
{
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    
    public string? Github { get; set; }
    public string? Instagram { get; set; }
    public string? Twitter { get; set; }
    public byte[]? ProfilePicture { get; set; }

    public User()
    {
        
    }
    
    public User(string username)
    {
        UserName = username;
    }
}