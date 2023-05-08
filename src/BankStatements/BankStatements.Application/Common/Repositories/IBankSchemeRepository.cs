using BankStatements.Domain.BankAggregate;
using GeldMeister.Common.Application.Interfaces;

namespace BankStatements.Application.Common.Repositories;

public interface IBankSchemeRepository : IRepository<BankScheme, Guid>
{
    public Task<BankScheme?> FindByBankId(Guid id, bool includeProperties, bool includeBank);
}