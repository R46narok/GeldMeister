﻿using BankStatements.Domain.BankAggregate;
using GeldMeister.Common.Application.Interfaces;

namespace BankStatements.Application.Common.Repositories;

public interface IBankStatementRepository : IRepository<BankStatement, Guid>
{
    public Task<BankStatement?> GetByIdAsync(Guid id, bool track, bool includeBank);
}