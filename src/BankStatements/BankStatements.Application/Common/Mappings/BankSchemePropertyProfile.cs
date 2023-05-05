using AutoMapper;
using BankStatements.Application.Common.Dto;
using BankStatements.Domain.BankAggregate;

namespace BankStatements.Application.Common.Mappings;

public class BankSchemePropertyProfile : Profile
{
    public BankSchemePropertyProfile()
    {
        CreateMap<BankSchemeProperty, BankSchemePropertyDto>();
    }
}