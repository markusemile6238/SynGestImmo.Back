PRINT 'Creating Identity Database...';
GO

-- creation de la base de donnée
USE master;
GO

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name='SynGestImmo_Identity')
BEGIN
	CREATE DATABASE SynGestImmo_Identity;
	PRINT 'Database SynGestImmo_Identity created';
END
ELSE
BEGIN
	PRINT 'Database SynGestImmo_Identity already exist';
END
GO

-- use the new database
USE SynGestImmo_Identity;
GO

IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
BEGIN
    CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) UNIQUE NOT NULL,
    Description NVARCHAR(255),
    IsSystemRole BIT DEFAULT 0, -- Rôle système qu'on ne peut pas supprimer
    CreatedAt DATETIME2 DEFAULT GETDATE(),
    UpdatedAt DATETIME2 DEFAULT GETDATE()
    );

    PRINT 'Table Roles created.';
END
ELSE
BEGIN
    PRINT 'Table Roles already exist.';
END

-- creation tables
PRINT 'Creating the tables...';

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
	CREATE TABLE Users(
		Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
		Email NVARCHAR(150) UNIQUE NOT NULL,
        PasswordHash NVARCHAR(255) NOT NULL,
        UserRef NVARCHAR(50) UNIQUE NOT NULL,
		EntityId UNIQUEIDENTIFIER NULL,
        RefreshToken NVARCHAR(255) NULL,
        RefreshTokenExpiry DATETIME2 NULL,
        MainRoleId int NULL FOREIGN KEY REFERENCES Roles(Id), -- role Principal
        IsActive BIT DEFAULT 1,
        CreatedAt DATETIME2 DEFAULT GETDATE(),
        UpdatedAt DATETIME2 DEFAULT GETDATE()	
	);
	 -- Index
    CREATE INDEX IX_Users_Email ON Users(Email);
    CREATE INDEX IX_Users_UserRef ON Users(UserRef);
    CREATE INDEX IX_Users_EntityId ON Users(EntityId);

    PRINT 'Table Users created.';
END 
ELSE
BEGIN
    PRINT 'Table Users already exist.';
END
GO



IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRoles]') AND type in (N'U'))
BEGIN
    CREATE TABLE UserRoles (
    UserId UNIQUEIDENTIFIER FOREIGN KEY REFERENCES Users(Id) ON DELETE CASCADE,
    RoleId INT FOREIGN KEY REFERENCES Roles(Id) ON DELETE CASCADE,
    AssignedAt DATETIME2 DEFAULT GETDATE(),
    AssignedBy UNIQUEIDENTIFIER NULL, -- Qui a assigné ce rôle
    PRIMARY KEY (UserId, RoleId)
    );

    PRINT 'Table UserRoles created';
END
ELSE
BEGIN
    PRINT 'Table UserRoles already exist.'
END
GO

-- Table PasswordResets
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PasswordResets]') AND type in (N'U'))
BEGIN
    CREATE TABLE PasswordResets (
        Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        Email NVARCHAR(150) NOT NULL,
        Code NVARCHAR(100) NOT NULL,
        CreatedAt DATETIME2 DEFAULT GETDATE(),
        ExpiresAt DATETIME2 DEFAULT DATEADD(HOUR, 1, GETDATE()),
        IsUsed BIT DEFAULT 0
    );
    
    CREATE INDEX IX_PasswordResets_Email ON PasswordResets(Email);
    CREATE INDEX IX_PasswordResets_Code ON PasswordResets(Code);
    
    PRINT 'Table PasswordResets created.';
END
ELSE
BEGIN
    PRINT 'Table PasswordResets already exists.';
END
GO

-- Table AuditLog (optionnel)
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AuditLogs]') AND type in (N'U'))
BEGIN
    CREATE TABLE AuditLogs (
        Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        UserId UNIQUEIDENTIFIER NULL,
        Action NVARCHAR(100) NOT NULL,
        EntityType NVARCHAR(100) NOT NULL,
        EntityId UNIQUEIDENTIFIER NULL,
        OldValues NVARCHAR(MAX) NULL,
        NewValues NVARCHAR(MAX) NULL,
        CreatedAt DATETIME2 DEFAULT GETDATE(),
        IPAddress NVARCHAR(45) NULL,
        UserAgent NVARCHAR(500) NULL
    );
    
    CREATE INDEX IX_AuditLogs_UserId ON AuditLogs(UserId);
    CREATE INDEX IX_AuditLogs_CreatedAt ON AuditLogs(CreatedAt);
    
    PRINT 'Table AuditLogs created.';
END
ELSE
BEGIN
    PRINT 'Table AuditLogs already exists.';
END
GO

-- 4. Créer les stored procedures
PRINT 'Creating stored procedures...';
-- Procédure pour créer un utilisateur
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_CreateUser]') AND type in (N'P', N'PC'))
BEGIN
    EXEC('
    CREATE PROCEDURE usp_CreateUser
        @Email NVARCHAR(150),
        @PasswordHash NVARCHAR(255),
        @UserRef NVARCHAR(50),
        @MainRoleId INT,
        @EntityId UNIQUEIDENTIFIER = NULL
    AS
    BEGIN
        INSERT INTO Users (Email, PasswordHash, UserRef, MainRoleId, EntityId)
        OUTPUT Inserted.Id
        VALUES (@Email, @PasswordHash, @UserRef, @MainRoleId, @EntityId);
    END');
    PRINT 'Stored procedure usp_CreateUser created.';
END
GO
PRINT 'Inserting default roles...';
IF NOT EXISTS (SELECT 1 FROM Roles)
BEGIN
-- 5 Rôles par défaut
INSERT INTO Roles (Name, Description, IsSystemRole) VALUES
('Syndic', 'Administrateur principal du système', 1),
('Manager', 'Gestionnaire d''immeuble', 1),
('Employee', 'Employé du syndic', 1),
('Owner', 'Propriétaire', 1),
('Lodger', 'Locataire', 1),
('Auditor', 'Auditeur comptable', 0),
('Technician', 'Technicien de maintenance', 0),
('Secretary', 'Secrétaire', 0);
END
ELSE
BEGIN
    PRINT 'Roles already exist, skipping insertion.';
END
GO

-- 5. Vue pratique pour avoir les rôles en string
PRINT 'Creating/Updating view vw_UserWithRoles...';
EXEC('
CREATE OR ALTER VIEW vw_UserWithRoles AS
SELECT 
    u.*,
    ISNULL(STRING_AGG(r.Name, ', ') AS RolesList,
    r_main.Name AS MainRoleName
FROM Users u
LEFT JOIN UserRoles ur ON u.Id = ur.UserId
LEFT JOIN Roles r ON ur.RoleId = r.Id
LEFT JOIN Roles r_main ON u.MainRoleId = r_main.Id
GROUP BY u.Id, u.Email, u.PasswordHash, u.UserRef, u.EntityId, 
         u.RefreshToken, u.RefreshTokenExpiry, u.MainRoleId, 
         u.IsActive, u.CreatedAt, u.UpdatedAt, r_main.Name;');
GO

PRINT 'Identity Database Setup Completed Successfully.'