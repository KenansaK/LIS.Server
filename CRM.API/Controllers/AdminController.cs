using CRM.Application.Validators;
using FluentValidation;
using Kernal.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Auth.authorize
{
    // [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IValidator<AssignPermissionToRoleModel> _permissionToRoleValidator;
        private readonly IValidator<AssignRoleToUserModel> _roleToUserValidator;

        public AdminController(IRoleService roleService, IValidator<AssignPermissionToRoleModel> perToRoleValidator, IValidator<AssignRoleToUserModel> roleToUserValidator)
        {
            _roleService = roleService;
            _permissionToRoleValidator = perToRoleValidator;
            _roleToUserValidator = roleToUserValidator;
        }

        [HttpPost("create-role")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            await _roleService.CreateRoleAsync(roleName);
            return Ok("Role created successfully.");
        }

        [HttpPost("assign-role-to-user")]
        [AllowAnonymous]
        public async Task<IActionResult> AssignRoleToUser(int userId, int roleId)
        {
            await _roleService.AssignRoleToUserAsync(userId, roleId);
            return Ok("Role assigned to user.");
        }

        [HttpPost("assign-permission-to-role")]
        [AllowAnonymous]
        public async Task<IActionResult> AssignPermissionToRole(int roleId, int permissionId)
        {
            await _roleService.AssignPermissionToRoleAsync(roleId, permissionId);
            return Ok("Permission assigned to role.");
        }

        [HttpPost("create-permission")]
        [AllowAnonymous]
        public async Task<IActionResult> CreatePermission(string permissionName)
        {
            await _roleService.CreatePermissionAsync(permissionName);
            return Ok("Permission created successfully.");
        }
    }
}

