using BankStatements.Application.BankSchemes.Commands.Update;
using BankStatements.Application.Common.Repositories;
using BankStatements.Domain.BankAggregate.Enums;
using ErrorOr;
using MediatR;

namespace BankStatements.Application.BankSchemeProperties.Commands.Update;

public record UpdateBankSchemePropertyCommandResponse(Guid Id);
public record UpdateBankSchemePropertyCommand(Guid Id, string? Name, DataType? DataType)
    : IRequest<ErrorOr<UpdateBankSchemePropertyCommandResponse>>;

public class UpdateBankSchemePropertyCommandHandler 
    : IRequestHandler<UpdateBankSchemePropertyCommand, ErrorOr<UpdateBankSchemePropertyCommandResponse>>
{
    private readonly IBankSchemePropertyRepository _repository;

    public UpdateBankSchemePropertyCommandHandler(IBankSchemePropertyRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<UpdateBankSchemePropertyCommandResponse>> Handle(UpdateBankSchemePropertyCommand request, CancellationToken cancellationToken)
    {
        var property = await _repository.GetByIdAsync(request.Id);
        
        property!.ChangeName(request.Name ?? property.Name);
        property!.ChangeType(request.DataType ?? property.Type);

        await _repository.UpdateAsync(property);

        return new UpdateBankSchemePropertyCommandResponse(request.Id);
    }
}