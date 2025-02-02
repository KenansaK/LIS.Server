using CRM.Application.Commands;
using CRM.Domain.Dtos;
using CRM.Domain.Mapping;
using CRM.Domain.Models;
using Kernal.Helpers;
using MediatR;
using SharedKernel;

namespace CRM.Application.Requests;
public class CreateCustomerRequest : IRequest<Result<CustomerDto>>
{
    public required CustomerModel Model { get; set; }
}
public class CreateCustomerRequestHandler(Dispatcher dispatcher) : IRequestHandler<CreateCustomerRequest, Result<CustomerDto>>
{
    public async Task<Result<CustomerDto>> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var CustomerEntity = request.Model.ToEntity();
        await dispatcher.DispatchAsync(new AddCustomerCommand { Customer = CustomerEntity });
        return Result.Success(CustomerEntity.ToDto());
    }
}
