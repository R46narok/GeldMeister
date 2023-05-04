using AutoMapper;
using BankStatements.Application.Banks.Commands.Create;
using BankStatements.Domain.BankAggregate;

namespace BankStatements.Application.Common.Mappings;

public class BankProfile : Profile
{
    public BankProfile()
    {
        CreateMap<CreateBankCommand, Bank>()
            .ConvertUsing(b => Bank.Create(b.Name)!);
    }
}