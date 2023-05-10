using AutoMapper;
using BankStatements.Application.Common.Dto;
using BankStatements.Domain.BankAggregate;

namespace BankStatements.Application.Common.Mappings;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<Transaction, TransactionDto>();
        CreateMap<TransactionField, TransactionFieldDto>();
    }
}