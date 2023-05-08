using BankStatements.Application.Common.Dto;
using BankStatements.Application.Common.Repositories;
using BankStatements.Domain.BankAggregate;
using ErrorOr;
using MediatR;

namespace BankStatements.Application.Banks.Commands.Delete;

public record DeleteBankCommandResponse(Guid Id);
public record DeleteBankCommand(Guid Id) : IRequest<ErrorOr<DeleteBankCommandResponse>>;

public class DeleteBankCommandHandler : IRequestHandler<DeleteBankCommand, ErrorOr<DeleteBankCommandResponse>>
{
    private readonly IBankRepository _repository;

    public DeleteBankCommandHandler(IBankRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<DeleteBankCommandResponse>> Handle(DeleteBankCommand request, CancellationToken cancellationToken)
    {
        var bank = await _repository.GetByIdAsync(request.Id);
        await _repository.DeleteAsync(bank!);

        return new DeleteBankCommandResponse(request.Id);
    }
}
