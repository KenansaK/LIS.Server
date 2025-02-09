using Identity.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolePermissionController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolePermissionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // POST: api/authorization/roles/{roleId}/permissions
    [HttpPost("roles/{roleId}/permissions")]
    public async Task<ActionResult> AssignPermissionToRole(long roleId, long permissionId)
    {
        return await _mediator.Send(new AssignPermissionToRoleRequest { RoleId = roleId, PermissionId = permissionId });
    }
    // PERMISSIONS
    // Delete: api/authorization/roles/{roleId}/permissions
    [HttpDelete("roles/{roleId}/permissions")]
    public async Task<ActionResult> DeletePermissionTFromRole(long roleId, long permissionId)
    {
        return await _mediator.Send(new DeletePermissionFromRoleRequest { RoleId = roleId, PermissionId = permissionId });
    }

    // GET: api/authorization/roles/{roleId}/permissions
    [HttpGet("roles/{roleId}/permissions")]
    public async Task<ActionResult> GetPermissionsForRole(long roleId)
    {
        return await _mediator.Send(new GetPermissionsForRoleRequest { RoleId = roleId });
    }
}
