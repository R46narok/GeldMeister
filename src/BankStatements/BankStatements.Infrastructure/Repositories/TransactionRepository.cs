using System.Data;
using System.Text;
using BankStatements.Application.Common.Builders;
using BankStatements.Application.Common.Interfaces;
using BankStatements.Domain.BankAggregate;
using BankStatements.Domain.BankAggregate.Enums;
using BankStatements.Infrastructure.Helpers;
using BankStatements.Infrastructure.Persistence;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace BankStatements.Infrastructure.Repositories;

public class DynamicTransactionRepository : IDynamicTransactionRepository
{
    private readonly ITransactionQueryBuilder _builder;
    private readonly IDbConnection _connection;

    public DynamicTransactionRepository(ISqlConnectionFactory connectionFactory, ITransactionQueryBuilder builder)
    {
        _builder = builder;
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
            Name = $"{name}"
        };

        return await _connection.QuerySingleOrDefaultAsync<int>(query, parameters) > 0;
    }


    public async Task CreateTransactionType(string name, BankScheme scheme)
    {
        var properties = scheme.Properties;

        var table = _builder
            .BeginTableDefinition(name)
            .DefineColumn("StatementId", DataType.UniqueIdentifier, notNull: true);

        foreach (var prop in properties)
            table.DefineColumn(prop.Name.ToTitleCaseTrimmed(), prop.Type, notNull: true);

        table.AddForeignKeyConstraint("BankStatements", "StatementId");
        table.EndTableDefinition();
        
        await _connection.QueryAsync(_builder.Build());
    }

    public async Task RenameTransactionType(string oldName, string newName)
    {
        var query = $@"EXEC sp_rename '{oldName}', '{newName}'";
        await _connection.QueryAsync(query);
    }

    public async Task CreateTransaction(string name, BankScheme scheme, Guid statementId, IDictionary<string, object> parameters)
    {
        var parametersTitleCase = new Dictionary<string, object>();
        foreach (var pair in parameters)
            parametersTitleCase.Add(pair.Key.ToTitleCaseTrimmed(), pair.Value);
        
        var properties = scheme.Properties;
        var queryBuilder = new StringBuilder();

        queryBuilder.Append($@"INSERT INTO [{name}](");
        foreach (var prop in properties)
            queryBuilder.Append($"[{prop.Name.ToTitleCaseTrimmed()}],");
        queryBuilder.Append("[StatementId])");

        queryBuilder.Append(@$"VALUES (");
        foreach (var prop in properties)
            queryBuilder.Append($@"@{prop.Name.ToTitleCaseTrimmed()},");

        queryBuilder.Append(@$"'{statementId}')");
        
        await _connection.QueryAsync(queryBuilder.ToString(), parametersTitleCase);
    }

    public async Task<List<dynamic>> GetTransactionsByStatementId(string name, Guid statementId)
    { 
        string query = @$"SELECT * FROM [{name}] WHERE StatementId = '{statementId}'";

        return (await _connection.QueryAsync(query)).ToList();
    }

    public async Task<List<dynamic>> GetTransactionsByStatementIdWithPagination(string name, Guid statementId, int pageIndex, int pageSize)
    {
        var query = $@"SELECT * FROM [{name}] 
                          WHERE StatementId = @StatementId
                          ORDER BY Id
                          OFFSET @Offset ROWS
                          FETCH NEXT @PageSize ROWS ONLY";
        
        var parameters = new
        {
            Name = name, 
            StatementId = statementId,
            Offset = pageIndex * pageSize,
            PageSize = pageSize,
        };

        return (await _connection.QueryAsync(query, parameters)).ToList();
    }
    
    
}