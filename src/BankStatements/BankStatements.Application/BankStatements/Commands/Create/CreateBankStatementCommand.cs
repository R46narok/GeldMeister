using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BankStatements.Application.BankStatements.Commands.Create;

public record CreateBankStatementCommandResponse(Guid Id);
public record CreateBankStatementCommand(Guid BankId, IFormFile File) 
    : IRequest<ErrorOr<CreateBankStatementCommandResponse>>;

public class CreateBankStatementCommandHandler 
    : IRequestHandler<CreateBankStatementCommand, ErrorOr<CreateBankStatementCommandResponse>>
{
    public async Task<ErrorOr<CreateBankStatementCommandResponse>> Handle(CreateBankStatementCommand request, CancellationToken cancellationToken)
    {
        return new CreateBankStatementCommandResponse(Guid.Empty);
    }
}