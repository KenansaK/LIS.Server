using CRM.Domain.Dtos;
using CRM.Domain.Entities;
using CRM.Domain.Models;

namespace CRM.Domain.Mapping;

public static class AddressMapper
{
    public static IEnumerable<AddressDto?> ToDtos(this IEnumerable<Address> entities)
    {
        return entities.Select(x => x.ToDto());
    }
    public static AddressDto? ToDto(this Address entity)
    {
        return entity == null
            ? null
            : new AddressDto
            {
                Id = entity.Id,
                Country = entity.Country,
                City = entity.City,
                AddressLine1 = entity.AddressLine1,
                AddressLine2 = entity.AddressLine2,
                FAXNumber = entity.FAXNumber,
                BranchCode = entity.BranchCode,
                ContactName = entity.ContactName,
                Email = entity.Email,
                LocationCode1 = entity.LocationCode1,
                LocationCode2 = entity.LocationCode2,
                LocationCode3 = entity.LocationCode3,
                Phone = entity.Phone,
                ZipCode = entity.ZipCode,
                CreatedBy = entity.CreatedBy,
                UpdatedBy = entity.UpdatedBy,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                Status = entity.StatusId


            };
    }

    public static List<Address> ToEntities(this List<AddressModel> models)
    {
        if (models != null)
            return models.Select(x => x.ToEntity()).ToList();
        return null;
    }
    public static Address ToEntity(this AddressModel model)
    {
        if (model != null)
        {
            return new Address
            {
                Id = model.Id,
                Country = model.Country,
                City = model.City,
                AddressLine1 = model.AddressLine1,
                AddressLine2 = model.AddressLine2,
                FAXNumber = model.FAXNumber,
                BranchCode = model.BranchCode,
                ContactName = model.ContactName,
                Email = model.Email,
                LocationCode1 = model.LocationCode1,
                LocationCode2 = model.LocationCode2,
                LocationCode3 = model.LocationCode3,
                Phone = model.Phone,
                ZipCode = model.ZipCode,
                StatusId = model.Status,



            };
        }
        return null;
    }

    public static List<AddressModel> ToModels(this List<AddressDto> models)
    {
        if (models != null)
            return models.Select(x => x.ToModel()).ToList();
        return null;
    }
    public static AddressModel ToModel(this AddressDto dto)
    {
        if (dto != null)
        {
            return new AddressModel
            {
                Id = dto.Id,
                Status = dto.Status,
                Country = dto.Country,
                City = dto.City,
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                FAXNumber = dto.FAXNumber,
                BranchCode = dto.BranchCode,
                ContactName = dto.ContactName,
                Email = dto.Email,
                LocationCode1 = dto.LocationCode1,
                LocationCode2 = dto.LocationCode2,
                LocationCode3 = dto.LocationCode3,
                Phone = dto.Phone,
                ZipCode = dto.ZipCode,




            };
        }
        return null;
    }
}
