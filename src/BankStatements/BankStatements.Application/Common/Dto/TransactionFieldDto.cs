using BankStatements.Domain.BankAggregate;

namespace BankStatements.Application.Common.Dto;

public class TransactionFieldDto
{
    public BankSchemePropertyDto Property { get; private init; } 
    public byte[] Value { get; private set; } 
}