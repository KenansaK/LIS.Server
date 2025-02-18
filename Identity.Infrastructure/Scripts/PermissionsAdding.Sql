-- Step 1: Insert Permissions (with StatusId)
INSERT INTO "Permissions" ("PermissionCode", "PermissionName", "CreatedAt", "StatusId") VALUES
  ('ViewCustomer', 'View Customer', CURRENT_TIMESTAMP, 1),
  ('CreateCustomer', 'Create Customer', CURRENT_TIMESTAMP, 1),
  ('EditCustomer', 'Edit Customer', CURRENT_TIMESTAMP, 1),
  ('DeleteCustomer', 'Delete Customer', CURRENT_TIMESTAMP, 1),
  ('DeleteBranch', 'Delete Branch', CURRENT_TIMESTAMP, 1),
  ('ViewBranch', 'View Branch', CURRENT_TIMESTAMP, 1),
  ('EditBranch', 'Edit Branch', CURRENT_TIMESTAMP, 1),
  ('CreateBranch', 'Create Branch', CURRENT_TIMESTAMP, 1),
  ('ViewAddress', 'View Address', CURRENT_TIMESTAMP, 1),
  ('CreateAddress', 'Create Address', CURRENT_TIMESTAMP, 1),
  ('EditAddress', 'Edit Address', CURRENT_TIMESTAMP, 1),
  ('DeleteAddress', 'Delete Address', CURRENT_TIMESTAMP, 1),
  ('ViewPermission', 'View Permission', CURRENT_TIMESTAMP, 1),
  ('ViewRole', 'View Role', CURRENT_TIMESTAMP, 1),
  ('CreateRole', 'Create Role', CURRENT_TIMESTAMP, 1),
  ('EditRole', 'Edit Role', CURRENT_TIMESTAMP, 1),
  ('DeleteRole', 'Delete Role', CURRENT_TIMESTAMP, 1),
  ('AssignPermissionToRole', 'Assign Permission To Role', CURRENT_TIMESTAMP, 1),
  ('DeletePermissionFromRole', 'Delete Permission From Role', CURRENT_TIMESTAMP, 1),
  ('ViewPermissionsForRole', 'View Permissions For Role', CURRENT_TIMESTAMP, 1),
  ('ViewUser', 'View User', CURRENT_TIMESTAMP, 1),
  ('CreateUser', 'Create User', CURRENT_TIMESTAMP, 1),
  ('EditUser', 'Edit User', CURRENT_TIMESTAMP, 1),
  ('DeleteUser', 'Delete User', CURRENT_TIMESTAMP, 1);

-- Step 2: Insert Manager Role (with StatusId)
INSERT INTO "Roles" ("Name", "CreatedAt", "StatusId") 
VALUES 
  ('Manager', CURRENT_TIMESTAMP, 1);


-- Step 3: Insert RolePermissions for Manager Role
-- Assuming the Manager role has RoleId = 1 (You may need to adjust this if the ID is different)

-- Get the RoleId of 'Manager' role
WITH ManagerRole AS (
    SELECT "Id" FROM "Roles" WHERE "Name" = 'Manager'
)
-- Insert RolePermissions for Manager Role
INSERT INTO "RolePermissions" ("RoleId", "PermissionId", "AssignedAt", "CreatedAt", "StatusId")
SELECT 
    ManagerRole."Id", 
    p."Id", 
    CURRENT_TIMESTAMP, 
    CURRENT_TIMESTAMP,
    1  -- Set StatusId to 1 (assuming 1 means active)
FROM 
    ManagerRole, "Permissions" p
WHERE 
    p."PermissionCode" IN (
                           'ViewCustomer', 'CreateCustomer', 'EditCustomer', 'DeleteCustomer',
                           'DeleteBranch', 'ViewBranch', 'EditBranch', 'CreateBranch',
                           'ViewAddress', 'CreateAddress', 'EditAddress', 'DeleteAddress',
                           'ViewPermission', 'ViewRole', 'CreateRole', 'EditRole', 'DeleteRole',
                           'AssignPermissionToRole', 'DeletePermissionFromRole', 'ViewPermissionsForRole',
                           'ViewUser', 'CreateUser', 'EditUser', 'DeleteUser'

        );
-- creating admin user
INSERT INTO "Users" ("Username", "FullName", "PasswordHash", "Email", "IsEmailConfirmed",
                     "PhoneNumber", "LastLoginTime", "RoleId", "CreatedAt", "StatusId",
                     "CreatedBy")
VALUES ('admin', 'Admin User', 'hashed_password_here', 'admin@example.com', true,
        '1234567890', NULL, (SELECT "Id" FROM "Roles" WHERE "Name" = 'Manager'),
        CURRENT_TIMESTAMP, 1, 'System');



