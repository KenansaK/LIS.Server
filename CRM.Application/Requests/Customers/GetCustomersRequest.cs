using CRM.Application.Queries;
using CRM.Domain.Dtos;
using CRM.Domain.Mapping;
using Kernal.Helpers;
using MediatR;
using SharedKernel;

namespace CRM.Application.Requests.Customers;
public class GetCustomersRequest : IRequest<Result<IEnumerable<CustomerDto>>> { }

/// <summary>
/// Handles the GetOrdersRequest by first attempting to retrieve the data from the cache. 
/// </summary>
public class GetCustomersRequestHandler(Dispatcher _dispatcher) : IRequestHandler<GetCustomersRequest, Result<IEnumerable<CustomerDto>>>
{
    public async Task<Result<IEnumerable<CustomerDto>>> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
    {
        var Customers = await _dispatcher.DispatchAsync(new GetCustomersQuery(), cancellationToken);

        if (Customers.Count > 0)
        {
            return Result.Success(Customers.ToDtos());
        }
        return Result.NotFound("No Orders Found");
    }
}