using CRM.Domain.Dtos;
using Kernal.Helpers;
using MediatR;
using SharedKernel;
using CRM.Application.Queries;
using CRM.Domain.Mapping;

namespace CRM.Application.Requests;

public class GetBranchRequest : IRequest<Result<BranchDto>>
{
    public long Id { get; set; }
}

public class GetBranchRequestHandler(Dispatcher dispatcher) : IRequestHandler<GetBranchRequest, Result<BranchDto>>
{
    public async Task<Result<BranchDto>> Handle(GetBranchRequest request, CancellationToken cancellationToken)
    {
        var branch = await dispatcher.DispatchAsync(new GetBranchQuery { Id = request.Id });

        if (branch is null)
            return Result.NotFound("Branch Not Found");

        return Result.Success(branch.ToDto()!);
    }
}