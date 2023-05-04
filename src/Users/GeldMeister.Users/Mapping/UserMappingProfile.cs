using AutoMapper;
using GeldMeister.Users.Commands.Users.CreateUser;
using GeldMeister.Users.Commands.Users.DeleteUser;
using GeldMeister.Users.Data.Entities;
using GeldMeister.Users.Dto;
using GeldMeister.Users.Commands.Users;

namespace GeldMeister.Users.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserCredentialsDto, User>();
        CreateMap<UserCredentialsDto, CreateUserCommand>();

        CreateMap<User, UserDto>();
        // CreateMap<User, UserView>();
        CreateMap<CreateUserCommand, User>();

        CreateMap<User, UserCreatedEvent>();
        CreateMap<User, UserDeletedEvent>();
        // CreateMap<UpdateUserDto, UpdateUserCommand>();
        // CreateMap<UpdateUserCommand, User>();
    }
}