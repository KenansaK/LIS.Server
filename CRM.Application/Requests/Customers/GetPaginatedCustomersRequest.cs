using CRM.Application.Queries.Customers;
using CRM.Domain.Dtos;
using Kernal.Helpers;
using MediatR;
using SharedKernel;

namespace CRM.Application.Requests;
public class GetPaginatedCustomersRequest : IRequest<Result<PaginatedCustomersDto>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string? SearchTerm { get; set; }
    public string? SortColumn { get; set; }
    public bool SortAscending { get; set; }

    public GetPaginatedCustomersRequest(int pageIndex, int pageSize, string? searchTerm = null, string? sortColumn = "_", bool sortAscending = true)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        SearchTerm = searchTerm;
        SortColumn = sortColumn;
        SortAscending = sortAscending;
    }
}
public class GetPaginatedCustomersRequestHandler : IRequestHandler<GetPaginatedCustomersRequest, Result<PaginatedCustomersDto>>
{
    private readonly Dispatcher _dispatcher;

    public GetPaginatedCustomersRequestHandler(Dispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public async Task<Result<PaginatedCustomersDto>> Handle(GetPaginatedCustomersRequest request, CancellationToken cancellationToken)
    {
        // Dispatch the query for paginated customers.
        var customers = await _dispatcher.DispatchAsync(new GetPaginatedCustomersQuery
        {
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            SearchTerm = request.SearchTerm,
            SortColumn = request.SortColumn,
            SortAscending = request.SortAscending
        }, cancellationToken);

        // Check if customers were found and return the result.
        if (customers != null && customers.Customers.Any())
        {
            return Result.Success(customers);
        }

        return Result.NotFound("No Customers Found");
    }
}
