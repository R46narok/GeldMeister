using BankStatements.Domain.BankAggregate.Enums;

namespace BankStatements.Application.Common.Builders;

public interface ITransactionDefineTable
{
    public ITransactionDefineTable DefineColumn(string name, DataType type, bool notNull);
    public ITransactionDefineTable AddForeignKeyConstraint(string table, string column);
    public ITransactionQueryBuilder EndTableDefinition();
}


public interface ITransactionQueryBuilder : ITransactionDefineTable
{
    public string Build();
    public ITransactionDefineTable BeginTableDefinition(string name);
}
