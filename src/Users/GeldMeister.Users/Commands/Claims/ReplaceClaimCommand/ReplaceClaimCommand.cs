using System.Security.Claims;
using GeldMeister.Users.Data.Entities;
using MediatR;
using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace GeldMeister.Users.Commands.Claims.ReplaceClaimCommand;

public record ReplaceClaimCommand
    (string? Id, string? UserName, string ClaimType, string OldClaimValue, string NewClaimValue) : IRequest<ErrorOr<ReplaceClaimCommandResponse>>;

public record ReplaceClaimCommandResponse(string Id);

public class ReplaceClaimCommandHandler : IRequestHandler<ReplaceClaimCommand, ErrorOr<ReplaceClaimCommandResponse>>
{
    private readonly UserManager<User> _userManager;

    public ReplaceClaimCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<ReplaceClaimCommandResponse>> Handle(ReplaceClaimCommand request, CancellationToken cancellationToken)
    {
        var user = await FindUserByNameOrId(request);
        
        await _userManager.RemoveClaimAsync(user, new Claim(request.ClaimType, request.OldClaimValue));
        await _userManager.AddClaimAsync(user, new Claim(request.ClaimType, request.NewClaimValue));
        
        return new ReplaceClaimCommandResponse(user.Id);
    }
    
    private async Task<User> FindUserByNameOrId(ReplaceClaimCommand command)
    {
        if (command.Id is null) return (await _userManager.FindByNameAsync(command.UserName!))!;
        return (await _userManager.FindByIdAsync(command.Id))!;
    }
}