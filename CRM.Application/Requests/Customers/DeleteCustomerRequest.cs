using CRM.Application.Commands;
using CRM.Application.Queries;
using Kernal.Contracts;
using Kernal.Helpers;
using MediatR;
using SharedKernel;

namespace CRM.Application.Requests;
public class DeleteCustomerRequest : IRequest<Result>
{
    public long Id { get; set; }
}
public class DeleteCustomerRequestHandler(Dispatcher dispatcher) : IRequestHandler<DeleteCustomerRequest, Result>
{
    public async Task<Result> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await dispatcher.DispatchAsync(new GetCustomerQuery { Id = request.Id }, cancellationToken);

        if (customer == null)
            return Result.NotFound("Order not found");

        await dispatcher.DispatchAsync(new DeleteCustomerCommand { Customer = customer }, cancellationToken);

        return Result.Success("Successfully Deleted");
    }
}
