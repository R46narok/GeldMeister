using System.Text;
using BankStatements.Application.Common.Interfaces;
using BankStatements.Domain.BankAggregate.Enums;

namespace BankStatements.Infrastructure.Memory;

public class BinarySerializer : IBinarySerializer
{
    public byte[] Serialize(int value)
    {
        return BitConverter.GetBytes(value);
    }

    public byte[] Serialize(long value)
    {
        return BitConverter.GetBytes(value);
    }

    public byte[] Serialize(decimal value)
    {
        var decimalBits = decimal.GetBits(value);
        var decimalBytes = new byte[decimalBits.Length * sizeof(int)];
        Buffer.BlockCopy(decimalBits, 0, decimalBytes, 0, decimalBytes.Length);

        return decimalBytes;
    }

    public byte[] Serialize(double value)
    {
        return BitConverter.GetBytes(value);
    }

    public byte[] Serialize(DateTime value)
    {
        return Serialize(value.ToBinary());
    }

    public byte[] Serialize(string value)
    {
        return Encoding.UTF8.GetBytes(value);
    }

    public byte[] Serialize(object obj, DataType type)
    {
        var str = (string) obj;

        return type switch
        {
            DataType.Int => Serialize(int.Parse(str)),
            DataType.Decimal => Serialize(decimal.Parse(str)),
            DataType.Double => Serialize(double.Parse(str)),
            DataType.DateTime => Serialize(DateTime.Parse(str)),
            DataType.String => Serialize(str),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}