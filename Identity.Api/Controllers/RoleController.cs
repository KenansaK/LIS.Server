using Identity.Application.Requests;
using Identity.Domain.Models;
using Kernal.Middleware;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET all roles 
    [HttpGet("roles")]
    [AuthorizePermission("ViewRole")]
    public async Task<ActionResult> GetAllRoles()
    {
        return await _mediator.Send(new GetRolesRequest());
    }

    // POST: create role
    [HttpPost("roles")]
    [AuthorizePermission("CreateRole")]
    public async Task<ActionResult> CreateRole([FromBody] RoleModel model)
    {
        if (model == null)
        {
            return BadRequest("Invalid role data.");
        }
        return await _mediator.Send(new CreateRoleRequest { Model = model });
    }

    // PUT: update roles
    [HttpPut("roles/{id}")]
    [AuthorizePermission("EditRole")]
    public async Task<ActionResult> UpdateRole(long id, [FromBody] RoleModel model)
    {
        if (model == null)
        {
            return BadRequest("Invalid role data.");
        }
        return await _mediator.Send(new UpdateRoleRequest { Id = id, Model = model });
    }

    // DELETE role
    [HttpDelete("roles/{id}")]
    [AuthorizePermission("DeleteRole")]
    public async Task<ActionResult> DeleteRole(long id)
    {
        return await _mediator.Send(new DeleteRoleRequest { Id = id });
    }


}
