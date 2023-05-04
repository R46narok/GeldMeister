using System.Security.Claims;
using AutoMapper;
using ErrorOr;
using GeldMeister.Common.Application.MessageBrokers;
using GeldMeister.Users.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GeldMeister.Users.Commands.Users.CreateUser;

public record CreateUserCommandResponse(string Id);

public class CreateUserCommand : IRequest<ErrorOr<CreateUserCommandResponse>>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public CreateUserCommand(string userName, string email, string password)
    {
        UserName = userName;
        Email = email;
        Password = password;
    }

    public CreateUserCommand() { }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<CreateUserCommandResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IMessagePublisher _publisher;

    public CreateUserCommandHandler(IMapper mapper, IMessagePublisher publisher, UserManager<User> userManager)
    {
        _mapper = mapper;
        _publisher = publisher;
        _userManager = userManager;
    }
    
    public async Task<ErrorOr<CreateUserCommandResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        
        user.CreatedOn = DateTime.Now;
        user.UpdatedOn = DateTime.Now;

        var identityResult = await _userManager.CreateAsync(user, request.Password);

        if (identityResult.Succeeded)
        {
            await _userManager.AddClaimsAsync(user, new []
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, "User")
            });

            var @event = _mapper.Map<UserCreatedEvent>(user);
            await _publisher.PublishTopicAsync(@event, MessageMetadata.Now(), cancellationToken);

            var saved = await _userManager.FindByNameAsync(request.UserName);
            return new CreateUserCommandResponse(saved.Id);
        }

        var errors = identityResult.Errors
            .Select(x => Error.Failure(x.Code, x.Description))
            .ToList();
        return ErrorOr<CreateUserCommandResponse>.From(errors);
    }
}
