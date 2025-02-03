using CRM.Application.Commands;
using CRM.Application.Queries;
using CRM.Domain.Dtos;
using CRM.Domain.Mapping;
using CRM.Domain.Models;
using Kernal.Helpers;
using MediatR;
using SharedKernel;

namespace CRM.Application.Requests;

public class UpdateAddressRequest : IRequest<Result<AddressDto>>
{
    public required long Id { get; set; }
    public required AddressModel Model { get; set; }
}
public class UpdateAddressRequesttHandler(Dispatcher dispatcher) : IRequestHandler<UpdateAddressRequest, Result<AddressDto>>
{
    public async Task<Result<AddressDto>> Handle(UpdateAddressRequest request, CancellationToken cancellationToken)
    {
        var address = await dispatcher.DispatchAsync(new GetAddressQuery { Id = request.Id });

        if (address == null)
        {
            return Result.NotFound("Address not found");
        }

        address.AddressLine1 = request.Model.AddressLine1;
        address.AddressLine2 = request.Model.AddressLine2;
        address.BranchCode = request.Model.BranchCode;
        address.City = request.Model.City;
        address.ContactName = request.Model.ContactName;
        address.Country = request.Model.Country;
        address.Email = request.Model.Email;
        address.FAXNumber = request.Model.FAXNumber;
        address.LocationCode1 = request.Model.LocationCode1;
        address.LocationCode2 = request.Model.LocationCode2;
        address.LocationCode3 = request.Model.LocationCode3;
        address.Phone = request.Model.Phone;
        address.StatusId = request.Model.Status;
        address.Phone = request.Model.Phone;
        address.ZipCode = request.Model.ZipCode;
        address.BranchId = request.Model.BranchId;

        await dispatcher.DispatchAsync(new UpdateAddressCommand { Address = address });

        return Result.Success(address.ToDto()!);
    }
}