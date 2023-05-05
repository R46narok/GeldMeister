using AutoMapper;
using BankStatements.Application.Common.Repositories;
using BankStatements.Domain.BankAggregate;
using ErrorOr;
using MediatR;

namespace BankStatements.Application.Banks.Commands.Create;

public record CreateBankCommandResponse(Guid Id);

public record CreateBankCommand(string Name) : IRequest<ErrorOr<CreateBankCommandResponse>>;

public class CreateBankCommandHandler : IRequestHandler<CreateBankCommand, ErrorOr<CreateBankCommandResponse>>
{
    private IBankRepository _repository;
    private IMapper _mapper;

    public CreateBankCommandHandler(IBankRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<CreateBankCommandResponse>> Handle(CreateBankCommand request, CancellationToken cancellationToken)
    {
        var bank = _mapper.Map<Bank>(request);
        var key = await _repository.CreateAsync(bank);

        return new CreateBankCommandResponse(key);
    }
}