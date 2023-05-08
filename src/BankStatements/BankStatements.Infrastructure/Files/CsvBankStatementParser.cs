using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using BankStatements.Application.Common.Interfaces;
using BankStatements.Domain.BankAggregate;
using BankStatements.Domain.BankAggregate.Enums;
using CsvHelper;
using CsvHelper.Configuration;

namespace BankStatements.Infrastructure.Files;

public class CsvBankStatementParser : IBankStatementParser
{
    public CsvBankStatementParser()
    {
    }

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

    private static Type DataTypeToNet(DataType dataType)
    {
        return dataType switch
        {
            // DataType.Int => typeof(int),
            // DataType.Decimal => typeof(decimal),
            // DataType.Double => typeof(double),
            // DataType.DateTime => typeof(DateTime),
            // DataType.String => typeof(string),
            // _ => throw new ArgumentOutOfRangeException(nameof(dataType), dataType, null)
            _ => typeof(string)
        };
    }
}