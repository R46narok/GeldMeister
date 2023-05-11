using FluentValidation;
using GeldMeister.Users.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace GeldMeister.Users.Commands.Claims.RemoveClaimCommand;

public class RemoveClaimCommandValidator : AbstractValidator<RemoveClaimCommand>
{
    public RemoveClaimCommandValidator(UserManager<User> userManager)
    {
        RuleFor(cmd => cmd)
            .Cascade(CascadeMode.Stop)
            
            .Must(cmd => cmd.Id is not null || cmd.UserName is not null)
            .WithMessage("You must specify either Id or username!")
            
            .MustAsync(async (cmd, _) => await FindUserByNameOrId(cmd) is not null)
            .WithMessage("No such user exists!")
            
            .MustAsync(async (cmd, _) =>
            {
                var user = await FindUserByNameOrId(cmd);
                var claims = await userManager.GetClaimsAsync(user!);
                return claims.Any(c => c.Type == cmd.ClaimType && c.Value == cmd.ClaimValue);
            })
            .WithMessage("User does not have the specified claim!");

        async Task<User?> FindUserByNameOrId(RemoveClaimCommand command)
        {
            if (command.Id is null) return (await userManager.FindByNameAsync(command.UserName!))!;
            return (await userManager.FindByIdAsync(command.Id))!;
        }
    }
}