CREATE TABLE [dbo].[Permissions]
(
	[Id] INT PRIMARY KEY IDENTITY(1,1),
    [Code] VARCHAR(50) UNIQUE NOT NULL,    -- Ex: "PROPERTY_CREATE"
    [Name] NVARCHAR(100) NOT NULL,         -- Ex: "Créer un bien"
    [Category] VARCHAR(50) NOT NULL,       -- "Property", "Visit", "Contract"
    [Description] NVARCHAR(500) NOT NULL,
    [CreatedAt] DATETIME2 DEFAULT GETUTCDATE()
)
