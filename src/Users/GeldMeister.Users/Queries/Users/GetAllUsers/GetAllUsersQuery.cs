using AutoMapper;
using ErrorOr;
using GeldMeister.Users.Data.Entities;
using GeldMeister.Users.Dto;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GeldMeister.Users.Queries.Users.GetAllUsers;

public record GetAllUsersQuery : IRequest<ErrorOr<List<UserDto>>>;

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ErrorOr<List<UserDto>>>
    {
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(IMapper mapper, UserManager<User> userManager)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = _userManager.Users.ToList();
        var dtos = users
            .Select(x => _mapper.Map<UserDto>(x))
            .ToList();

        return dtos;
    }
}

