using BankStatements.Application.Common.Repositories;
using FluentValidation;

namespace BankStatements.Application.Banks.Commands.Delete;

public class DeleteBankCommandValidator : AbstractValidator<DeleteBankCommand>
{
    public DeleteBankCommandValidator(IBankRepository repository)
    {
        RuleFor(cmd => cmd.Id)
            .MustAsync(async (id, _) => await repository.GetByIdAsync(id) is not null)
            .WithErrorCode("Bank does not exist in the database");
    }
}