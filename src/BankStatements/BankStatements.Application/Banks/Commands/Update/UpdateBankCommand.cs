using BankStatements.Application.Common.Interfaces;
using BankStatements.Application.Common.Repositories;
using ErrorOr;
using MediatR;

namespace BankStatements.Application.Banks.Commands.Update;

public record UpdateBankCommandResponse(Guid Id);

public record UpdateBankCommand(Guid Id, string Name) : IRequest<ErrorOr<UpdateBankCommandResponse>>;

public class UpdateBankCommandHandler : IRequestHandler<UpdateBankCommand, ErrorOr<UpdateBankCommandResponse>>
{
    private readonly IBankRepository _repository;

    public UpdateBankCommandHandler(IBankRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ErrorOr<UpdateBankCommandResponse>> Handle(UpdateBankCommand request,
        CancellationToken cancellationToken)
    {
        var bank = await _repository.GetByIdAsync(request.Id);
        var oldName = bank!.Name;
        var newName = request.Name ?? bank.Name;
        
        bank.ChangeName(newName);

        await _repository.UpdateAsync(bank);
        
        return new UpdateBankCommandResponse(request.Id);
    }
}