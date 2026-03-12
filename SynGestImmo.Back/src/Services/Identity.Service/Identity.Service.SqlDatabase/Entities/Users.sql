CREATE TABLE Users(
		[Id] UNIQUEIDENTIFIER  DEFAULT NEWID(),
		[Username] NVARCHAR(50) UNIQUE NOT NULL,
		[Email] NVARCHAR(150) UNIQUE NOT NULL,
        [PasswordHash] NVARCHAR(255) NOT NULL,
		[MustChangePassword] BIT DEFAULT 1,
        [UserRef] NVARCHAR(50) UNIQUE NOT NULL,
		[EntityId] INT NULL,
        [MainRoleId] int NULL , -- role Principal
        [IsActive] BIT DEFAULT 1,
        [CreatedAt] DATETIME2 DEFAULT GETDATE(),
        [UpdatedAt] DATETIME2 DEFAULT GETDATE()	
		--CONSTRAINT
		CONSTRAINT [PK_Users] PRIMARY KEY (Id),
		CONSTRAINT [FK_Users_MainRoleId_Roles.Id] FOREIGN KEY (MainRoleId) REFERENCES Roles(Id)

	);