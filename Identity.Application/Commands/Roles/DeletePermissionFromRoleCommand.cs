using Identity.Domain.Entities;
using Kernal.Contracts;

namespace Identity.Application.Commands;
public class DeletePermissionFromRoleCommand : ICommand
{
    public RolePermission entity { get; set; }
}

public class DeletePermissionFromRoleCommandHandler : ICommandHandler<DeletePermissionFromRoleCommand>
{
    private readonly IRepository<RolePermission> _repo;

    public DeletePermissionFromRoleCommandHandler(IRepository<RolePermission> repo)
    {
        _repo = repo;
    }

    public async Task Handle(DeletePermissionFromRoleCommand command, CancellationToken cancellationToken = default)
    {
        await _repo.DeleteAsync(command.entity);
    }
}
