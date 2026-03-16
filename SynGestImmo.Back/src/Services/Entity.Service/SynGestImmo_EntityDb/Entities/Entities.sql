CREATE SCHEMA [entity];
GO

CREATE TABLE [entity].[Entities]
(
    [Id] UNIQUEIDENTIFIER ,
    [EntityType] INT NOT NULL,
    [DisplayName] VARCHAR(50) NOT NULL,
    [Email] VARCHAR(150) NOT NULL,
    [Phone] VARCHAR(80) NOT NULL,
    [IsActive] BIT DEFAULT 1,
    [CreatedAt] DATETIME DEFAULT SYSUTCDATETIME() NOT NULL,
    [UpdatedAt] DATETIME DEFAULT SYSUTCDATETIME() NULL,
    
    --CONSTRAINT
    CONSTRAINT [FK_Entities_Id] FOREIGN KEY (Id) REFERENCES [Users] (EntityId)
);
    