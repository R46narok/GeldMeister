using BankStatements.Application.Common.Repositories;
using FluentValidation;

namespace BankStatements.Application.BankSchemes.Commands.Create;

public class CreateBankSchemeCommandValidator : AbstractValidator<CreateBankSchemeCommand>
{
    public CreateBankSchemeCommandValidator(IBankRepository repository)
    {
        RuleFor(cmd => cmd.BankName)
            .MustAsync(async (name, _) => await repository.FindByNameAsync(name, false) is not null)
            .WithMessage("Bank not found");
    }
}