using BankStatements.Domain.BankAggregate.Enums;

namespace BankStatements.Application.Common.Dto;

public class BankSchemePropertyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DataType Type { get; set; }
}