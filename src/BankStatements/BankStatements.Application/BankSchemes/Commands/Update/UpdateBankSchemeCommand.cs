using BankStatements.Application.Common.Repositories;
using BankStatements.Domain.BankAggregate.Enums;
using ErrorOr;
using MediatR;

namespace BankStatements.Application.BankSchemes.Commands.Update;

public record UpdateBankSchemeCommandResponse(Guid Id);

public record UpdateBankSchemeCommand(Guid Id, FileType? FileType)
    : IRequest<ErrorOr<UpdateBankSchemeCommandResponse>>;

public class
    UpdateBankSchemeCommandHandler : IRequestHandler<UpdateBankSchemeCommand, ErrorOr<UpdateBankSchemeCommandResponse>>
{
    private readonly IBankSchemeRepository _repository;

    public UpdateBankSchemeCommandHandler(IBankSchemeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<UpdateBankSchemeCommandResponse>> Handle(UpdateBankSchemeCommand request,
        CancellationToken cancellationToken)
    {
        var scheme = await _repository.GetByIdAsync(request.Id);
        scheme!.ChangeFileType(request.FileType ?? scheme.FileType);

        await _repository.UpdateAsync(scheme);

        return new UpdateBankSchemeCommandResponse(request.Id);
    }
}