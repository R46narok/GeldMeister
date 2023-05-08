using BankStatements.Application.Common.Repositories;
using FluentValidation;

namespace BankStatements.Application.BankSchemes.Commands.Update;

public class UpdateBankSchemeCommandValidator : AbstractValidator<UpdateBankSchemeCommand>
{
    public UpdateBankSchemeCommandValidator(IBankSchemeRepository repository)
    {
        RuleFor(cmd => cmd.Id)
            .MustAsync(async (id, _) => await repository.GetByIdAsync(id, false) is not null)
            .WithErrorCode("Bank scheme does not exist in the database");
    }
}