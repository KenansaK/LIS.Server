using Identity.Domain.Entities;
using Kernal.Contracts;
namespace Identity.Application.Commands;
public class DeleteRoleCommand : ICommand
{
    public Role Role { get; set; }
}

public class DeleteRoleCommandHandler : ICommandHandler<DeleteRoleCommand>
{
    private readonly IRepository<Role> _roleRepository;

    public DeleteRoleCommandHandler(IRepository<Role> roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task Handle(DeleteRoleCommand command, CancellationToken cancellationToken = default)
    {
        await _roleRepository.DeleteAsync(command.Role);
    }
}