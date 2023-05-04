using BankStatements.Application.Common.Repositories;
using FluentValidation;

namespace BankStatements.Application.Banks.Commands.Create;

public class CreateBankCommandValidator : AbstractValidator<CreateBankCommand>
{
    public CreateBankCommandValidator(IBankRepository repository)
    {
        RuleFor(cmd => cmd.Name)
            .MustAsync(async (name, _) => await repository.FindByNameAsync(name, false) is null)
            .WithErrorCode("Bank names must be unique!");
    }
}