using System.Data;

namespace BankStatements.Application.Common.Interfaces;

public interface ISqlConnectionFactory
{
    public IDbConnection Create();
}