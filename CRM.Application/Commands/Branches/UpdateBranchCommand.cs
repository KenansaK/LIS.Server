using CRM.Domain.Entities;
using Kernal.Contracts;

namespace CRM.Application.Commands;
public class UpdateBranchCommand : ICommand
{
    public Branch Branch { get; set; }
}
public class UpdateBranchCommandHandler : ICommandHandler<UpdateBranchCommand>
{
    private readonly IRepository<Branch> _Branch;
    public UpdateBranchCommandHandler(IRepository<Branch> Branch)
    {
        _Branch = Branch;
    }

    public async Task Handle(UpdateBranchCommand command, CancellationToken cancellationToken = default)
    {
        await _Branch.UpdateAsync(command.Branch);
    }
}
