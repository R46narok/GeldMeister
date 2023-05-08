using BankStatements.Application.Common.Repositories;
using BankStatements.Domain.BankAggregate;
using BankStatements.Infrastructure.Persistence;
using GeldMeister.Common.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankStatements.Infrastructure.Repositories;

public class BankStatementRepository :
    RepositoryBase<BankStatement, Guid, BankStatementsDbContext>,
    IBankStatementRepository
{
    public BankStatementRepository(BankStatementsDbContext context) : base(context)
    {
    }

    public async Task<BankStatement?> GetByIdAsync(Guid id, bool track, bool includeBank)
    {
        var queryable = Context
            .Set<BankStatement>()
            .AsQueryable()
            .AsNoTracking();

        if (includeBank)
            queryable = queryable
                .Include(x => x.Bank)
                .ThenInclude(x => x.Scheme)
                .ThenInclude(x => x.Properties);

        return await queryable.FirstOrDefaultAsync(x => x.Id == id);
    }
}