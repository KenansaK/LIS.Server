using CRM.Domain.Enums;

namespace CRM.Domain.Models;
public class CustomerModel
{
    public long Id { get; set; }
    public string CompanyCommercialName { get; set; }
    public string CompanyLegalName { get; set; }
    public BusinessType BusinessType { get; set; }
    public string CustomerCode { get; set; }
    public string CustomerNumber { get; set; }
    public string RegistrationNumber { get; set; }
    public short Status { get; set; }
    public string BillingExternalCode { get; set; }
    public string ExternalCode { get; set; }
    public string StoreBarcodePrefix { get; set; }
    public string LogoBase64 { get; set; }
}
