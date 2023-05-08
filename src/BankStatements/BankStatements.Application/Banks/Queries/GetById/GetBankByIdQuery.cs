using AutoMapper;
using BankStatements.Application.Common.Dto;
using BankStatements.Application.Common.Repositories;
using BankStatements.Domain.BankAggregate;
using ErrorOr;
using MediatR;

namespace BankStatements.Application.Banks.Queries.GetByName;

public record GetBankByIdQuery(Guid Id) : IRequest<ErrorOr<BankDto>>;

public class GetBankByIdQueryHandler : IRequestHandler<GetBankByIdQuery, ErrorOr<BankDto>>
{
    private readonly IMapper _mapper;
    private readonly IBankRepository _repository;

    public GetBankByIdQueryHandler(IMapper mapper, IBankRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<ErrorOr<BankDto>> Handle(GetBankByIdQuery request, CancellationToken cancellationToken)
    {
        var bank = await _repository.GetByIdAsync(request.Id,
            track: false, 
            includeScheme: true,
            includeSchemeProperties: true);
        
        var dto = _mapper.Map<BankDto>(bank);
        return dto;
    }
}