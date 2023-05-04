using AutoMapper;
using BankStatements.Application.Banks.Commands.Create;
using BankStatements.Application.Common.Dto;
using BankStatements.Domain.BankAggregate;

namespace BankStatements.Application.Common.Mappings;

public class BankProfile : Profile
{
    public BankProfile()
    {
        CreateMap<CreateBankCommand, Bank>()
            .ConvertUsing(b => Bank.Create(b.Name)!);

        CreateMap<Bank, BankDto>()
            .ForMember(x => x.Scheme, opt => opt.MapFrom(x => x.Scheme));
    }
}