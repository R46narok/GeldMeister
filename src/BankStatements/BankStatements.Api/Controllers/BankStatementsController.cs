using BankStatements.Application.BankStatements.Commands.Create;
using GeldMeister.Common.Infrastructure.Web;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankStatements.Api.Controllers;


public class BankStatementsController : ApiController
{
    private readonly IMediator _mediator;

    public BankStatementsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> ParseBankStatementAsync([FromForm] CreateBankStatementCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }
}