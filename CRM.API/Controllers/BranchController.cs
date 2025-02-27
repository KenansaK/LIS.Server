using CRM.Application.Queries;
using CRM.Application.Requests;
using CRM.Domain.Models;
using Kernal.Caching;
using Kernal.Interfaces;
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
    private readonly ICacheService _cacheService;

    public BranchesController(IMediator mediator, ICacheService cacheService)
    {
        _mediator = mediator;
        _cacheService = cacheService;
    }

    [HttpGet]
    [AuthorizePermission("ViewBranch")]
    public async Task<ActionResult> GetAllBranches()
    {
        return await _mediator.Send(new GetBranchesRequest() { });
    }

    [HttpPost]
    [AuthorizePermission("CreateBranch")]
    public async Task<ActionResult> CreateBranch([FromBody] BranchModel model)
    {
        if (model == null)
        {
            return BadRequest("Invalid branch data.");
        }
        return await _mediator.Send(new CreateBranchRequest { Model = model });
    }

    [HttpGet("{id}/addresses")]
    [AuthorizePermission("ViewAddress")]
    public async Task<ActionResult> GetAddressByBranchId(long id)
    {
        var result = await _mediator.Send(new GetAddressByBranchIdRequest() { Id = id });
        return result;
    }

    [HttpPut("{id}")]
    [AuthorizePermission("EditBranch")]
    public async Task<ActionResult> UpdateBranch(int id, [FromBody] BranchModel model)
    {
        return await _mediator.Send(new UpdateBranchRequest { Id = id, Model = model });
    }

    [HttpGet("{id}")]
    [AuthorizePermission("ViewBranch")]
    public async Task<ActionResult> GetBranchById(long id)
    {
        var result = await _mediator.Send(new GetBranchRequest() { Id = id });
        return result;
    }

    [HttpDelete("{id}")]
    [AuthorizePermission("DeleteBranch")]
    public async Task<ActionResult> DeleteBranch(int id)
    {
        return await _mediator.Send(new DeleteBranchRequest { Id = id });
    }
    
    [HttpGet("GetFromDB")]
    //[ApiExplorerSettings(IgnoreApi = true)]
    public async Task<ActionResult> GetFromDB([FromQuery] CacheQueryOption? option)
    {

        if (option.Id!= null)
        {
            var result = await _mediator.Send(new GetBranchQuery() { Id = option.Id });
            return Ok(result);
        }

        return Ok(null);
    }
    
    
}

