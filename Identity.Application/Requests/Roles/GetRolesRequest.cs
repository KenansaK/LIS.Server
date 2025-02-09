using Identity.Application.Queries;
using Identity.Domain.Dtos;
using Kernal.Helpers;
using MediatR;
using SharedKernel;
using Identity.Domain.mapping;

namespace Identity.Application.Requests;

public class GetRolesRequest : IRequest<Result<IEnumerable<RoleDto>>>
{ }

public class GetRolesRequestHandler(Dispatcher _dispatcher) : IRequestHandler<GetRolesRequest, Result<IEnumerable<RoleDto>>>
{
    public async Task<Result<IEnumerable<RoleDto>>> Handle(GetRolesRequest request, CancellationToken cancellationToken)
    {
        var roles = await _dispatcher.DispatchAsync(new GetRolesQuery(), cancellationToken);

        if (roles.Any())
        {
            return Result.Success(roles.ToDtos());
        }
        return Result.NotFound("No Roles Found");
    }
}
