using BankStatements.Domain.BankAggregate;
using GeldMeister.Common.Application.Interfaces;

namespace BankStatements.Application.Common.Interfaces;

public interface ITransactionFieldRepository : IRepository<TransactionField, Guid>
{
    
}