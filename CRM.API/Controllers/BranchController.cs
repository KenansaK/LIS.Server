using CRM.Application.Requests;
using CRM.Domain.Models;
using Kernal.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class BranchesController : ControllerBase
{
    private readonly IMediator _mediator;

    public BranchesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Permission("ViewBranch")]
    public async Task<ActionResult> GetAllBranches()
    {
        return await _mediator.Send(new GetBranchesRequest() { });
    }

    [HttpPost]
    [Permission("CreateBranch")]
    public async Task<ActionResult> CreateBranch([FromBody] BranchModel model)
    {
        if (model == null)
        {
            return BadRequest("Invalid branch data.");
        }
        return await _mediator.Send(new CreateBranchRequest { Model = model });
    }

    [HttpGet("{id}/addresses")]
    [Permission("ViewAddress")]
    public async Task<ActionResult> GetAddressByBranchId(long id)
    {
        var result = await _mediator.Send(new GetAddressByBranchIdRequest() { Id = id });
        return result;
    }

    [HttpPut("{id}")]
    [Permission("EditBranch")]
    public async Task<ActionResult> UpdateBranch(int id, [FromBody] BranchModel model)
    {
        return await _mediator.Send(new UpdateBranchRequest { Id = id, Model = model });
    }

    [HttpGet("{id}")]
    [Permission("ViewBranch")]
    public async Task<ActionResult> GetBranchById(long id)
    {
        var result = await _mediator.Send(new GetBranchRequest() { Id = id });
        return result;
    }

    [HttpDelete("{id}")]
    [Permission("DeleteBranch")]
    public async Task<ActionResult> DeleteBranch(int id)
    {
        return await _mediator.Send(new DeleteBranchRequest { Id = id });
    }
}

