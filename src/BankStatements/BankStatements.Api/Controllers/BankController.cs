using BankStatements.Application.Banks.Commands.Create;
using BankStatements.Application.Banks.Commands.Delete;
using BankStatements.Application.Banks.Commands.Update;
using BankStatements.Application.Banks.Queries.GetAll;
using BankStatements.Application.Banks.Queries.GetByName;
using GeldMeister.Common.Infrastructure.Web;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankStatements.Api.Controllers;

[ApiController, Route("/api/[controller]")]
public class BankController : ApiController
{
    private readonly IMediator _mediator;

    public BankController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBanksAsync()
    {
        var query = new GetAllBanksQuery();
        var response = await _mediator.Send(query);
        return response.Match(Ok, Problem);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBankByNameAsync(Guid id)
    {
        var query = new GetBankByIdQuery(id);
        var response = await _mediator.Send(query);
        return response.Match(Ok, Problem);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBankAsync([FromBody] CreateBankCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateBankAsync([FromBody] UpdateBankCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBankAsync(Guid id)
    {
        var command = new DeleteBankCommand(id);
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }
}