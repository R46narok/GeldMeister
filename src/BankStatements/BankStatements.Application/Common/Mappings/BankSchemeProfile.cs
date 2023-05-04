using AutoMapper;
using BankStatements.Application.BankSchemes.Commands.Create;
using BankStatements.Application.Common.Dto;
using BankStatements.Domain.BankAggregate;

namespace BankStatements.Application.Common.Mappings;

public class BankSchemeProfile : Profile
{
    public BankSchemeProfile()
    {
        CreateMap<BankScheme, BankSchemeDto>();
    }
}