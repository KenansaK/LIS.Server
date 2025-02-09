using Identity.Domain.Entities;
using Kernal.Contracts;

namespace Identity.Application.Commands;
public class DeleteUserCommand : ICommand
{
    public User User { get; set; }
}
public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IRepository<User> _userRepository;

    public DeleteUserCommandHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken = default)
    {
        await _userRepository.DeleteAsync(command.User);
    }
}