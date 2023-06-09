﻿using BankStatements.Application.Common.Interfaces;
using BankStatements.Domain.BankAggregate;
using BankStatements.Infrastructure.Persistence;
using GeldMeister.Common.Application.Interfaces;

namespace BankStatements.Infrastructure.Repositories;

public class TransactionRepository 
    : RepositoryBase<Transaction, Guid, BankStatementsDbContext>, ITransactionRepository
{
    public TransactionRepository(BankStatementsDbContext context) : base(context)
    {
    }
}