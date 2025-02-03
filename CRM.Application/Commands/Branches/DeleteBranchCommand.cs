using CRM.Domain.Entities;
using Kernal.Contracts;

namespace CRM.Application.Commands;

public class DeleteBranchCommand : ICommand
{
    public Branch Branch { get; set; }
}
public class DeleteBranchCommandHandler : ICommandHandler<DeleteBranchCommand>
{
    private readonly IRepository<Branch> _Branch;

    public DeleteBranchCommandHandler(IRepository<Branch> branch)
    {
        _Branch = branch;
    }

    public async Task Handle(DeleteBranchCommand command, CancellationToken cancellationToken = default)
    {
        await _Branch.DeleteAsync(command.Branch);
    }
}