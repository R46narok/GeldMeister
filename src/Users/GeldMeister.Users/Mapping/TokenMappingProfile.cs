using AutoMapper;
using GeldMeister.Users.Commands.Token.CreateToken;
using GeldMeister.Users.Dto;

namespace GeldMeister.Users.Mapping;

public class TokenMappingProfile : Profile
{
    public TokenMappingProfile()
    {
        CreateMap<UserCredentialsDto, CreateTokenCommand>();
    }
}