using Identity.Domain.Dtos;
using Identity.Domain.Entities;
using Identity.Domain.Models;

namespace Identity.Domain.Mapping;
public static class PermissionMapper
{
    public static IEnumerable<PermissionDto?> ToDtos(this IEnumerable<Permission> entities)
    {
        return entities.Select(x => x.ToDto());
    }

    public static PermissionDto ToDto(this Permission entity)
    {
        return new PermissionDto
        {
            Id = entity.Id,
            PermissionName = entity.PermissionName,
            PermissionCode = entity.PermissionCode,
            Module = entity.module
        };
    }

    public static List<Permission> ToEntities(this List<PermissionModel> models)
    {
        if (models != null)
            return models.Select(x => x.ToEntity()).ToList();
        return null;
    }

    public static Permission ToEntity(this PermissionModel model)
    {
        return new Permission
        {
            Id = model.Id,
            PermissionName = model.PermissionName,
            PermissionCode = model.PermissionCode,
            module = model.Module
        };
    }

    public static List<PermissionModel> ToModels(this List<PermissionDto> models)
    {
        if (models != null)
            return models.Select(x => x.ToModel()).ToList();
        return null;
    }

    public static PermissionModel ToModel(this PermissionDto dto)
    {
        return new PermissionModel
        {
            Id = dto.Id,
            PermissionName = dto.PermissionName,
            PermissionCode = dto.PermissionCode,
            Module = dto.Module
        };
    }
}
