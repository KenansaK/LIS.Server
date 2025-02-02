using CRM.Application.Commands;
using CRM.Application.Queries;
using CRM.Domain.Dtos;
using CRM.Domain.Mapping;
using CRM.Domain.Models;
using Kernal.Helpers;
using MediatR;
using SharedKernel;

namespace CRM.Application.Requests;
public class UpdateCustomerRequest : IRequest<Result<CustomerDto>>
{
    public required long Id { get; set; }
    public required CustomerModel Model { get; set; }
}
public class UpdateCustomerRequestHandler(Dispatcher dispatcher) : IRequestHandler<UpdateCustomerRequest, Result<CustomerDto>>
{
    public async Task<Result<CustomerDto>> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await dispatcher.DispatchAsync(new GetCustomerQuery { Id = request.Id });

        if (customer == null)
        {
            return Result.NotFound("Order not found");
        }

        customer.CompanyCommercialName = request.Model.CompanyCommercialName;
        customer.CompanyLegalName = request.Model.CompanyLegalName;
        customer.BusinessType = request.Model.BusinessType;
        customer.CustomerCode = request.Model.CustomerCode;
        customer.CustomerNumber = request.Model.CustomerNumber;
        customer.RegistrationNumber = request.Model.RegistrationNumber;
        customer.StatusId = request.Model.Status;
        customer.BillingExternalCode = request.Model.BillingExternalCode;
        customer.ExternalCode = request.Model.ExternalCode;
        customer.StoreBarcodePrefix = request.Model.StoreBarcodePrefix;
        // customer.Logo = request.Model.Logo;

        await dispatcher.DispatchAsync(new UpdateCustomerCommand { Customer = customer });

        return Result.Success(customer.ToDto()!);
    }
}


