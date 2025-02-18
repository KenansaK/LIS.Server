using Identity.Application.Requests;
using Identity.Application.Requests.Permissions;
using Identity.Infrastructure;
using Kernal.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class PermissionController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IdentityDbContext _context;

    public PermissionController(IMediator mediator, IdentityDbContext context)
    {
        _mediator = mediator;
        _context = context;
    }

    // get all permissions 
    [HttpGet("permissions")] 
    [Permission("ViewPermission")]
    public async Task<ActionResult> GetAllPermissions()
    {
        return await _mediator.Send(new GetPermissionsRequest());
    }

    [HttpGet("permissions/{userId}")]
    [Permission("ViewPermission")]
    public async Task<ActionResult> GetPermissionsByUserId(long userId)
    {
        var permissionNames = await _mediator.Send(new GetPermissionsByUserIdRequest { UserId = userId });
        return Ok(permissionNames);
    }
    

    [HttpGet("roles/{roleId}/all-permissions")]
    [Permission("ViewPermission")]
    public async Task<IActionResult> GetAllPermissionsForRole(int roleId)
    {
        // Get all system permissions
        var allPermissions = await _context.Permissions.ToListAsync();

        // Get the role's permissions
        var role = await _context.Roles
            .Include(r => r.RolePermissions)
            .FirstOrDefaultAsync(r => r.Id == roleId);

        if (role == null)
            return NotFound();

        // Create the response with all permissions, marking which ones the role has
        var response = new RolePermissionsDto
        {
            RoleName = role.Name,
            Permissions = allPermissions.Select(p => new PermissionDto
            {
                Id = p.Id,
                Name = p.PermissionName,
                Module = p.module,
                IsGranted = role.RolePermissions.Any(rp => rp.PermissionId == p.Id)
            }).ToList()
        };

        return Ok(new Result<RolePermissionsDto>
        {
            IsSuccess = true,
            Data = response,
            Message = "Permissions retrieved successfully",

        });
    }
    public class RolePermissionsDto
    {
        public string RoleName { get; set; }
        public List<PermissionDto> Permissions { get; set; }
    }

    public class PermissionDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Module { get; set; }
        public bool IsGranted { get; set; }
    }
}
