  CREATE TABLE UserRoles (
    [UserId] UNIQUEIDENTIFIER ,
    [RoleId] INT,
    [AssignedAt] DATETIME2 DEFAULT GETDATE(),
    [AssignedBy] UNIQUEIDENTIFIER NULL, -- Qui a assigné ce rôle
    --constraint
	CONSTRAINT [PK_UserRoles] PRIMARY KEY (UserId, RoleId),
	--Relations
	CONSTRAINT [FK_UserRoles_UserId_User.Id] FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE,
	CONSTRAINT [FK_UserRoles_RoleId_Role.Id] FOREIGN KEY (RoleId) REFERENCES Roles(Id) ON DELETE CASCADE
    );