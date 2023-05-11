using System.Security.Claims;
using MediatR;
using ErrorOr;
using GeldMeister.Users.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace GeldMeister.Users.Commands.Claims.RemoveClaimCommand;

public record RemoveClaimCommand
    (string? Id, string? UserName, string ClaimType, string ClaimValue) : IRequest<ErrorOr<RemoveClaimCommandResponse>>;

public record RemoveClaimCommandResponse(string Id);

public class RemoveClaimCommandHandler : IRequestHandler<RemoveClaimCommand, ErrorOr<RemoveClaimCommandResponse>>
{
    private readonly UserManager<User> _userManager;

    public RemoveClaimCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<RemoveClaimCommandResponse>> Handle(RemoveClaimCommand request, CancellationToken cancellationToken)
    {
        var user = await FindUserByNameOrId(request);
        await _userManager.RemoveClaimAsync(user, new Claim(request.ClaimType, request.ClaimValue));
        
        return new RemoveClaimCommandResponse(user.Id);
    }
    
    private async Task<User> FindUserByNameOrId(RemoveClaimCommand command)
    {
        if (command.Id is null) return (await _userManager.FindByNameAsync(command.UserName!))!;
        return (await _userManager.FindByIdAsync(command.Id))!;
    }
}