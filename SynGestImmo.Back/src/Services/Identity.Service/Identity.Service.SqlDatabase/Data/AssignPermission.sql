-- ============================================
-- ASSIGN PERMISSIONS TO SUPER ADMINISTRATOR
-- ============================================
DECLARE @SuperAdminRoleId INT = (SELECT Id FROM Roles WHERE Name = 'SU');

INSERT INTO RolePermissions (RoleId, PermissionId)
SELECT @SuperAdminRoleId, Id 
FROM Permissions;

