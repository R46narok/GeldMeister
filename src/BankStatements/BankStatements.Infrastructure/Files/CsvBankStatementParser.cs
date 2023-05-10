using System.Globalization;
using BankStatements.Application.Common.Interfaces;
using BankStatements.Domain.BankAggregate;
using CsvHelper;

namespace BankStatements.Infrastructure.Files;

public class CsvBankStatementParser : IBankStatementParser
{
    public async Task<List<dynamic>> Parse(StreamReader stream, BankScheme scheme)
    {
        var properties = scheme
            .Properties
            .ToList();
        using var csv = new CsvReader(stream, CultureInfo.InvariantCulture);

        // await csv.ReadAsync(); // Header
        var result = new List<dynamic>();
        while (await csv.ReadAsync())
        {
            var record = csv.GetRecord<dynamic>()!;
            result.Add(record);
        }

        return result;
    }
}