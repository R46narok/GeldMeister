using AutoMapper;
using BankStatements.Application.Common.Dto;
using BankStatements.Application.Common.Repositories;
using ErrorOr;
using MediatR;

namespace BankStatements.Application.Banks.Queries.GetAll;

public record GetAllBanksQuery : IRequest<ErrorOr<List<BankDto>>>;

public class GetAllBanksQueryHandler : IRequestHandler<GetAllBanksQuery, ErrorOr<List<BankDto>>>
{
    private readonly IMapper _mapper;
    private readonly IBankRepository _repository;

    public GetAllBanksQueryHandler(IBankRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<ErrorOr<List<BankDto>>> Handle(GetAllBanksQuery request, CancellationToken cancellationToken)
    {
        var banks = _repository.GetAll();
        var dtos = banks
            .Select(x => _mapper.Map<BankDto>(x))
            .ToList();

        return dtos;
    }
}