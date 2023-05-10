
namespace BankStatements.Application.Common.Dto;

public class BankStatementDto
{
    public BankDto Bank { get; set; }
    public List<TransactionDto> Transactions { get; set; }
}