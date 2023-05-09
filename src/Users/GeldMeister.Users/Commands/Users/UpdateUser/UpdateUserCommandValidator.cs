using FluentValidation;
using GeldMeister.Users.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace GeldMeister.Users.Commands.Users.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator(UserManager<User> userManager)
    {
        RuleFor(x => x)
            .MustAsync(async (command, _) =>
            {
                if (command.UserName is not null)
                    return await userManager.FindByNameAsync(command.UserName) is not null;
                if (command.Id is not null)
                    return await userManager.FindByIdAsync(command.Id) is not null;

                return false;
            })
            .WithMessage("Not authorized");
    }
}