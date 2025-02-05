using CRM.Domain.Enums;
using Kernal.Models;

namespace CRM.Domain.Entities;
public class Customer : BaseEntity
{
    public string CompanyCommercialName { get; set; } = string.Empty;
    public string CompanyLegalName { get; set; } = string.Empty;
    public BusinessType BusinessType { get; set; }
    public string CustomerCode { get; set; } = string.Empty;
    public string CustomerNumber { get; set; } = string.Empty;
    public string RegistrationNumber { get; set; } = string.Empty;
    public string BillingExternalCode { get; set; } = string.Empty;
    public string ExternalCode { get; set; } = string.Empty;
    public string StoreBarcodePrefix { get; set; } = string.Empty;
    public string LogoBase64 { get; set; } = string.Empty;
    public List<Branch> Branches { get; set; } = new List<Branch>();
}
