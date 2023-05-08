using BankStatements.Application.Common.Interfaces;
using BankStatements.Application.Common.Repositories;
using BankStatements.Domain.BankAggregate;
using BankStatements.Domain.BankAggregate.Enums;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BankStatements.Application.BankStatements.Commands.Create;

public record CreateBankStatementCommandResponse(Guid Id);

public record CreateBankStatementCommand(Guid BankId, IFormFile File)
    : IRequest<ErrorOr<CreateBankStatementCommandResponse>>;

public class CreateBankStatementCommandHandler
    : IRequestHandler<CreateBankStatementCommand,
        ErrorOr<CreateBankStatementCommandResponse>>
{
    private readonly IBankStatementParserFactory _factory;
    private readonly IBankSchemeRepository _repository;
    private readonly IDynamicTransactionRepository _transactionRepository;
    private readonly IBankStatementRepository _statementRepository;

    public CreateBankStatementCommandHandler(
        IBankStatementParserFactory factory,
        IBankSchemeRepository repository,
        IDynamicTransactionRepository transactionRepository,
        IBankStatementRepository statementRepository)
    {
        _factory = factory;
        _repository = repository;
        _transactionRepository = transactionRepository;
        _statementRepository = statementRepository;
    }

    public async Task<ErrorOr<CreateBankStatementCommandResponse>> Handle(CreateBankStatementCommand request,
        CancellationToken cancellationToken)
    {
        var scheme = await _repository.FindByBankId(request.BankId, includeProperties: true, includeBank: true);
        var name = scheme!.Bank.Name;

        if (!await _transactionRepository.IsTransactionTypeCreated(name))
            await _transactionRepository.CreateTransactionType(name, scheme);

        var guid = await _statementRepository.CreateAsync(BankStatement.Create(scheme.Bank, null));
        
        return new CreateBankStatementCommandResponse(guid);
    }
}