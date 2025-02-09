using Identity.Domain.Entities;
using FluentValidation;
using Kernal.Contracts;
namespace Identity.Application.Validators;

public class AssignPermissionToRoleModel
{
    public int PermissionId { get; set; }
    public int RoleId { get; set; }
}
public class AssignPermissionToRoleValidator : AbstractValidator<AssignPermissionToRoleModel>
{
    public AssignPermissionToRoleValidator(IRepository<Role> roleResitory, IRepository<Permission> permissionRepository)
    {

        RuleFor(x => x.PermissionId)
            .MustAsync(async (PermissionId, cancellation) =>
            {
                return await permissionRepository.ExistsAsync(PermissionId);
            })
            .WithMessage("Permission Id must be valid and exist in the database.");


        RuleFor(x => x.RoleId)
            .MustAsync(async (RoleId, cancellation) =>
            {
                return await roleResitory.ExistsAsync(RoleId);
            })
            .WithMessage("Role Id must be valid and exist in the database.");
    }
}