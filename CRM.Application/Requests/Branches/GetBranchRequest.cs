using CRM.Domain.Dtos;
using Kernal.Helpers;
using MediatR;
using SharedKernel;
using CRM.Application.Queries;
using CRM.Domain.Mapping;
using Kernal.Interfaces;

namespace CRM.Application.Requests;

public class GetBranchRequest : IRequest<Result<BranchDto>>
{
    public long Id { get; set; }
}

public class GetBranchRequestHandler : IRequestHandler<GetBranchRequest, Result<BranchDto>>
{
    private readonly Dispatcher _dispatcher;
    private readonly ICacheService _cacheService;
    private readonly string _baseUrl = "https://localhost:44317"; // Replace with actual base URL

    public GetBranchRequestHandler(Dispatcher dispatcher, ICacheService cacheService)
    {
        _dispatcher = dispatcher;
        _cacheService = cacheService;
    }

    public async Task<Result<BranchDto>> Handle(GetBranchRequest request, CancellationToken cancellationToken)
    {
        string cacheKey = $"Branches:Id:{request.Id}";

        // Check if data exists in cache first
        var cachedBranch = await _cacheService.GetRecordAsync<BranchDto>("Branches","Id",request.Id.ToString(), _baseUrl);
        if (cachedBranch is not null)
        {
            return Result.Success(cachedBranch);
        }


        // Fetch from database via dispatcher if not found in cache
        var branch = await _dispatcher.DispatchAsync(new GetBranchQuery { Id = request.Id });
        if (branch is null)
        {
            return Result.NotFound("Branch Not Found");
        }

        // Map the branch data to DTO and store it in cache
        var branchDto = branch.ToDto();
        if (branchDto is not null)
        {
            await _cacheService.SetRecordAsync(cacheKey, branchDto, TimeSpan.FromMinutes(10));
        }

        return Result.Success(branchDto);
    }
}