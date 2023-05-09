using BankStatements.Application.Common.Repositories;
using FluentValidation;

namespace BankStatements.Application.Banks.Commands.Update;

public class UpdateBankCommandValidator : AbstractValidator<UpdateBankCommand>
{
    public UpdateBankCommandValidator(IBankRepository repository)
    {
        RuleFor(cmd => cmd.Id)
            .MustAsync(async (id, _) => await repository.GetByIdAsync(id) is not null)
            .WithMessage("Bank does not exist in the database");

        RuleFor(cmd => cmd.Name)
            .MustAsync(async (name, _) => await repository.FindByNameAsync(name, false) is null)
            .WithMessage("Bank names must be unique!");
    }
}