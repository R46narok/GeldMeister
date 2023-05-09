using FluentValidation;
using GeldMeister.Users.Data.Entities;
using Microsoft.AspNetCore.Identity;
using V9.Application;

namespace GeldMeister.Users.Commands.Users.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(UserManager<User> userManager)
    {
        RuleFor(u => u.UserName)
            .MustAsync(async (username, _) => await userManager.FindByNameAsync(username) is null)
            .WithMessage("User already exists");

        RuleFor(u => u.Email)
            .EmailAddress()
            .WithMessage("Value should be a valid email address!");
    }
}