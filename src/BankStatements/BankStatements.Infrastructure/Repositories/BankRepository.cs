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

    public async Task<Bank?> FindByNameAsync(string name, bool track)
    {
        if (track)
        {
            return await Context
                .Set<Bank>()
                .AsTracking()
                .SingleOrDefaultAsync(x => x.Name == name);
        }

        return await Context
            .Set<Bank>()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Name == name);
    }
}