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

    public override List<Bank> GetAll()
    {
        return Context
            .Set<Bank>()
            .Include(x => x.Scheme)
            .ThenInclude(x => x.Properties)
            .ToList();
    }

    public async Task<Bank?> FindByNameAsync(string name, bool track, bool includeScheme, bool includeSchemeProperties)
    {
        var queryable = Context
            .Set<Bank>()
            .AsQueryable();

        queryable = includeScheme switch
        {
            true when includeSchemeProperties => queryable
                .Include(x => x.Scheme)
                .ThenInclude(x => x.Properties),
            true => queryable
                .Include(x => x.Scheme),
            _ => track ? queryable.AsTracking() : queryable.AsNoTracking()
        };

        return await queryable.SingleOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Bank?> GetByIdAsync(Guid id, bool track, bool includeScheme, bool includeSchemeProperties)
    {
        var queryable = Context
            .Set<Bank>()
            .AsQueryable();

        queryable = includeScheme switch
        {
            true when includeSchemeProperties => queryable
                .Include(x => x.Scheme)
                .ThenInclude(x => x.Properties),
            true => queryable
                .Include(x => x.Scheme),
            _ => track ? queryable.AsTracking() : queryable.AsNoTracking()
        };

        return await queryable.SingleOrDefaultAsync(x => x.Id == id);
    }
}