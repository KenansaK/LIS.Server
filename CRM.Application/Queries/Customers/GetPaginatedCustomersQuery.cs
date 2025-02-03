using CRM.Domain.Dtos;
using CRM.Domain.Entities;
using CRM.Domain.Mapping;
using Kernal.Contracts;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace CRM.Application.Queries.Customers;

public class GetPaginatedCustomersQuery : IRequest<PaginatedCustomersDto>
{
    public int PageIndex { get; set; } // Default to first page
    public int PageSize { get; set; } // Default page size
    public string? SearchTerm { get; set; } // Optional search term
    public string? SortColumn { get; set; } = "_"; // Default sort column
    public bool SortAscending { get; set; } = true; // Default sort order
}

public class GetPaginatedCustomersQueryHandler : IRequestHandler<GetPaginatedCustomersQuery, PaginatedCustomersDto>
{
    private readonly IRepository<Customer> _repository;

    public GetPaginatedCustomersQueryHandler(IRepository<Customer> repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedCustomersDto> Handle(GetPaginatedCustomersQuery request, CancellationToken cancellationToken)
    {
        // Build the ordering logic dynamically based on the SortColumn and SortAscending values.
        var orderBy = BuildOrderByExpression(request.SortColumn, request.SortAscending);

        var customersQuery = _repository.Query().Where(x => x.StatusId != 3);

        // Apply search filtering if provided.
        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            var searchTerm = request.SearchTerm.ToLower();
            customersQuery = customersQuery.Where(b => EF.Functions.Like(b.CompanyCommercialName.ToLower(), $"%{searchTerm}%"));
        }

        // Count the total records for pagination.
        var totalRecords = await customersQuery.CountAsync(cancellationToken);

        // Apply ordering, skipping and taking for pagination.
        var paginatedCustomer = await orderBy(customersQuery)
       .Skip((request.PageIndex - 1) * request.PageSize)
       .Take(request.PageSize)
       .ToListAsync(cancellationToken);

        // Map to DTO and return.
        return new PaginatedCustomersDto
        {
            TotalRecords = totalRecords,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            Customers = paginatedCustomer.ToDtos().ToList() // Assuming `ToDTOs` is an extension method to map entities to DTOs.
        };
    }

    private static Func<IQueryable<Customer>, IOrderedQueryable<Customer>> BuildOrderByExpression(string? sortColumn, bool sortAscending)
    {
        return sortColumn?.ToLower() switch
        {
            "CompanyCommercialName" => sortAscending
                ? (Func<IQueryable<Customer>, IOrderedQueryable<Customer>>)(q => q.OrderBy(b => b.CompanyCommercialName))
                : (q => q.OrderByDescending(b => b.CompanyCommercialName)),
            "CustomerCode" => sortAscending
                ? (q => q.OrderBy(b => b.CustomerCode))
                : (q => q.OrderByDescending(b => b.CustomerCode)),
            _ => sortAscending
           ? (q => q.OrderBy(b => b.Id))
           : (q => q.OrderByDescending(b => b.Id))
        };
    }
}
