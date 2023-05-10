using BankStatements.Domain.BankAggregate.Enums;

namespace BankStatements.Application.Common.Interfaces;

public interface IBinarySerializer
{
    public byte[] Serialize(int value);
    public byte[] Serialize(long value);
    public byte[] Serialize(decimal value);
    public byte[] Serialize(double value);
    public byte[] Serialize(DateTime value);
    public byte[] Serialize(string value);

    public byte[] Serialize(object obj, DataType type);
}