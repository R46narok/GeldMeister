using BankStatements.Application.Common.Interfaces;
using BankStatements.Domain.BankAggregate.Enums;

namespace BankStatements.Infrastructure.Files;

public class BankStatementParserFactory : IBankStatementParserFactory
{
    public IBankStatementParser Create(FileType type)
    {
        return type switch
        {
            FileType.Csv => new CsvBankStatementParser(),
            // FileType.None => expr,
            // FileType.Json => expr,
            // FileType.Xml => expr,
            _ => throw new ArgumentException()
        };
    }
}