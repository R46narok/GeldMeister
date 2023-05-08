using System.Text;
using BankStatements.Application.Common.Builders;
using BankStatements.Application.Common.Interfaces;
using BankStatements.Domain.BankAggregate.Enums;
using BankStatements.Infrastructure.Persistence.Extensions;

namespace BankStatements.Infrastructure.Persistence.Builders;


public class MssqlTransactionQueryBuilder : ITransactionQueryBuilder
{
    private StringBuilder _builder = new();
    private string _tableName;

    public ITransactionDefineTable DefineColumn(string name, DataType type, bool notNull)
    {
        var nullable = notNull ? "NOT NULL" : "";
        var query = @$"{name} {type.ToMssqlType()} {nullable},";
        _builder.Append(query);
        return this;
    }

    public ITransactionDefineTable AddForeignKeyConstraint(string table, string column)
    {
        var query = @$"CONSTRAINT [FK_{_tableName}_{table}] FOREIGN KEY ({column}) REFERENCES {table}(Id),";
        _builder.Append(query);
        return this;
    }

    public ITransactionDefineTable BeginTableDefinition(string name)
    {
        _builder = new();
        var definition = $@"CREATE TABLE [{name}](
                           Id INT PRIMARY KEY IDENTITY(1,1),";

        _tableName = name;

        _builder.Append(definition);

        return this;
    }

    public ITransactionQueryBuilder EndTableDefinition()
    {
        _builder.Remove(_builder.Length - 1, 1);
        _builder.Append(")");

        return this;
    }

    public string Build()
    {
        return _builder.ToString();
    }
}