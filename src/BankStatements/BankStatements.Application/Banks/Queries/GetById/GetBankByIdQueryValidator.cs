using BankStatements.Application.Banks.Queries.GetByName;
using BankStatements.Application.Common.Repositories;
using FluentValidation;

namespace BankStatements.Application.Banks.Queries.GetById;

public class GetBankByIdQueryValidator : AbstractValidator<GetBankByIdQuery>
{
    public GetBankByIdQueryValidator(IBankRepository repository)
    {
        RuleFor(cmd => cmd.Id)
            .MustAsync(async (name, _) => await repository.GetByIdAsync(name, false) is not null)
            .WithMessage("Bank not found");
    }
}