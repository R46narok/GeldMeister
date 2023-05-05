using BankStatements.Application.Common.Repositories;
using BankStatements.Domain.BankAggregate;
using BankStatements.Infrastructure.Persistence;
using GeldMeister.Common.Application.Interfaces;

namespace BankStatements.Infrastructure.Repositories;

public class BankSchemePropertyRepository : 
    RepositoryBase<BankSchemeProperty, Guid, BankStatementsDbContext>, IBankSchemePropertyRepository
{
    public BankSchemePropertyRepository(BankStatementsDbContext context) : base(context)
    {
    }
}