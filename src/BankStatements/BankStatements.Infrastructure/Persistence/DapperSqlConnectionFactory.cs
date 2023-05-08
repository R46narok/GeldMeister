using System.Data;
using BankStatements.Application.Common.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BankStatements.Infrastructure.Persistence;

public class DapperSqlConnectionFactory : ISqlConnectionFactory
{
    private readonly IConfiguration _configuration;

    public DapperSqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IDbConnection Create()
    {
        var connection = _configuration.GetConnectionString("Default");
        return new SqlConnection(connection);
    }
}