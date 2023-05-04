using FluentValidation;
using GeldMeister.Users.Data.Entities;
using Microsoft.AspNetCore.Identity;
using V9.Application;

namespace GeldMeister.Users.Queries.Users.GetUser;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator(UserManager<User> userManager)
    {
        RuleFor(x => x.UserName)
            .MustAsync(async (username, _) => await userManager.FindByNameAsync(username) is not null)
            .WithErrorCode("User does not exist in the database");
    }
}