using CRM.Application.Requests;
using CRM.Domain.Models;
using Kernal.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;


[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class BranchesController : ControllerBase
{
    private readonly IMediator _mediator;

    public BranchesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]

    public async Task<ActionResult> GetAllBranches()
    {
        return await _mediator.Send(new GetBranchesRequest() { });
    }

    [HttpPost]
    public async Task<ActionResult> CreateBranch([FromBody] BranchModel model)
    {
        if (model == null)
        {
            return BadRequest("Invalid branch data.");
        }
        return await _mediator.Send(new CreateBranchRequest { Model = model });
    }

    [HttpGet("{id}/addresses")]
    public async Task<ActionResult> GetAddressByBranchId(long id)
    {
        var result = await _mediator.Send(new GetAddressByBranchIdRequest() { Id = id });
        return result;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateBranch(int id, [FromBody] BranchModel model)
    {
        return await _mediator.Send(new UpdateBranchRequest { Id = id, Model = model });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetBranchById(long id)
    {
        var result = await _mediator.Send(new GetBranchRequest() { Id = id });
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBranch(int id)
    {
        return await _mediator.Send(new DeleteBranchRequest { Id = id });
    }
}

