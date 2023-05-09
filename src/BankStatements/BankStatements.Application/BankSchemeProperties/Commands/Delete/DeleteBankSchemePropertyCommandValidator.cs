using BankStatements.Application.Common.Repositories;
using FluentValidation;

namespace BankStatements.Application.BankSchemeProperties.Commands.Delete;

public class DeleteBankSchemePropertyCommandValidator : AbstractValidator<DeleteBankSchemePropertyCommand>
{
    public DeleteBankSchemePropertyCommandValidator(IBankSchemePropertyRepository repository)
    {
        RuleFor(cmd => cmd.Id)
            .MustAsync(async (id, _) => await repository.GetByIdAsync(id) is not null)
            .WithMessage("Bank scheme property does not exist in the database");
    }
}