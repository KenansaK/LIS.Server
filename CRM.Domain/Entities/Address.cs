using CRM.Domain.Enums;
using Kernal.Models;

namespace CRM.Domain.Entities;

public class Address : BaseEntity
{

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
    public long BranchId {  get; set; }
    public string BranchCode {  get; set; }
    public Branch Branch { get; set; }

}
