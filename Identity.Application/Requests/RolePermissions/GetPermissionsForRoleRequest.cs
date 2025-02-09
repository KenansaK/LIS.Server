using Identity.Application.Queries;
using Kernal.Helpers;
using MediatR;
using SharedKernel;

namespace Identity.Application.Requests;

public class GetPermissionsForRoleRequest : IRequest<Result<List<long>>>
{
    public long RoleId { get; set; }
}

public class GetPermissionsForRoleRequestHandler(Dispatcher _dispatcher) : IRequestHandler<GetPermissionsForRoleRequest, Result<List<long>>>
{
    public async Task<Result<List<long>>> Handle(GetPermissionsForRoleRequest request, CancellationToken cancellationToken)
    {
        var permissions = await _dispatcher.DispatchAsync(new GetPermissionsForRoleQuery { RoleId = request.RoleId }, cancellationToken);

        if (permissions.Any())
        {
            return Result.Success(permissions);
        }
        return Result.NotFound("No permissions Found");
    }
}
