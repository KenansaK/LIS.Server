using CRM.Application.Commands;
using CRM.Application.Queries;
using CRM.Domain.Dtos;
using CRM.Domain.Mapping;
using CRM.Domain.Models;
using Kernal.Contracts;
using Kernal.Helpers;
using MediatR;
using SharedKernel;

namespace CRM.Application.Requests.Customers;
public class ChangeStatusCustomerRequest : IRequest<Result<CustomerDto>>
{
    public required ChangeStatusCustomerModel Model { get; set; }
}

public class ChangeStatusCustomerRequestHandler(Dispatcher dispatcher, IUnitOfWork unitOfWork) : IRequestHandler<ChangeStatusCustomerRequest, Result<CustomerDto>>
{
    public async Task<Result<CustomerDto>> Handle(ChangeStatusCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await dispatcher.DispatchAsync(new GetCustomerQuery { Id = request.Model.Id });
        if (customer == null)
        {
            return Result.NotFound("Customer not found");
        }
        customer.StatusId = (short)request.Model.Status;
        await dispatcher.DispatchAsync(new UpdateCustomerCommand { Customer = customer });
        return Result.Success(customer.ToDto()!);
    }
}