using BankStatements.Application.Common.Repositories;
using FluentValidation;

namespace BankStatements.Application.BankSchemeProperties.Commands.Update;

public class UpdateBankSchemePropertyCommandValidator : AbstractValidator<UpdateBankSchemePropertyCommand>
{
    public UpdateBankSchemePropertyCommandValidator(IBankSchemePropertyRepository repository)
    {
        RuleFor(cmd => cmd.Id)
            .MustAsync(async (id, _) => await repository.GetByIdAsync(id) is not null)
            .WithErrorCode("Bank scheme property does not exist in the database");
    }
}