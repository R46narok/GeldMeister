using System.Data;
using System.Text;
using BankStatements.Application.Common.Interfaces;
using BankStatements.Domain.BankAggregate;
using BankStatements.Infrastructure.Helpers;
using BankStatements.Infrastructure.Persistence;
using BankStatements.Infrastructure.Repositories.Extensions;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace BankStatements.Infrastructure.Repositories;

public class DynamicTransactionRepository : IDynamicTransactionRepository
{
    private readonly IDbConnection _connection;

    public DynamicTransactionRepository(ISqlConnectionFactory connectionFactory)
    {
        _connection = connectionFactory.Create();
    }

    public async Task<bool> IsTransactionTypeCreated(string name)
    {
        const string query = $@"
            SELECT COUNT(*)
            FROM INFORMATION_SCHEMA.TABLES
            WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = @Name";

        var parameters = new
        {
            Name = $"{name}TransactionType"
        };

        return await _connection.QuerySingleOrDefaultAsync<int>(query, parameters) > 0;
    }


    public async Task CreateTransactionType(string name, BankScheme scheme)
    {
        var properties = scheme.Properties;

        var queryBuilder = new StringBuilder();
        queryBuilder.Append(@$"CREATE TABLE [{name}TransactionType](
                              Id INT PRIMARY KEY IDENTITY(1,1),
                              StatementId uniqueidentifier NOT NULL,
                              CONSTRAINT [FK_@Name_BankStatements] FOREIGN KEY (StatementId) REFERENCES BankStatements(Id),");

        foreach (var prop in properties)
        {
            queryBuilder.Append(@$"{prop.Name.ToTitleCaseTrimmed()} {prop.Type.ToMssqlType()} NOT NULL,");
        }

        queryBuilder.Remove(queryBuilder.Length - 1, 1);
        queryBuilder.Append(")");

        await _connection.QueryAsync(queryBuilder.ToString());
    }

    public async Task CreateTransaction(string name, BankScheme scheme, Guid statementId, Dictionary<string, object> parameters)
    {
        var parametersTitleCase = new Dictionary<string, object>();
        foreach (var pair in parameters)
            parametersTitleCase.Add(pair.Key.ToTitleCaseTrimmed(), pair.Value);
        
        var properties = scheme.Properties;
        var queryBuilder = new StringBuilder();

        queryBuilder.Append($@"INSERT INTO [{name}TransactionType](");
        foreach (var prop in properties)
            queryBuilder.Append($"[{prop.Name.ToTitleCaseTrimmed()}],");
        queryBuilder.Append("[StatementId])");

        queryBuilder.Append(@$"VALUES (");
        foreach (var prop in properties)
            queryBuilder.Append($@"@{prop.Name.ToTitleCaseTrimmed()},");

        queryBuilder.Append(@$"'{statementId}')");
        
        await _connection.QueryAsync(queryBuilder.ToString(), parametersTitleCase);
    }
}