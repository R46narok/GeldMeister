using BankStatements.Application.Common.Repositories;
using FluentValidation;

namespace BankStatements.Application.Banks.Commands.Update;

public class UpdateBankCommandValidator : AbstractValidator<UpdateBankCommand>
{
    public UpdateBankCommandValidator(IBankRepository repository)
    {
        RuleFor(cmd => cmd.Id)
            .MustAsync(async (id, _) => await repository.GetByIdAsync(id) is not null)
            .WithErrorCode("Bank does not exist in the database");
    }
}