using CRM.Domain.Enums;
using Kernal.Models;

namespace CRM.Domain.Entities;

public class Address : BaseEntity
{

    public Country Country { get; set; } // Enum for Country

    public string City { get; set; } = string.Empty;

    public string AddressLine1 { get; set; } = string.Empty;

    public string AddressLine2 { get; set; } = string.Empty;

    public string ZipCode { get; set; } = string.Empty;

    public string LocationCode1 { get; set; } = string.Empty;

    public string LocationCode2 { get; set; } = string.Empty;

    public string LocationCode3 { get; set; } = string.Empty;

    public string FAXNumber { get; set; } = string.Empty;

    public string ContactName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;
    public long BranchId { get; set; }
    public Branch Branch { get; set; } = null!;

}
