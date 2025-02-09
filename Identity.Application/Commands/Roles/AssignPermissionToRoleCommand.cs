using Identity.Domain.Entities;
using Kernal.Contracts;

namespace Identity.Application.Commands;

public class AssignPermissionToRoleCommand : ICommand
{
    public RolePermission entity { get; set; }
}

public class AssignPermissionToRoleCommandHandler : ICommandHandler<AssignPermissionToRoleCommand>
{
    private readonly IRepository<RolePermission> _repo;

    public AssignPermissionToRoleCommandHandler(IRepository<RolePermission> repo)
    {
        _repo = repo;
    }

    public async Task Handle(AssignPermissionToRoleCommand command, CancellationToken cancellationToken = default)
    {
        await _repo.AddAsync(command.entity);
    }
}