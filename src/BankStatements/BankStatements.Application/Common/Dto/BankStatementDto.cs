using BankStatements.Domain.UserAggregate;

namespace BankStatements.Application.Common.Dto;

public class BankStatementDto
{
    public BankDto Bank { get; set; }
    public UserDto User { get; set; }
    public List<dynamic> Transactions { get; set; }
}