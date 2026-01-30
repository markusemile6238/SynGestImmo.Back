CREATE VIEW [dbo].[RolesList]
	AS SELECT u.*, 
	STRING_AGG(r.Name, ', ') AS RolesList,
    r_main.Name AS MainRoleName
FROM Users u
LEFT JOIN UserRoles ur ON u.Id = ur.UserId
LEFT JOIN Roles r ON ur.RoleId = r.Id
LEFT JOIN Roles r_main ON u.MainRoleId = r_main.Id
GROUP BY u.Id, u.Email, u.PasswordHash, u.UserRef, u.EntityId, 
          u.MainRoleId, u.IsActive, u.CreatedAt, u.UpdatedAt, r_main.Name
GO
