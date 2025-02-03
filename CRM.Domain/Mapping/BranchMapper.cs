using CRM.Domain.Dtos;
using CRM.Domain.Entities;
using CRM.Domain.Models;

namespace CRM.Domain.Mapping;

public static class BranchMapper
{
    public static IEnumerable<BranchDto?> ToDtos(this IEnumerable<Branch> entities)
    {
        return entities.Select(x => x.ToDto());
    }
    public static BranchDto? ToDto(this Branch entity)
    {
        return entity == null
            ? null
            : new BranchDto
            {
                Id = entity.Id,
                BranchName = entity.BranchName,
                BranchCode = entity.BranchCode,
                CurrencyCode = entity.CurrencyCode,
                ConsolidationQuery = entity.ConsolidationQuery,
                VATNumber = entity.VATNumber,
                EORI = entity.EORI,
                IOSS = entity.IOSS,
                LicenseRegistrationNumber = entity.LicenseRegistrationNumber,
                GPI = entity.GPI,
                ExternalCode = entity.ExternalCode,
                BillingExternalCode = entity.BillingExternalCode,
                AllowedCODCurencies = entity.AllowedCODCurencies,
                WeightUnit = entity.WeightUnit,
                DimensionUnit = entity.DimensionUnit,
                ProductService = entity.ProductService,
                ProductTypeCode = entity.ProductTypeCode,
                ShipmentService = entity.ShipmentService,
                SupplierCode = entity.SupplierCode,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                CustomerCode = entity.CustomerCode,
                Status = entity.StatusId


            };
    }

    public static List<Branch> ToEntities(this List<BranchModel> models)
    {
        if (models != null)
            return models.Select(x => x.ToEntity()).ToList();
        return null;
    }
    public static Branch ToEntity(this BranchModel model)
    {
        if (model != null)
        {
            return new Branch
            {
                Id = model.Id,
                BranchName = model.BranchName,
                BranchCode = model.BranchCode,
                CurrencyCode = model.CurrencyCode,
                ConsolidationQuery = model.ConsolidationQuery,
                VATNumber = model.VATNumber,
                EORI = model.EORI,
                IOSS = model.IOSS,
                LicenseRegistrationNumber = model.LicenseRegistrationNumber,
                GPI = model.GPI,
                ExternalCode = model.ExternalCode,
                BillingExternalCode = model.BillingExternalCode,
                AllowedCODCurencies = model.AllowedCODCurencies,
                WeightUnit = model.WeightUnit,
                DimensionUnit = model.DimensionUnit,
                ProductService = model.ProductService,
                ProductTypeCode = model.ProductTypeCode,
                ShipmentService = model.ShipmentService,
                SupplierCode = model.SupplierCode,
                CustomerCode = model.CustomerCode,
                StatusId = model.Status


            };
        }
        return null;
    }

    public static List<BranchModel> ToModels(this List<BranchDto> models)
    {
        if (models != null)
            return models.Select(x => x.ToModel()).ToList();
        return null;
    }
    public static BranchModel ToModel(this BranchDto dto)
    {
        if (dto != null)
        {
            return new BranchModel
            {
                Id = dto.Id,
                Status = dto.Status,
                BranchName = dto.BranchName,
                BranchCode = dto.BranchCode,
                CurrencyCode = dto.CurrencyCode,
                ConsolidationQuery = dto.ConsolidationQuery,
                VATNumber = dto.VATNumber,
                EORI = dto.EORI,
                IOSS = dto.IOSS,
                LicenseRegistrationNumber = dto.LicenseRegistrationNumber,
                GPI = dto.GPI,
                ExternalCode = dto.ExternalCode,
                BillingExternalCode = dto.BillingExternalCode,
                AllowedCODCurencies = dto.AllowedCODCurencies,
                WeightUnit = dto.WeightUnit,
                DimensionUnit = dto.DimensionUnit,
                ProductService = dto.ProductService,
                ProductTypeCode = dto.ProductTypeCode,
                ShipmentService = dto.ShipmentService,
                SupplierCode = dto.SupplierCode,
                CustomerCode = dto.CustomerCode,



            };
        }
        return null;
    }
}
