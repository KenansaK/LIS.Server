using CRM.Application.Commands;
using CRM.Application.Queries;
using Kernal.Helpers;
using MediatR;
using SharedKernel;

namespace CRM.Application.Requests;
public class DeleteAddressRequest : IRequest<Result>
{
    public long Id { get; set; }
}
public class DeleteAddressRequestHandler(Dispatcher dispatcher) : IRequestHandler<DeleteAddressRequest, Result>
{
    public async Task<Result> Handle(DeleteAddressRequest request, CancellationToken cancellationToken)
    {
        var address = await dispatcher.DispatchAsync(new GetAddressQuery { Id = request.Id }, cancellationToken);

        if (address == null)
            return Result.NotFound("Address not found");

        await dispatcher.DispatchAsync(new DeleteAddressCommand { Address = address }, cancellationToken);

        return Result.Success("Successfully Deleted");
    }
}