using BankStatements.Application.Common.Repositories;
using BankStatements.Domain.BankAggregate;
using BankStatements.Infrastructure.Persistence;
using GeldMeister.Common.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankStatements.Infrastructure.Repositories;

public class BankSchemeRepository : RepositoryBase<BankScheme, Guid, BankStatementsDbContext>, IBankSchemeRepository
{
    public BankSchemeRepository(BankStatementsDbContext context) : base(context)
    {
    }

    public async Task<BankScheme?> FindByBankId(Guid id, bool includeProperties, bool includeBank)
    {
        var queryable = Context
            .Set<BankScheme>()
            .AsQueryable();

        if (includeProperties)
            queryable = queryable.Include(x => x.Properties);
        
        if (includeBank)
            queryable = queryable.Include(x => x.Bank);
        
        return await queryable.FirstOrDefaultAsync(x => x.BankId == id);
    }
}