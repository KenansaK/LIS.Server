using Identity.Domain.Dtos;
using Identity.Domain.Entities;
using Identity.Domain.Models;

namespace Identity.Domain.mapping;
public static class RoleMapper
{
    public static IEnumerable<RoleDto?> ToDtos(this IEnumerable<Role> entities)
    {
        return entities.Select(x => x.ToDto());
    }
    public static RoleDto ToDto(this Role entity)
    {
        return new RoleDto
        {
            Id = entity.Id,
            Name = entity.Name,
            status = entity.StatusId
        };
    }
    public static List<Role> ToEntities(this List<RoleModel> models)
    {
        if (models != null)
            return models.Select(x => x.ToEntity()).ToList();
        return null;
    }
    public static Role ToEntity(this RoleModel model)
    {
        return new Role
        {
            Id = model.Id,
            Name = model.Name,




        };
    }
    public static List<RoleModel> ToModels(this List<RoleDto> models)
    {
        if (models != null)
            return models.Select(x => x.ToModel()).ToList();
        return null;
    }

    // UserCreateDto -> UserModel
    public static RoleModel ToModel(this RoleDto dto)
    {
        return new RoleModel
        {
            Id = dto.Id,
            Name = dto.Name,
        };
    }


}
