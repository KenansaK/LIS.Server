using CRM.Domain.Enums;

namespace CRM.Domain.Dtos;

public class BranchDto
{
    public long Id { get; set; }
    public string BranchCode { get; set; }
    public string BranchName { get; set; }
    public Currency CurrencyCode { get; set; }
    public string ConsolidationQuery { get; set; }
    public string VATNumber { get; set; }
    public string EORI { get; set; }
    public string IOSS { get; set; }
    public short Status { get; set; }
    public string LicenseRegistrationNumber { get; set; }
    public string GPI { get; set; }
    public string ExternalCode { get; set; }
    public string BillingExternalCode { get; set; }
    public Currency AllowedCODCurencies { get; set; }
    public WeightUnit WeightUnit { get; set; }
    public DimensionUnit DimensionUnit { get; set; }
    public string ProductService { get; set; }
    public string ProductTypeCode { get; set; }
    public string ShipmentService { get; set; }
    public string SupplierCode { get; set; }
    public string CustomerCode { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }


}
