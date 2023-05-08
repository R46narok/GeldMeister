using BankStatements.Application.Common.Repositories;
using ErrorOr;
using MediatR;

namespace BankStatements.Application.BankSchemes.Commands.Delete;

public record DeleteBankSchemeCommandResponse(Guid Id);
public record DeleteBankSchemeCommand(Guid Id) : IRequest<ErrorOr<DeleteBankSchemeCommandResponse>>;

public class DeleteBankSchemeCommandHandler : IRequestHandler<DeleteBankSchemeCommand,
    ErrorOr<DeleteBankSchemeCommandResponse>>
{
    private readonly IBankSchemeRepository _repository;

    public DeleteBankSchemeCommandHandler(IBankSchemeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<DeleteBankSchemeCommandResponse>> Handle(DeleteBankSchemeCommand request, CancellationToken cancellationToken)
    {
        var scheme = await _repository.GetByIdAsync(request.Id);
        await _repository.DeleteAsync(scheme!);

        return new DeleteBankSchemeCommandResponse(request.Id);
    }
}