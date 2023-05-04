namespace BankStatements.Application.Common.Dto;

public class BankDto
{
     public Guid Id { get; set; }
     public string Name { get;  set; }
     public BankSchemeDto? Scheme { get;  set; }
}