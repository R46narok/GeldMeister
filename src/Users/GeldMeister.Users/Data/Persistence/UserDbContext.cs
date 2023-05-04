using GeldMeister.Users.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeldMeister.Users.Data.Persistence;

public class UserDbContext : IdentityDbContext<User>
{
    public UserDbContext()
    {
        
    }

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
        
    }
}