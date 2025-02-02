namespace Kernal.Contracts;

public interface ICommand
{
}
public interface ICommand<T>
{
}
public interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    Task Handle(TCommand command, CancellationToken cancellationToken = default);
}
