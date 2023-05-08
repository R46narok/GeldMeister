using AutoMapper;
using BankStatements.Application.Common.Dto;
using BankStatements.Domain.BankAggregate;

namespace BankStatements.Application.Common.Mappings;

public class BankStatementProfile : Profile
{
    public BankStatementProfile()
    {
        CreateMap<BankStatement, BankStatementDto>();
    }
}