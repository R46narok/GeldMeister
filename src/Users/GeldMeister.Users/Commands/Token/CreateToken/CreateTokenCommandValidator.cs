using FluentValidation;
using GeldMeister.Users.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using V9.Application;

namespace GeldMeister.Users.Commands.Token.CreateToken;

public class CreateTokenCommandValidator : AbstractValidator<CreateTokenCommand>
{
    public CreateTokenCommandValidator(UserManager<User> userManager)
    {
        RuleFor(x => new {x.UserName, x.Password})
            .MustAsync(async (command, _) =>
            {
                var user = await userManager.FindByNameAsync(command.UserName);
                return await userManager.CheckPasswordAsync(user, command.Password);
            })
            .WithName("Password")
            .WithMessage("Password is not correct.");
    }
}