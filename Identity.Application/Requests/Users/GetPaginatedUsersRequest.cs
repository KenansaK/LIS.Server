using Identity.Application.Queries;
using Identity.Domain.Dtos;
using Kernal.Helpers;
using MediatR;
using SharedKernel;

namespace CRM.Application.Requests;
public class GetPaginatedUsersRequest : IRequest<Result<PaginatedUsersDto>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string? SearchTerm { get; set; }
    public string? SortColumn { get; set; }
    public bool SortAscending { get; set; }

    public GetPaginatedUsersRequest(int pageIndex, int pageSize, string? searchTerm = null, string? sortColumn = "_", bool sortAscending = true)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        SearchTerm = searchTerm;
        SortColumn = sortColumn;
        SortAscending = sortAscending;
    }
}
public class GetPaginatedUsersRequestHandler : IRequestHandler<GetPaginatedUsersRequest, Result<PaginatedUsersDto>>
{
    private readonly Dispatcher _dispatcher;

    public GetPaginatedUsersRequestHandler(Dispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public async Task<Result<PaginatedUsersDto>> Handle(GetPaginatedUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await _dispatcher.DispatchAsync(new GetPaginatedUsersQuery
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            SearchTerm = request.SearchTerm,
            SortColumn = request.SortColumn,
            SortAscending = request.SortAscending
        }, cancellationToken);

        if (users != null && users.Users.Any())
        {
            return Result.Success(users);
        }

        return Result.NotFound("No users Found");
    }
}
