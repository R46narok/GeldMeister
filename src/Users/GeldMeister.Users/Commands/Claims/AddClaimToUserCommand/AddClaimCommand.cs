using System.Security.Claims;
using ErrorOr;
using GeldMeister.Users.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GeldMeister.Users.Commands.Claims.AddClaimToUserCommand;

public record AddClaimCommand
    (string? Id, string? UserName, string ClaimType, string ClaimValue) : IRequest<ErrorOr<AddClaimCommandResponse>>;

public record AddClaimCommandResponse(string Id);

public class AddClaimCommandHandler : IRequestHandler<AddClaimCommand, ErrorOr<AddClaimCommandResponse>>
{
    private readonly UserManager<User> _userManager;

    public AddClaimCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<AddClaimCommandResponse>> Handle(AddClaimCommand request, CancellationToken cancellationToken)
    {
        var user = await FindUserByNameOrId(request);
        await _userManager.AddClaimAsync(user, new Claim(request.ClaimType, request.ClaimValue));
        return new AddClaimCommandResponse(user.Id);
    }

    private async Task<User> FindUserByNameOrId(AddClaimCommand command)
    {
        if (command.Id is null) return (await _userManager.FindByNameAsync(command.UserName!))!;
        return (await _userManager.FindByIdAsync(command.Id))!;
    }
}