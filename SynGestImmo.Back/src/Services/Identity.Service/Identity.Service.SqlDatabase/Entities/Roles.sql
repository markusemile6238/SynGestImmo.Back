 CREATE TABLE Roles (
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [Name] NVARCHAR(50) UNIQUE NOT NULL,
	[Prefix] VARCHAR(10) UNIQUE NULL,
    [Description] NVARCHAR(255),
    [IsSystemRole] BIT DEFAULT 0, -- Rôle système qu'on ne peut pas supprimer
	[IsActive] BIT DEFAULT 1,
    [CreatedAt] DATETIME2 DEFAULT GETDATE() NOT NULL,
    [UpdatedAt] DATETIME2 DEFAULT GETDATE() NULL
    );
