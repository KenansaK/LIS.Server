using CRM.Application.Commands;
using CRM.Domain.Dtos;
using CRM.Domain.Models;
using MediatR;
using SharedKernel;
using CRM.Domain.Mapping;
using Kernal.Helpers;

namespace CRM.Application.Requests;

public class CreateAddressRequest : IRequest<Result<AddressDto>>
{
    public required AddressModel Model { get; set; }
}
public class CreateAddressRequestHandler(Dispatcher dispatcher) : IRequestHandler<CreateAddressRequest, Result<AddressDto>>
{
    public async Task<Result<AddressDto>> Handle(CreateAddressRequest request, CancellationToken cancellationToken)
    {
        var addressEntity = request.Model.ToEntity();
        await dispatcher.DispatchAsync(new AddAddressCommand { Address = addressEntity });
        return Result.Success(addressEntity.ToDto());
    }
}
