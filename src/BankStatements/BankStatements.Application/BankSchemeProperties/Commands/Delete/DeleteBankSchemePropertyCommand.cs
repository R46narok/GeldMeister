using BankStatements.Application.Common.Repositories;
using ErrorOr;
using MediatR;

namespace BankStatements.Application.BankSchemeProperties.Commands.Delete;

public record DeleteBankSchemePropertyCommandResponse(Guid Id);
public record DeleteBankSchemePropertyCommand(Guid Id)
    : IRequest<ErrorOr<DeleteBankSchemePropertyCommandResponse>>;

public class DeleteBankSchemePropertyCommandHandler 
    : IRequestHandler<DeleteBankSchemePropertyCommand, ErrorOr<DeleteBankSchemePropertyCommandResponse>>
{
    private readonly IBankSchemePropertyRepository _repository;

    public DeleteBankSchemePropertyCommandHandler(IBankSchemePropertyRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<DeleteBankSchemePropertyCommandResponse>> Handle(DeleteBankSchemePropertyCommand request, CancellationToken cancellationToken)
    {
        var property = await _repository.GetByIdAsync(request.Id);

        await _repository.DeleteAsync(property);

        return new DeleteBankSchemePropertyCommandResponse(request.Id);
    }
}