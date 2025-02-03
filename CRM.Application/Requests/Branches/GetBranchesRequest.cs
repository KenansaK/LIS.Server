using CRM.Application.Queries;
using CRM.Domain.Dtos;
using Kernal.Helpers;
using MediatR;
using SharedKernel;
using CRM.Domain.Mapping;

namespace CRM.Application.Requests;

public class GetBranchesRequest : IRequest<Result<IEnumerable<BranchDto>>> { }
public class GetBranchesRequestHandler(Dispatcher _dispatcher) : IRequestHandler<GetBranchesRequest, Result<IEnumerable<BranchDto>>>
{
    public async Task<Result<IEnumerable<BranchDto>>> Handle(GetBranchesRequest request, CancellationToken cancellationToken)
    {
        var branches = await _dispatcher.DispatchAsync(new GetBranchesQuery(), cancellationToken);

        if (branches.Count > 0)
        {
            return Result.Success(branches.ToDtos());
        }
        return Result.NotFound("No Orders Found");
    }
}
