using CRM.Domain.Dtos;
using CRM.Domain.Entities;
using CRM.Domain.Models;

namespace CRM.Domain.Mapping;
public static class CustomerMapper
{
    public static IEnumerable<CustomerDto?> ToDtos(this IEnumerable<Customer> entities)
    {
        return entities.Select(x => x.ToDto());
    }
    public static CustomerDto? ToDto(this Customer entity)
    {
        return entity == null
            ? null
            : new CustomerDto
            {
                Id = entity.Id,
                CompanyCommercialName = entity.CompanyCommercialName,
                CompanyLegalName = entity.CompanyLegalName,
                BusinessType = entity.BusinessType,
                CustomerCode = entity.CustomerCode,
                CustomerNumber = entity.CustomerNumber,
                RegistrationNumber = entity.RegistrationNumber,
                Status = entity.StatusId,
                BillingExternalCode = entity.BillingExternalCode,
                ExternalCode = entity.ExternalCode,
                StoreBarcodePrefix = entity.StoreBarcodePrefix,
                LogoBase64 = entity.LogoBase64,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy

            };
    }
    public static List<Customer> ToEntities(this List<CustomerModel> models)
    {
        if (models != null)
            return models.Select(x => x.ToEntity()).ToList();
        return null;
    }
    public static Customer ToEntity(this CustomerModel model)
    {
        if (model != null)
        {
            return new Customer
            {
                Id = model.Id,
                CompanyCommercialName = model.CompanyCommercialName,
                CompanyLegalName = model.CompanyLegalName,
                BusinessType = model.BusinessType,
                CustomerCode = model.CustomerCode,
                CustomerNumber = model.CustomerNumber,
                StatusId = model.Status,
                RegistrationNumber = model.RegistrationNumber,
                BillingExternalCode = model.BillingExternalCode,
                ExternalCode = model.ExternalCode,
                StoreBarcodePrefix = model.StoreBarcodePrefix,
                LogoBase64 = model.LogoBase64

            };
        }
        return null;
    }
    public static List<CustomerModel> ToModels(this List<CustomerDto> models)
    {
        if (models != null)
            return models.Select(x => x.ToModel()).ToList();
        return null;
    }
    public static CustomerModel ToModel(this CustomerDto dto)
    {
        if (dto != null)
        {
            return new CustomerModel
            {
                Id = dto.Id,
                CompanyCommercialName = dto.CompanyCommercialName,
                CompanyLegalName = dto.CompanyLegalName,
                BusinessType = dto.BusinessType,
                CustomerCode = dto.CustomerCode,
                CustomerNumber = dto.CustomerNumber,
                RegistrationNumber = dto.RegistrationNumber,
                Status = dto.Status,
                BillingExternalCode = dto.BillingExternalCode,
                ExternalCode = dto.ExternalCode,
                StoreBarcodePrefix = dto.StoreBarcodePrefix,
                LogoBase64 = dto.LogoBase64

            };
        }
        return null;
    }

}
