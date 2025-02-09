using Identity.Domain.Entities;
using Kernal.Contracts;

namespace Identity.Application.Commands;

public class AddUserCommand : ICommand
{
    public User User { get; set; }
}
public class AddUserCommandHandler : ICommandHandler<AddUserCommand>
{
    private readonly IRepository<User> _userRepository;

    public AddUserCommandHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(AddUserCommand command, CancellationToken cancellationToken = default)
    {
        await _userRepository.AddAsync(command.User);
    }
}
