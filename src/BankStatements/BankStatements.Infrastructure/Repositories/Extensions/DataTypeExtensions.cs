using BankStatements.Domain.BankAggregate.Enums;

namespace BankStatements.Infrastructure.Repositories.Extensions;

public static class DataTypeExtensions
{
    public static string ToMssqlType(this DataType type)
    {
        return type switch
        {
            DataType.Int => "INT",
            DataType.Decimal => "DECIMAL(18,2)",
            DataType.Double => "FLOAT",
            DataType.DateTime => "DATETIME2",
            DataType.String => "NVARCHAR(MAX)",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}