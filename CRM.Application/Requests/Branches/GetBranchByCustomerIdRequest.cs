using CRM.Application.Queries;
using CRM.Domain.Dtos;
using CRM.Domain.Mapping;
using Kernal.Helpers;
using MediatR;
using SharedKernel;

namespace CRM.Application.Requests.Branches
{
    public class GetBranchByCustomerIdRequest : IRequest<Result<IEnumerable<BranchDto>>>  // Changed from Address to Branch
    {
        public long Id { get; set; }
    }
    public class GetBranchByCustomerIdRequestHandler(Dispatcher dispatcher) : IRequestHandler<GetBranchByCustomerIdRequest, Result<IEnumerable<BranchDto>>>  // Changed from Address to Branch
    {
        public async Task<Result<IEnumerable<BranchDto>>> Handle(GetBranchByCustomerIdRequest request, CancellationToken cancellationToken)
        {
            // Dispatch query to fetch branches by AddressId
            var branch = await dispatcher.DispatchAsync(new GetBranchByCustomerIdQuery { CustomerId = request.Id });

            if (branch is null)
                return Result.NotFound("Branch Not Found");

            return Result.Success(branch.ToDtos());  // Assuming there's a ToDtos method to map the branch entities to DTOs
        }
    }


}
