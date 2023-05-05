using BankStatements.Application.Banks.Commands.Create;
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

    [HttpPost]
    public async Task<IActionResult> CreateBankAsync([FromBody] CreateBankCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBanksAsync()
    {
        var query = new GetAllBanksQuery();
        var response = await _mediator.Send(query);
        return response.Match(Ok, Problem);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetBankByNameAsync(string name)
    {
        var query = new GetBankByNameQuery(name);
        var response = await _mediator.Send(query);
        return response.Match(Ok, Problem);
    }
}