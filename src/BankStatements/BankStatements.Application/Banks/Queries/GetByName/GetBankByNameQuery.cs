using AutoMapper;
using BankStatements.Application.Common.Dto;
using BankStatements.Application.Common.Repositories;
using BankStatements.Domain.BankAggregate;
using ErrorOr;
using MediatR;

namespace BankStatements.Application.Banks.Queries.GetByName;

public record GetBankByNameQuery(string Name) : IRequest<ErrorOr<BankDto>>;

public class GetBankByNameQueryHandler : IRequestHandler<GetBankByNameQuery, ErrorOr<BankDto>>
{
    private readonly IMapper _mapper;
    private readonly IBankRepository _repository;

    public GetBankByNameQueryHandler(IMapper mapper, IBankRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ErrorOr<BankDto>> Handle(GetBankByNameQuery request, CancellationToken cancellationToken)
    {
        var bank = await _repository.FindByNameAsync(request.Name,
            track: false, 
            includeScheme: true,
            includeSchemeProperties: true);
        
        var dto = _mapper.Map<BankDto>(bank);
        return dto;
    }
}