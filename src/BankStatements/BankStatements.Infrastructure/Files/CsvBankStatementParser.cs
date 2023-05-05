using System.Globalization;
using BankStatements.Application.Common.Interfaces;
using BankStatements.Domain.BankAggregate;
using CsvHelper;

namespace BankStatements.Infrastructure.Files;

public class CsvBankStatementParser : IBankStatementParser
{
    public CsvBankStatementParser()
    {
    }

    public async Task Parse(StreamReader stream, BankScheme scheme)
    {
        var properties = scheme.Properties;
        using var csv = new CsvReader(stream, CultureInfo.InvariantCulture);

        csv.ReadHeader();
        
        
    }
}