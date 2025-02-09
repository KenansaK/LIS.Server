using Identity.Application.Commands;
using Identity.Domain.Dtos;
using Identity.Domain.Models;
using MediatR;
using SharedKernel;
using Kernal.Helpers;
using Identity.Domain.Entities;

namespace Identity.Application.Requests;

public class AssignPermissionToRoleRequest : IRequest<Result>
{
    public required long RoleId { get; set; }
    public required long PermissionId { get; set; }
}

public class AssignPermissionToRoleRequestHandler(Dispatcher dispatcher) : IRequestHandler<AssignPermissionToRoleRequest, Result>
{
    public async Task<Result> Handle(AssignPermissionToRoleRequest request, CancellationToken cancellationToken)
    {
        // Create the RolePermission entity to assign the permission to the role
        var rolePermissionEntity = new RolePermission
        {
            RoleId = request.RoleId,
            PermissionId = request.PermissionId
        };

        // Dispatch a command to add the RolePermission entity
        await dispatcher.DispatchAsync(new AssignPermissionToRoleCommand { entity = rolePermissionEntity });

        // Return the RolePermission DTO
        return Result.Success("Permission Assigned To Role Succesfully");
    }
}
