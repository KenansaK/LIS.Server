using CRM.Domain.Enums;
using Kernal.Models;

namespace CRM.Domain.Dtos;

public class AddressDto
{
    public long Id { get; set; }
    public Country Country { get; set; } // Enum for Country

    public string City { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public string ZipCode { get; set; }

    public string LocationCode1 { get; set; }

    public string LocationCode2 { get; set; }

    public string LocationCode3 { get; set; }

    public string FAXNumber { get; set; }

    public string ContactName { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }
    public string BranchCode { get; set; }
    public long BranchId { get; set; }
    public short Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

}

