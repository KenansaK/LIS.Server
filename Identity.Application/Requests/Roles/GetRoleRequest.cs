using Identity.Application.Queries;
using Identity.Domain.Dtos;
using Kernal.Helpers;
using MediatR;
using SharedKernel;
using Identity.Domain.mapping;

namespace Identity.Application.Requests;

public class GetRoleRequest : IRequest<Result<RoleDto>>
{
    public long Id { get; set; }
}

public class GetRoleRequestHandler(Dispatcher dispatcher) : IRequestHandler<GetRoleRequest, Result<RoleDto>>
{
    public async Task<Result<RoleDto>> Handle(GetRoleRequest request, CancellationToken cancellationToken)
    {
        var role = await dispatcher.DispatchAsync(new GetRoleQuery { Id = request.Id });

        if (role is null)
            return Result.NotFound("Role Not Found");

        return Result.Success(role.ToDto());
    }
}

