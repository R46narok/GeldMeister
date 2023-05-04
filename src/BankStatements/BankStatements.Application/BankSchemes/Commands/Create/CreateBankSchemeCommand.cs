using AutoMapper;
using BankStatements.Application.Common.Repositories;
using BankStatements.Domain.BankAggregate;
using BankStatements.Domain.BankAggregate.Enums;
using ErrorOr;
using MediatR;

namespace BankStatements.Application.BankSchemes.Commands.Create;

public record CreateBankSchemeCommandResponse(Guid Id);

public class CreateBankSchemeCommand : IRequest<ErrorOr<CreateBankSchemeCommandResponse>>
{
    public string BankName { get; set; }
    public FileType FileType { get; set; }
}

public class CreateBankSchemeCommandHandler 
    : IRequestHandler<CreateBankSchemeCommand, ErrorOr<CreateBankSchemeCommandResponse>>
{
    private readonly IMapper _mapper;
    private readonly IBankSchemeRepository _bankSchemeRepository;
    private readonly IBankRepository _bankRepository;


    public CreateBankSchemeCommandHandler(IMapper mapper, IBankSchemeRepository bankSchemeRepository, IBankRepository bankRepository)
    {
        _mapper = mapper;
        _bankSchemeRepository = bankSchemeRepository;
        _bankRepository = bankRepository;
    }

    public async Task<ErrorOr<CreateBankSchemeCommandResponse>> Handle(
        CreateBankSchemeCommand request, CancellationToken cancellationToken)
    {
        var bank = (await _bankRepository.FindByNameAsync(request.BankName))!;
        var scheme = BankScheme.Create(bank, request.FileType);

        await _bankSchemeRepository.CreateAsync(scheme);

        return new CreateBankSchemeCommandResponse(scheme.Id);
    }
}