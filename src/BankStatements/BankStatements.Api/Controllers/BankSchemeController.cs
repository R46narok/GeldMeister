using BankStatements.Application.Banks.Commands.Create;
using BankStatements.Application.Banks.Queries.GetByName;
using BankStatements.Application.BankSchemeProperties.Commands.Create;
using BankStatements.Application.BankSchemeProperties.Commands.Delete;
using BankStatements.Application.BankSchemeProperties.Commands.Update;
using BankStatements.Application.BankSchemes.Commands.Create;
using BankStatements.Application.BankSchemes.Commands.Delete;
using BankStatements.Application.BankSchemes.Commands.Update;
using GeldMeister.Common.Infrastructure.Web;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BankStatements.Api.Controllers;

public class BankSchemeController : ApiController
{
    private readonly IMediator _mediator;

    public BankSchemeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBankSchemeAsync([FromBody] CreateBankSchemeCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBankSchemeAsync([FromBody] DeleteBankSchemeCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateBankSchemeAsync([FromBody] UpdateBankSchemeCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }

    [HttpPost, Route("/api/[controller]/property")]
    public async Task<IActionResult> CreateBankSchemePropertyAsync([FromBody] CreateBankSchemePropertyCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }

    [HttpDelete, Route("/api/[controller]/property")]
    public async Task<IActionResult> DeleteBankSchemePropertyAsync([FromBody] DeleteBankSchemePropertyCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }

    [HttpPatch, Route("/api/[controller]/property")]
    public async Task<IActionResult> UpdateBankSchemePropertyAsync([FromBody] UpdateBankSchemePropertyCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }
}