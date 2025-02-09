using Identity.Domain.Dtos;
using Identity.Domain.Entities;
using Identity.Domain.mapping;
using Kernal.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Identity.Application.Queries;

public class GetPaginatedUsersQuery : IQuery<PaginatedUsersDto>
{
    public int PageIndex { get; set; } // Default to first page
    public int PageSize { get; set; } // Default page size
    public string? SearchTerm { get; set; } // Optional search term
    public string? SortColumn { get; set; } = "_"; // Default sort column
    public bool SortAscending { get; set; } = true; // Default sort order
}

public class GetPaginatedUsersQueryHandler : IQueryHandler<GetPaginatedUsersQuery, PaginatedUsersDto>
{
    private readonly IRepository<User> _repository;

    public GetPaginatedUsersQueryHandler(IRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedUsersDto> Handle(GetPaginatedUsersQuery request, CancellationToken cancellationToken)
    {
        // Build the ordering logic dynamically based on the SortColumn and SortAscending values.
        var orderBy = BuildOrderByExpression(request.SortColumn, request.SortAscending);

        var usersQuery = _repository.Query().Where(x => x.StatusId != 3);

        // Apply search filtering if provided.
        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.ToLower();
            usersQuery = usersQuery.Where(b => EF.Functions.Like(b.FullName.ToLower(), $"%{searchTerm}%"));
        }

        // Count the total records for pagination.
        var totalRecords = await usersQuery.CountAsync(cancellationToken);

        // Apply ordering, skipping and taking for pagination.
        var paginatedUsers = await orderBy(usersQuery)
       .Skip((request.PageIndex - 1) * request.PageSize)
       .Take(request.PageSize)
       .ToListAsync(cancellationToken);

        // Map to DTO and return.
        return new PaginatedUsersDto
        {
            TotalRecords = totalRecords,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Users = paginatedUsers.ToDtos().ToList() // Assuming `ToDTOs` is an extension method to map entities to DTOs.
        };
    }

    private static Func<IQueryable<User>, IOrderedQueryable<User>> BuildOrderByExpression(string? sortColumn, bool sortAscending)
    {
        return sortColumn?.ToLower() switch
        {
            "FullName" => sortAscending
                ? (Func<IQueryable<User>, IOrderedQueryable<User>>)(q => q.OrderBy(b => b.FullName))
                : (q => q.OrderByDescending(b => b.FullName)),
            "Username" => sortAscending
                ? (q => q.OrderBy(b => b.Username))
                : (q => q.OrderByDescending(b => b.Username)),
            _ => sortAscending
           ? (q => q.OrderBy(b => b.Id))
           : (q => q.OrderByDescending(b => b.Id))
        };
    }
}
