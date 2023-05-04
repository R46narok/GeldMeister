using System.Security.Claims;
using ErrorOr;
using GeldMeister.Users.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using V9.Application;

namespace GeldMeister.Users.Commands.Users.ElevateUser;

public class ElevateUserCommand : IRequest<ErrorOr<string>>
{
    public string? Id { get; set; }
    public string? UserName { get; set; }

    public ElevateUserCommand(string? id = null, string? userName = null)
    {
        Id = id;
        UserName = userName;
    }
}

public class ElevateUserCommandHandler : IRequestHandler<ElevateUserCommand, ErrorOr<string>>
{
    private readonly UserManager<User> _userManager;

    public ElevateUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<string>> Handle(ElevateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await FindUserByNameOrId(request);
        
        await _userManager.ReplaceClaimAsync(user, 
            new Claim(ClaimTypes.Role, "User"),
            new Claim(ClaimTypes.Role, "Admin"));
        return request.UserName;
    }
    
     private async Task<User> FindUserByNameOrId(ElevateUserCommand command)
     {
         if (command.Id is null) return await _userManager.FindByNameAsync(command.UserName);
         return await _userManager.FindByIdAsync(command.Id);
     }
}