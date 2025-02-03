using CRM.Domain.Entities;
using CRM.Infrastructure;
using Kernal.Contracts;
namespace CRM.Application.Commands;

public class AddBranchCommand : ICommand
{
    public Branch Branch { get; set; }
}
public class AddBranchCommandHandler : ICommandHandler<AddBranchCommand>
{
    private readonly IRepository<Branch> _Branch;

    public AddBranchCommandHandler(IRepository<Branch> branch)
    {
        _Branch = branch;
    }

    public async Task Handle(AddBranchCommand command, CancellationToken cancellationToken = default)
    {
        await _Branch.AddAsync(command.Branch);
    }
}
