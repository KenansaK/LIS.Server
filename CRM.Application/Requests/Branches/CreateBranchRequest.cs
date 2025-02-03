using CRM.Application.Commands;
using CRM.Domain.Dtos;
using CRM.Domain.Mapping;
using CRM.Domain.Models;
using Kernal.Helpers;
using MediatR;
using SharedKernel;

namespace CRM.Application.Requests;

public class CreateBranchRequest : IRequest<Result<BranchDto>>
{
    public required BranchModel Model { get; set; }
}
public class CreateBranchRequestHandler(Dispatcher dispatcher) : IRequestHandler<CreateBranchRequest, Result<BranchDto>>
{
    public async Task<Result<BranchDto>> Handle(CreateBranchRequest request, CancellationToken cancellationToken)
    {
        var branchEntity = request.Model.ToEntity();
        await dispatcher.DispatchAsync(new AddBranchCommand { Branch = branchEntity });
        return Result.Success(branchEntity.ToDto());
    }
}