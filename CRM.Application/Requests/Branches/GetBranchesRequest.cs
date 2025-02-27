using CRM.Application.Queries;
using CRM.Domain.Dtos;
using Kernal.Helpers;
using MediatR;
using SharedKernel;
using CRM.Domain.Mapping;
using Kernal.Caching;
using System.Text.Json;
using Kernal.Interfaces;

namespace CRM.Application.Requests;

public class GetBranchesRequest : IRequest<Result<IEnumerable<BranchDto>>> { }

public class GetBranchesRequestHandler : IRequestHandler<GetBranchesRequest, Result<IEnumerable<BranchDto>>>
{
    private readonly Dispatcher _dispatcher;
    private readonly ICacheService _cacheService;

    public GetBranchesRequestHandler(Dispatcher dispatcher, ICacheService cacheService)
    {
        _dispatcher = dispatcher;
        _cacheService = cacheService;
    }

    public async Task<Result<IEnumerable<BranchDto>>> Handle(GetBranchesRequest request, CancellationToken cancellationToken)
    {
        // // Generate a cache key for the list of branches
        // string cacheKey = "Branches:All";
        //
        // // First, check if the data exists in the cache
        // var cachedBranches = await _cacheService.GetRecordAsync<IEnumerable<BranchDto>>(cacheKey, "/api/branches");
        //
        // if (cachedBranches is not null)
        // {
        //     // If the data is found in the cache, return it
        //     return Result.Success(cachedBranches);
        // }
        //
        // // If the data is not found in cache, fetch it from the database via the dispatcher (query)
        // var branches = await _dispatcher.DispatchAsync(new GetBranchesQuery(), cancellationToken);
        //
        // if (branches is null || !branches.Any())
        //     return Result.NotFound("No Branches Found");
        //
        // // Map the branch data to DTO and cache it for future requests
        // var branchDtos = branches.ToDtos();
        // await _cacheService.SetRecordAsync(cacheKey, branchDtos, TimeSpan.FromMinutes(10));
        //
        // // Return the mapped DTO
        // return Result.Success(branchDtos);
        return Result.Success();
    }
}