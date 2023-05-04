using AutoMapper;
using ErrorOr;
using GeldMeister.Common.Application.MessageBrokers;
using GeldMeister.Users.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using V9.Application;

namespace GeldMeister.Users.Commands.Users.DeleteUser;

public record DeleteUserCommandResponse(string UserName);

public class DeleteUserCommand : IRequest<ErrorOr<DeleteUserCommandResponse>>
{
    public string? Id { get; set; }
    public string? UserName { get; set; } 
    
    public DeleteUserCommand(string? id, string? userName)
    {
        Id = id;
        UserName = userName;
    }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<DeleteUserCommandResponse>>
{
    private readonly IMapper _mapper;
    private readonly IMessagePublisher _publisher;
    private readonly UserManager<User> _userManager;
    
    public DeleteUserCommandHandler(IMapper mapper, IMessagePublisher publisher, UserManager<User> userManager)
    {
        _mapper = mapper;
        _publisher = publisher;
        _userManager = userManager;
    }

    public async Task<ErrorOr<DeleteUserCommandResponse>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await FindUserByNameOrId(request);
        var identityResult = await _userManager.DeleteAsync(user);

        if (identityResult.Succeeded)
        {
            var @event = _mapper.Map<UserDeletedEvent>(user);
            await _publisher.PublishTopicAsync(@event, MessageMetadata.Now(), cancellationToken);
            return new DeleteUserCommandResponse(request.UserName!);
        }
        
        var errors = identityResult.Errors
            .Select(x => Error.Failure(x.Code, x.Description))
            .ToList();
        return ErrorOr<DeleteUserCommandResponse>.From(errors);
    }

    private async Task<User> FindUserByNameOrId(DeleteUserCommand command)
    {
        if (command.Id is null) return await _userManager.FindByNameAsync(command.UserName);
        return await _userManager.FindByIdAsync(command.Id);
    }
}