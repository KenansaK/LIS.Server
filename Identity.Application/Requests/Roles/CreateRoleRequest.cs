using Identity.Application.Commands;
using Identity.Domain.Dtos;
using Identity.Domain.Models;
using MediatR;
using SharedKernel;
using Kernal.Helpers;
using Identity.Domain.mapping;

namespace Identity.Application.Requests;

public class CreateRoleRequest : IRequest<Result<RoleDto>>
{
    public required RoleModel Model { get; set; }
}

public class CreateRoleRequestHandler(Dispatcher dispatcher) : IRequestHandler<CreateRoleRequest, Result<RoleDto>>
{
    public async Task<Result<RoleDto>> Handle(CreateRoleRequest request, CancellationToken cancellationToken)
    {
        var roleEntity = request.Model.ToEntity();
        await dispatcher.DispatchAsync(new AddRoleCommand { Role = roleEntity });
        return Result.Success(roleEntity.ToDto());
    }
}
