using BankStatements.Application.BankStatements.Commands.Create;
using BankStatements.Application.BankStatements.Queries.GetByIdWithPagination;
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
    [DisableRequestSizeLimit]
    [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> ParseBankStatementAsync([FromForm] CreateBankStatementCommand command)
    {
        var response = await _mediator.Send(command);
        return response.Match(Ok, Problem);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBankStatementByIdAsync(string id, [FromQuery] int pageIndex, [FromQuery] int pageSize)
    {
        var query = new GetBankStatementByIdWithPaginationQuery(new Guid(id), pageIndex, pageSize);
        var response = await _mediator.Send(query);
        return response.Match(Ok, Problem);
    }
}