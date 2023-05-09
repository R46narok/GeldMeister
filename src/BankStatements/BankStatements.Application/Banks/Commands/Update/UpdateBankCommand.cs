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
    private readonly IDynamicTransactionRepository _transactionRepository;

    public UpdateBankCommandHandler(IBankRepository repository, IDynamicTransactionRepository transactionRepository)
    {
        _repository = repository;
        _transactionRepository = transactionRepository;
    }
    
    public async Task<ErrorOr<UpdateBankCommandResponse>> Handle(UpdateBankCommand request,
        CancellationToken cancellationToken)
    {
        var bank = await _repository.GetByIdAsync(request.Id);
        var oldName = bank!.Name;
        var newName = request.Name ?? bank.Name;
        
        bank.ChangeName(newName);

        await _repository.UpdateAsync(bank);
        await _transactionRepository.RenameTransactionType(oldName, newName);
        
        return new UpdateBankCommandResponse(request.Id);
    }
}