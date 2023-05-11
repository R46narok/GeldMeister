using GeldMeister.Common.Infrastructure.Web;
using GeldMeister.Users.Commands.Claims.AddClaimToUserCommand;
using GeldMeister.Users.Commands.Claims.RemoveClaimCommand;
using GeldMeister.Users.Commands.Claims.ReplaceClaimCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GeldMeister.Users.Api.Controllers;

[ApiController, Route("/api/[controller]")]
public class ClaimController : ApiController
{
    private readonly IMediator _mediator;

    public ClaimController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddClaimAsync([FromQuery] string? id, [FromQuery] string? username,
        [FromQuery] string claimType, [FromQuery] string claimValue)
    {
        var command = new AddClaimCommand(id, username, claimType, claimValue);
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveClaimAsync([FromQuery] string? id, [FromQuery] string? username,
        [FromQuery] string claimType, [FromQuery] string claimValue)
    {
        var command = new RemoveClaimCommand(id, username, claimType, claimValue);
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ReplaceClaimValueAsync([FromQuery] string? id, [FromQuery] string? username,
        [FromQuery] string claimType, [FromQuery] string oldClaimValue, [FromQuery] string newClaimValue)
    {
        var command = new ReplaceClaimCommand(id, username, claimType, oldClaimValue, newClaimValue);
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }
}