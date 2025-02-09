using Identity.Domain.Dtos;
using Identity.Domain.Entities;
using Identity.Domain.Models;

namespace Identity.Domain.mapping;
public static class UserMapper
{
    // --------- USER ---------
    public static IEnumerable<UserDto?> ToDtos(this IEnumerable<User> entities)
    {
        return entities.Select(x => x.ToDto());
    }
    public static UserDto ToDto(this User entity)
    {
        return new UserDto
        {

            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            FullName = entity.FullName,
            Username = entity.Username,
            Id = entity.Id,
            IsEmailConfirmed = entity.IsEmailConfirmed,
            LastLoginTime = entity.LastLoginTime,
            RoleId = entity.RoleId,
            CreatedBy = entity.CreatedBy,
            UpdatedBy = entity.UpdatedBy,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt,
            Status = entity.StatusId,

        };
    }

    public static List<User> ToEntities(this List<UserModel> models)
    {
        if (models != null)
            return models.Select(x => x.ToEntity()).ToList();
        return null;
    }
    public static User ToEntity(this UserModel model)
    {
        return new User
        {
            Username = model.Username,
            Email = model.Email,
            FullName = model.FullName,
            PhoneNumber = model.PhoneNumber,
            PasswordHash = model.Password,
            RoleId = model.RoleId,
            StatusId = model.status



        };
    }
    public static List<UserModel> ToModels(this List<UserDto> models)
    {
        if (models != null)
            return models.Select(x => x.ToModel()).ToList();
        return null;
    }

    // UserCreateDto -> UserModel
    public static UserModel ToModel(this UserDto dto)
    {
        return new UserModel
        {
            Email = dto.Email,
            Username = dto.Username,
            PhoneNumber = dto.PhoneNumber,
            FullName = dto.FullName,
        };
    }
}


