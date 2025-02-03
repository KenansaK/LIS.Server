using CRM.Application.Commands;
using CRM.Application.Queries;
using Kernal.Helpers;
using MediatR;
using SharedKernel;

namespace CRM.Application.Requests;
public class DeleteBranchRequest : IRequest<Result>
{
    public long Id { get; set; }
}
public class DeleteBranchRequestHandler(Dispatcher dispatcher) : IRequestHandler<DeleteBranchRequest, Result>
{
    public async Task<Result> Handle(DeleteBranchRequest request, CancellationToken cancellationToken)
    {
        var branch = await dispatcher.DispatchAsync(new GetBranchQuery { Id = request.Id }, cancellationToken);

        if (branch == null)
            return Result.NotFound("Order not found");

        await dispatcher.DispatchAsync(new DeleteBranchCommand { Branch = branch }, cancellationToken);

        return Result.Success("Successfully Deleted");
    }
}

