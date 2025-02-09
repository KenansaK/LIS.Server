using Identity.Domain.Entities;
using Kernal.Contracts;

namespace Identity.Application.Commands;

public class UpdateRoleCommand : ICommand
{
    public Role Role { get; set; }
}

public class UpdateRoleCommandHandler : ICommandHandler<UpdateRoleCommand>
{
    private readonly IRepository<Role> _roleRepository;

    public UpdateRoleCommandHandler(IRepository<Role> roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task Handle(UpdateRoleCommand command, CancellationToken cancellationToken = default)
    {
        await _roleRepository.UpdateAsync(command.Role);
    }
}