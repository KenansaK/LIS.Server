using CRM.Domain.Enums;
using Kernal.Models;

namespace CRM.Domain.Entities;
public class Customer : BaseEntity
{
    public string CompanyCommercialName { get; set; }
    public string CompanyLegalName { get; set; }
    public BusinessType BusinessType { get; set; }
    public string CustomerCode { get; set; }
    public string CustomerNumber { get; set; }
    public string RegistrationNumber { get; set; }
    public string BillingExternalCode { get; set; }
    public string ExternalCode { get; set; }
    public string StoreBarcodePrefix { get; set; }
    public string LogoBase64 { get; set; }
    public List<Branch>? Branches { get; set; }
}
