using BankStatements.Application.Common.Repositories;
using FluentValidation;

namespace BankStatements.Application.Banks.Queries.GetByName;

public class GetBankByNameQueryValidator : AbstractValidator<GetBankByNameQuery>
{
    public GetBankByNameQueryValidator(IBankRepository repository)
    {
        RuleFor(cmd => cmd.Name)
            .MustAsync(async (name, _) => await repository.FindByNameAsync(name, false) is not null)
            .WithErrorCode("Bank not found");
    }
}