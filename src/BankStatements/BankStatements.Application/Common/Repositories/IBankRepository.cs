using BankStatements.Domain.BankAggregate;
using GeldMeister.Common.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankStatements.Application.Common.Repositories;

public interface IBankRepository : IRepository<Bank, Guid>
{
    public Task<Bank?> FindByNameAsync(string name, 
        bool track = true,
        bool includeScheme = false,
        bool includeSchemeProperties = false);
}
