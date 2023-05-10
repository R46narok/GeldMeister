using AutoMapper;
using BankStatements.Application.Common.Dto;
using BankStatements.Application.Common.Interfaces;
using BankStatements.Application.Common.Repositories;
using ErrorOr;
using MediatR;

namespace BankStatements.Application.BankStatements.Queries.GetByIdWithPagination;

public record GetBankStatementByIdWithPaginationQuery(Guid Id, int PageIndex, int PageSize) :
    IRequest<ErrorOr<BankStatementDto>>;

public class
    GetBankStatementByIdWithPaginationQueryHandler : IRequestHandler<GetBankStatementByIdWithPaginationQuery,
        ErrorOr<BankStatementDto>>
{
    private readonly IMapper _mapper;
    private readonly IBankStatementRepository _statementRepository;

    public GetBankStatementByIdWithPaginationQueryHandler(
        IMapper mapper,
        IBankStatementRepository statementRepository)
    {
        _mapper = mapper;
        _statementRepository = statementRepository;
    }

    public async Task<ErrorOr<BankStatementDto>> Handle(GetBankStatementByIdWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var statement = await _statementRepository
            .GetByIdAsync(request.Id, track: false, includeBank: true, includeTransactions: true);

        var dto = _mapper.Map<BankStatementDto>(statement);
        // dto.Transactions = await _transactionRepository
        //     .GetTransactionsByStatementIdWithPagination(statement!.Bank.Name, request.Id, request.PageIndex,
        //         request.PageSize);

        return dto;
    }
}