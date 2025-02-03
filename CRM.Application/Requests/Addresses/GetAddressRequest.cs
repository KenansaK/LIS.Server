using CRM.Application.Queries;
using CRM.Domain.Dtos;
using CRM.Domain.Mapping;
using Kernal.Helpers;
using MediatR;
using SharedKernel;

namespace CRM.Application.Requests;
public class GetAddressRequest : IRequest<Result<AddressDto>>
{
    public long Id { get; set; }
}

public class GetAddressRequestHandler(Dispatcher dispatcher) : IRequestHandler<GetAddressRequest, Result<AddressDto>>
{
    public async Task<Result<AddressDto>> Handle(GetAddressRequest request, CancellationToken cancellationToken)
    {
        var address = await dispatcher.DispatchAsync(new GetAddressQuery { Id = request.Id });

        if (address is null)
            return Result.NotFound("Lcoation Not Found");

        return Result.Success(address.ToDto());
    }
}