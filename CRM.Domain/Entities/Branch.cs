using CRM.Domain.Enums;
using Kernal.Models;

namespace CRM.Domain.Entities;

public class Branch : BaseEntity
{
    public string BranchCode { get; set; } = string.Empty;
    public string BranchName { get; set; } = string.Empty;
    public Currency CurrencyCode { get; set; }
    public string ConsolidationQuery { get; set; } = string.Empty;
    public string VATNumber { get; set; } = string.Empty;
    public string EORI { get; set; } = string.Empty;
    public string IOSS { get; set; } = string.Empty;
    public string LicenseRegistrationNumber { get; set; } = string.Empty;
    public string GPI { get; set; } = string.Empty;
    public string ExternalCode { get; set; } = string.Empty;
    public string BillingExternalCode { get; set; } = string.Empty;
    public Currency AllowedCODCurencies { get; set; }
    public WeightUnit WeightUnit { get; set; }
    public DimensionUnit DimensionUnit { get; set; }
    public string ProductService { get; set; } = string.Empty;
    public string ProductTypeCode { get; set; } = string.Empty;
    public string ShipmentService { get; set; } = string.Empty;
    public string SupplierCode { get; set; } = string.Empty;
    public string CustomerCode { get; set; } = string.Empty;
    public long CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;
    public List<Address> Addresses { get; set; } = new List<Address>();

}