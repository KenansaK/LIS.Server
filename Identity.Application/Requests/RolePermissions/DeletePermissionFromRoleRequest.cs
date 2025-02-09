using Identity.Application.Commands;
using Identity.Domain.Entities;
using Kernal.Helpers;
using MediatR;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Requests;
public class DeletePermissionFromRoleRequest : IRequest<Result>
{
    public required long RoleId { get; set; }
    public required long PermissionId { get; set; }
}

public class DeletePermissionFromRoleRequestHandler(Dispatcher dispatcher) : IRequestHandler<DeletePermissionFromRoleRequest, Result>
{
    public async Task<Result> Handle(DeletePermissionFromRoleRequest request, CancellationToken cancellationToken)
    {
        // Create the RolePermission entity to delete the permission from the role
        var rolePermissionEntity = new RolePermission
        {
            RoleId = request.RoleId,
            PermissionId = request.PermissionId
        };

        // Dispatch a command to add the RolePermission entity
        await dispatcher.DispatchAsync(new DeletePermissionFromRoleCommand { entity = rolePermissionEntity });

        // Return the RolePermission DTO
        return Result.Success("Permission Deleted From the Role Succesfully");
    }
}

