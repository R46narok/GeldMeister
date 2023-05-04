using AutoMapper;
using BankStatements.Application.Common.Repositories;
using BankStatements.Domain.BankAggregate;
using ErrorOr;
using MediatR;

namespace BankStatements.Application.Banks.Queries.GetByName;

public record GetBankByNameQuery(string Name) : IRequest<ErrorOr<Bank>>;

public class GetBankByNameQueryHandler : IRequestHandler<GetBankByNameQuery, ErrorOr<Bank>>
{
    private readonly IMapper _mapper;
    private readonly IBankRepository _repository;

    public GetBankByNameQueryHandler(IMapper mapper, IBankRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ErrorOr<Bank>> Handle(GetBankByNameQuery request, CancellationToken cancellationToken)
    {
        var bank = await _repository.FindByNameAsync(request.Name, false);
        return bank!;
    }
}