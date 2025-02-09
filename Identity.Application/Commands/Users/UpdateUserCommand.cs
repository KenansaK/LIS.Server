using Identity.Domain.Entities;
using Kernal.Contracts;

namespace Identity.Application.Commands;
public class UpdateUserCommand : ICommand
{
    public User User { get; set; }
}
public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IRepository<User> _userRepository;

    public UpdateUserCommandHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(UpdateUserCommand command, CancellationToken cancellationToken = default)
    {
        await _userRepository.UpdateAsync(command.User);
    }
}