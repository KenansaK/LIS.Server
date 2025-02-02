using CRM.Domain.Auth;
using FluentValidation;
using Kernal.Contracts;

namespace CRM.Application.Validators;

public class AssignRoleToUserModel
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
}
public class AssignRoleToUserValidator : AbstractValidator<AssignRoleToUserModel>
{
    public AssignRoleToUserValidator(IRepository<Role> roleResitory, IRepository<User> userRepository)
    {

        RuleFor(x => x.UserId)
            .MustAsync(async (UserId, cancellation) =>
            {
                return await userRepository.ExistsAsync(UserId);
            })
            .WithMessage("User Id must be valid and exist in the database.");


        RuleFor(x => x.RoleId)
            .MustAsync(async (RoleId, cancellation) =>
            {
                return await roleResitory.ExistsAsync(RoleId);
            })
            .WithMessage("Role Id must be valid and exist in the database.");
    }
}
