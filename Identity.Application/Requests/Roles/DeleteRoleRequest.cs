using Identity.Application.Commands;
using Identity.Application.Queries;
using Kernal.Helpers;
using MediatR;
using SharedKernel;

namespace Identity.Application.Requests;

public class DeleteRoleRequest : IRequest<Result>
{
    public long Id { get; set; }
}

public class DeleteRoleRequestHandler(Dispatcher dispatcher) : IRequestHandler<DeleteRoleRequest, Result>
{
    public async Task<Result> Handle(DeleteRoleRequest request, CancellationToken cancellationToken)
    {
        // Get the role by its ID
        var role = await dispatcher.DispatchAsync(new GetRoleQuery { Id = request.Id }, cancellationToken);

        // If the role doesn't exist, return a NotFound result
        if (role == null)
            return Result.NotFound("Role not found");

        // Dispatch the delete command for the role
        await dispatcher.DispatchAsync(new DeleteRoleCommand { Role = role }, cancellationToken);

        // Return success message
        return Result.Success("Successfully Deleted");
    }
}
