using ErrorOr;
using GeldMeister.Users.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GeldMeister.Users.Commands.Users.UpdateUser;

public record UpdateUserCommandResponse(string Id);

public record UpdateUserCommand(
    string? Id,
    string? UserName, string? Email, string? PhoneNumber,
    string? Github, string? Instagram, string? Twitter,
    byte[]? ProfilePicture
) : IRequest<ErrorOr<UpdateUserCommandResponse>>;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ErrorOr<UpdateUserCommandResponse>>
{
    private readonly UserManager<User> _userManager;

    public UpdateUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ErrorOr<UpdateUserCommandResponse>> Handle(UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await FindUserByNameOrId(request);

        user.UserName = request.UserName ?? user.UserName;
        user.Email = request.Email ?? user.Email;
        user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
        
        user.Github = request.Github ?? user.Github;
        user.Instagram = request.Instagram ?? user.Instagram;
        user.Twitter = request.Twitter ?? user.Twitter;
        
        user.ProfilePicture = request.ProfilePicture ?? user.ProfilePicture;

        await _userManager.UpdateAsync(user);

        return new UpdateUserCommandResponse(user.Id);
    }

    private async Task<User> FindUserByNameOrId(UpdateUserCommand command)
    {
        if (command.Id is null) return await _userManager.FindByNameAsync(command.UserName);
        return await _userManager.FindByIdAsync(command.Id);
    }
}