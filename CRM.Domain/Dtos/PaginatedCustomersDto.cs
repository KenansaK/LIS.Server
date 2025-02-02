using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Dtos;

public class PaginatedCustomersDto
{
    public int TotalRecords { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public IEnumerable<CustomerDto> Customers { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
}