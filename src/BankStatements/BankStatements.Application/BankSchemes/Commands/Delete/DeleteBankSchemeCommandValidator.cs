using BankStatements.Application.Common.Repositories;
using FluentValidation;

namespace BankStatements.Application.BankSchemes.Commands.Delete;

public class DeleteBankSchemeCommandValidator : AbstractValidator<DeleteBankSchemeCommand>
{
    public DeleteBankSchemeCommandValidator(IBankSchemeRepository repository)
    {
        RuleFor(cmd => cmd.Id)
            .MustAsync(async (id, _) => await repository.GetByIdAsync(id, false) is not null)
            .WithMessage("Bank scheme does not exist in the database");
    }
}