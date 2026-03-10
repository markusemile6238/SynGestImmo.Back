CREATE SCHEMA [entity];
GO

CREATE TABLE [entity].[Entities]
(
    [Id] INT NOT NULL PRIMARY KEY,
    [EntityType] VARCHAR(20) NOT NULL,
    [DisplayName] VARCHAR(50) NOT NULL,
    [Email] VARCHAR(150) NOT NULL,
    [Phone] VARCHAR(80) NOT NULL,
    [IsActive] BIT DEFAULT 1,
    [CreatedAt] DATETIME DEFAULT SYSUTCDATETIME() NOT NULL,
    [UpdatedAt] DATETIME DEFAULT SYSUTCDATETIME() NULL,
    
    --CONSTRAINT
    CONSTRAINT [CK_Entities_EntityType] CHECK(EntityType IN('Entites','Owners','Persons','Tenants','Companies'))
);
