using BankStatements.Domain.BankAggregate.Enums;

namespace BankStatements.Application.Common.Interfaces;

public interface IBankStatementParserFactory
{
    public IBankStatementParser Create(FileType type);
}