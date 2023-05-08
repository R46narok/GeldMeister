using BankStatements.Application.Common.Repositories;
using FluentValidation;

namespace BankStatements.Application.BankStatements.Commands.Create;

public class CreateBankStatementCommandValidator : AbstractValidator<CreateBankStatementCommand>
{
    public CreateBankStatementCommandValidator(IBankRepository repository)
    {
        RuleFor(cmd => cmd.BankId)
            .MustAsync(async (id, _) => await repository.GetByIdAsync(id, false) is not null)
            .WithErrorCode("Bank does not exist in the database");
    }
}