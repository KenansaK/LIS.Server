using CRM.Domain.Dtos;
using Kernal.Helpers;
using MediatR;
using SharedKernel;
using CRM.Application.Queries;
using CRM.Domain.Mapping;

namespace CRM.Application.Requests;

public class GetAddressByBranchIdRequest : IRequest<Result<IEnumerable<AddressDto>>>
{
    public long Id { get; set; }
}

public class GetAddressByBranchIdRequestHandler(Dispatcher dispatcher) : IRequestHandler<GetAddressByBranchIdRequest, Result<IEnumerable<AddressDto>>>
{
    public async Task<Result<IEnumerable<AddressDto>>> Handle(GetAddressByBranchIdRequest request, CancellationToken cancellationToken)
    {
        var address = await dispatcher.DispatchAsync(new GetAddressesByBranchIdQuery { BranchId = request.Id });

        if (address is null)
            return Result.NotFound("Address Not Found");

        return Result.Success(address.ToDtos());
    }
}
