using BankStatements.Domain.BankAggregate.Enums;

namespace BankStatements.Application.Common.Dto;

public class BankSchemeDto
{
    public Guid Id { get; set; }
    public FileType FileType { get; set; }
}