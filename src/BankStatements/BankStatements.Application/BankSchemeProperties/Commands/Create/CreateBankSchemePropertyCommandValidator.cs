using BankStatements.Application.Common.Repositories;
using FluentValidation;

namespace BankStatements.Application.BankSchemeProperties.Commands.Create;

public class CreateBankSchemePropertyCommandValidator : AbstractValidator<CreateBankSchemePropertyCommand>
{
    public CreateBankSchemePropertyCommandValidator(IBankSchemeRepository repository)
    {
        RuleFor(cmd => cmd.SchemeId)
            .MustAsync(async (id, _) => await repository.GetByIdAsync(id) is not null)
            .WithMessage("Scheme not found");

        RuleFor(cmd => cmd.Name)
            .NotEmpty()
            .WithMessage("Name must not be empty");
    }
}