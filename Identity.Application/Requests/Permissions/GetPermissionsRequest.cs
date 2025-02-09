using Identity.Application.Queries;
using Identity.Domain.Dtos;
using Identity.Domain.Mapping;
using Kernal.Helpers;
using MediatR;
using SharedKernel;
namespace Identity.Application.Requests;
public class GetPermissionsRequest : IRequest<Result<IEnumerable<PermissionDto>>>
{ }

public class GetPermissionsRequestHandler(Dispatcher _dispatcher) : IRequestHandler<GetPermissionsRequest, Result<IEnumerable<PermissionDto>>>
{
    public async Task<Result<IEnumerable<PermissionDto>>> Handle(GetPermissionsRequest request, CancellationToken cancellationToken)
    {
        var permissions = await _dispatcher.DispatchAsync(new GetPermissionsQuery(), cancellationToken);

        if (permissions.Any())
        {
            return Result.Success(permissions.ToDtos());
        }
        return Result.NotFound("No Permissions Found");
    }
}
