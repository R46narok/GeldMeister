using BankStatements.Application.Common.Repositories;
using BankStatements.Domain.BankAggregate;
using BankStatements.Infrastructure.Persistence;
using GeldMeister.Common.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankStatements.Infrastructure.Repositories;

public class BankRepository : RepositoryBase<Bank, Guid, BankStatementsDbContext>, IBankRepository
{
    public BankRepository(BankStatementsDbContext context) : base(context)
    {
    }

    public async Task<Bank?> FindByNameAsync(string name, bool track, bool includeScheme)
    {
        var queryable = Context
            .Set<Bank>()
            .AsQueryable();

        queryable = track ? queryable.AsTracking() : queryable.AsNoTracking();

        if (includeScheme)
            queryable = queryable.Include(x => x.Scheme);

        return await queryable.SingleOrDefaultAsync(x => x.Name == name);
    }
}