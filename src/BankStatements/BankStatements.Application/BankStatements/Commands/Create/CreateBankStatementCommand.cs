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
    private readonly IBankStatementRepository _statementRepository;
    private readonly ICurrentUserService _userService;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IBinarySerializer _serializer;

    public CreateBankStatementCommandHandler(
        IBankStatementParserFactory factory,
        IBankSchemeRepository repository,
        IBankStatementRepository statementRepository,
        ICurrentUserService userService,
        ITransactionRepository transactionRepository,
        IBinarySerializer serializer)
    {
        _factory = factory;
        _repository = repository;
        _statementRepository = statementRepository;
        _userService = userService;
        _transactionRepository = transactionRepository;
        _serializer = serializer;
    }

    public async Task<ErrorOr<CreateBankStatementCommandResponse>> Handle(CreateBankStatementCommand request,
        CancellationToken cancellationToken)
    {
        var scheme = await _repository.FindByBankId(request.BankId, includeProperties: true, includeBank: true);

        var statement = await _statementRepository.CreateAsync(BankStatement.Create(scheme.Bank, _userService.UserId)!);
        await CreateTransactionsFromFile(request.File, scheme, statement);

        return new CreateBankStatementCommandResponse(statement.Id);
    }

    private async Task CreateTransactionsFromFile(IFormFile file,
        BankScheme scheme,
        BankStatement statement)
    {
        await using var stream = file.OpenReadStream();
        var properties = scheme.Properties;
        var parser = _factory.Create(scheme.FileType);
        var entries =
            (await parser.Parse(new StreamReader(stream), scheme))
            .Select(x => x as IDictionary<string, object>)
            .ToList();

        foreach (var entry in entries)
        {
            var transaction = Transaction.Create(statement, new byte[] {1, 2, 3})!; // TODO: salt

            foreach (var pair in entry!)
            {
                var property = GetPropertyByName(properties, pair.Key) ?? throw new ArgumentException();
                var bytes = _serializer.Serialize(pair.Value, property.Type);
                var field = TransactionField.Create(transaction, property, bytes)!;
                transaction.AddField(field);
            }

            await _transactionRepository.CreateAsync(transaction);
        }
    }

    private BankSchemeProperty? GetPropertyByName(IReadOnlySet<BankSchemeProperty> properties, string name)
    {
        return properties.FirstOrDefault(t => t.Name == name);
    }
}