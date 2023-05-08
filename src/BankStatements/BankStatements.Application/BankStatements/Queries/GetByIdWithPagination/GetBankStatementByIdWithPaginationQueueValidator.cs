using BankStatements.Application.Common.Repositories;
using FluentValidation;

namespace BankStatements.Application.BankStatements.Queries.GetByIdWithPagination;

public class GetBankStatementByIdWithPaginationQueueValidator 
    : AbstractValidator<GetBankStatementByIdWithPaginationQuery>
{
    public GetBankStatementByIdWithPaginationQueueValidator(IBankStatementRepository repository)
    {
        RuleFor(q => q.Id)
            .MustAsync(async (id, _) => await repository.GetByIdAsync(id, false, false) is not null)
            .WithErrorCode("The bank statement does not exist in the database");

        RuleFor(q => q.PageIndex)
            .GreaterThanOrEqualTo(0);
        
        RuleFor(q => q.PageSize)
            .GreaterThanOrEqualTo(15)
            .LessThanOrEqualTo(50);
    }
}