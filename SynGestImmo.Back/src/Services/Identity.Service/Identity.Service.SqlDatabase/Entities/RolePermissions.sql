CREATE TABLE [dbo].[RolePermissions]
(
	[RoleId] INT NOT NULL,
    [PermissionId] INT NOT NULL,
    [AgencyId] INT NULL,  -- NULL = toutes agences
    CONSTRAINT PK_RolePermissions PRIMARY KEY (RoleId, PermissionId),
    CONSTRAINT FK_RolePermissions_Roles FOREIGN KEY (RoleId) REFERENCES Roles(Id),
    CONSTRAINT FK_RolePermissions_Permissions FOREIGN KEY (PermissionId) REFERENCES Permissions(Id)
)
