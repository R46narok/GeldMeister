using BankStatements.Application.Common.Repositories;
using BankStatements.Domain.BankAggregate;
using BankStatements.Domain.BankAggregate.Enums;
using ErrorOr;
using MediatR;

namespace BankStatements.Application.BankSchemeProperties.Commands.Create;

public record CreateBankSchemePropertyCommandResponse(Guid Id);


public record CreateBankSchemePropertyCommand(Guid SchemeId, string Name, DataType Type)
    : IRequest<ErrorOr<CreateBankSchemePropertyCommandResponse>>;

public class CreateBankSchemePropertyCommandHandler 
    : IRequestHandler<CreateBankSchemePropertyCommand, ErrorOr<CreateBankSchemePropertyCommandResponse>>
{
    private readonly IBankSchemeRepository _schemeRepository;
    private readonly IBankSchemePropertyRepository _propertyRepository;

    public CreateBankSchemePropertyCommandHandler(IBankSchemeRepository schemeRepository, IBankSchemePropertyRepository propertyRepository)
    {
        _schemeRepository = schemeRepository;
        _propertyRepository = propertyRepository;
    }

    public async Task<ErrorOr<CreateBankSchemePropertyCommandResponse>> Handle(CreateBankSchemePropertyCommand request,
        CancellationToken cancellationToken)
    {
        var scheme = await _schemeRepository.GetByIdAsync(request.SchemeId, track: true);
        var property = BankSchemeProperty.Create(scheme!, request.Name, request.Type);

        var key = await _propertyRepository.CreateAsync(property);

        return new CreateBankSchemePropertyCommandResponse(key);
    }
}