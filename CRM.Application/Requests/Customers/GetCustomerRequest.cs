using CRM.Application.Queries;
using CRM.Domain.Dtos;
using Kernal.Helpers;
using MediatR;
using SharedKernel;
using CRM.Domain.Mapping;

namespace CRM.Application.Requests.Customers;
public class GetCustomerRequest : IRequest<Result<CustomerDto>>
{
    public long Id { get; set; }
}

public class GetCustomerRequestHandler(Dispatcher dispatcher) : IRequestHandler<GetCustomerRequest, Result<CustomerDto>>
{
    public async Task<Result<CustomerDto>> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await dispatcher.DispatchAsync(new GetCustomerQuery { Id = request.Id });

        if (customer is null)
            return Result.NotFound("Order Not Found");

        return Result.Success(customer.ToDto());
    }
}