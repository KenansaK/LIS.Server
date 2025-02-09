using Identity.Domain.Entities;
using Kernal.Contracts;

namespace Identity.Application.Commands;

// Add Role Command
public class AddRoleCommand : ICommand
{
    public Role Role { get; set; }
}

public class AddRoleCommandHandler : ICommandHandler<AddRoleCommand>
{
    private readonly IRepository<Role> _roleRepository;

    public AddRoleCommandHandler(IRepository<Role> roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task Handle(AddRoleCommand command, CancellationToken cancellationToken = default)
    {
        await _roleRepository.AddAsync(command.Role);
    }
}