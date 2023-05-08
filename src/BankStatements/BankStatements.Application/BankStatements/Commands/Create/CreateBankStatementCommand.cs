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
    private readonly ICurrentUserService _userService;

    public CreateBankStatementCommandHandler(
        IBankStatementParserFactory factory,
        IBankSchemeRepository repository,
        IDynamicTransactionRepository transactionRepository,
        IBankStatementRepository statementRepository,
        ICurrentUserService userService
    )
    {
        _factory = factory;
        _repository = repository;
        _transactionRepository = transactionRepository;
        _statementRepository = statementRepository;
        _userService = userService;
    }

    public async Task<ErrorOr<CreateBankStatementCommandResponse>> Handle(CreateBankStatementCommand request,
        CancellationToken cancellationToken)
    {
        var scheme = await _repository.FindByBankId(request.BankId, includeProperties: true, includeBank: true);
        var name = scheme!.Bank.Name;

        if (!await _transactionRepository.IsTransactionTypeCreated(name))
            await _transactionRepository.CreateTransactionType(name, scheme);

        var guid = await _statementRepository.CreateAsync(BankStatement.Create(scheme.Bank, _userService.UserId)!);

        await CreateTransactionsFromFile(request.File, scheme, guid);

        return new CreateBankStatementCommandResponse(guid);
    }

    private async Task CreateTransactionsFromFile(
        IFormFile file,
        BankScheme scheme,
        Guid statementId)
    {
        var parser = _factory.Create(scheme.FileType);
        await using var stream = file.OpenReadStream();
        var transactions = (await parser.Parse(new StreamReader(stream), scheme))
            .Select(x => x as IDictionary<string, object>)
            .ToList();

        foreach (var transaction in transactions)
        {
            await _transactionRepository
                .CreateTransaction(scheme.Bank.Name, scheme, statementId, transaction!);
        }
    }
}