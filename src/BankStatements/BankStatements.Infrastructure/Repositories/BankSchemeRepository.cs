using BankStatements.Application.Common.Repositories;
using BankStatements.Domain.BankAggregate;
using BankStatements.Infrastructure.Persistence;
using GeldMeister.Common.Application.Interfaces;

namespace BankStatements.Infrastructure.Repositories;

public class BankSchemeRepository : RepositoryBase<BankScheme, Guid, BankStatementsDbContext>, IBankSchemeRepository
{
    public BankSchemeRepository(BankStatementsDbContext context) : base(context)
    {
    }
}