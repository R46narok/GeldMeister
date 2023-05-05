using BankStatements.Application.Common.Repositories;
using BankStatements.Domain.BankAggregate;
using BankStatements.Infrastructure.Persistence;
using GeldMeister.Common.Application.Interfaces;

namespace BankStatements.Infrastructure.Repositories;

public class BankStatementRepository : 
    RepositoryBase<BankStatement, Guid, BankStatementsDbContext>,
    IBankStatementRepository
{
    public BankStatementRepository(BankStatementsDbContext context) : base(context)
    {
    }
}