using CRM.Application.Queries;
using CRM.Domain.Dtos;
using CRM.Domain.Mapping;
using Kernal.Helpers;
using MediatR;
using SharedKernel;

namespace CRM.Application.Requests;

public class GetAddressesRequest : IRequest<Result<IEnumerable<AddressDto>>> { }
public class GetAddressesRequestHandler(Dispatcher _dispatcher) : IRequestHandler<GetAddressesRequest, Result<IEnumerable<AddressDto>>>
{
    public async Task<Result<IEnumerable<AddressDto>>> Handle(GetAddressesRequest request, CancellationToken cancellationToken)
    {
        var address = await _dispatcher.DispatchAsync(new GetAddressesQuery(), cancellationToken);

        if (address.Count > 0)
        {
            return Result.Success(address.ToDtos());
        }
        return Result.NotFound("No Orders Found");
    }
}
