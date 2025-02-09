using Identity.Application.Commands;
using Identity.Application.Queries;
using Identity.Domain.Dtos;
using Identity.Domain.Models;
using Kernal.Helpers;
using MediatR;
using SharedKernel;
using Identity.Domain.mapping;

namespace Identity.Application.Requests;

public class UpdateRoleRequest : IRequest<Result<RoleDto>>
{
    public required long Id { get; set; }
    public required RoleModel Model { get; set; }
}

public class UpdateRoleRequestHandler(Dispatcher dispatcher) : IRequestHandler<UpdateRoleRequest, Result<RoleDto>>
{
    public async Task<Result<RoleDto>> Handle(UpdateRoleRequest request, CancellationToken cancellationToken)
    {
        // Get the role by its Id
        var role = await dispatcher.DispatchAsync(new GetRoleQuery { Id = request.Id }, cancellationToken);

        // If the role doesn't exist, return a NotFound result
        if (role == null)
        {
            return Result.NotFound("Role not found");
        }

        // Update the role properties with the data from the request model
        role.Name = request.Model.Name;

        // Dispatch the update command for the role
        await dispatcher.DispatchAsync(new UpdateRoleCommand { Role = role }, cancellationToken);

        // Return the updated role as a DTO
        return Result.Success(role.ToDto());
    }
}
