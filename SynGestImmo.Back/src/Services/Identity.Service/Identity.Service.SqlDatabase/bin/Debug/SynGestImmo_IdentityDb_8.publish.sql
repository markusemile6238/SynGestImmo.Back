/*
Script de déploiement pour SynGestImmo_Identity

Ce code a été généré par un outil.
La modification de ce fichier peut provoquer un comportement incorrect et sera perdue si
le code est régénéré.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "SynGestImmo_Identity"
:setvar DefaultFilePrefix "SynGestImmo_Identity"
:setvar DefaultDataPath "/var/opt/mssql/data/"
:setvar DefaultLogPath "/var/opt/mssql/data/"

GO
:on error exit
GO
/*
Détectez le mode SQLCMD et désactivez l'exécution du script si le mode SQLCMD n'est pas pris en charge.
Pour réactiver le script une fois le mode SQLCMD activé, exécutez ce qui suit :
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'Le mode SQLCMD doit être activé de manière à pouvoir exécuter ce script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Suppression de Contrainte par défaut contrainte sans nom sur [dbo].[Roles]...';


GO
ALTER TABLE [dbo].[Roles] DROP CONSTRAINT [DF__Roles__IsSystemR__22751F6C];


GO
PRINT N'Suppression de Contrainte par défaut contrainte sans nom sur [dbo].[Roles]...';


GO
ALTER TABLE [dbo].[Roles] DROP CONSTRAINT [DF__Roles__CreatedAt__236943A5];


GO
PRINT N'Suppression de Contrainte par défaut contrainte sans nom sur [dbo].[Roles]...';


GO
ALTER TABLE [dbo].[Roles] DROP CONSTRAINT [DF__Roles__UpdatedAt__245D67DE];


GO
PRINT N'Suppression de Clé étrangère [dbo].[FK_RolePermissions_Roles]...';


GO
ALTER TABLE [dbo].[RolePermissions] DROP CONSTRAINT [FK_RolePermissions_Roles];


GO
PRINT N'Suppression de Clé étrangère [dbo].[FK_UserRoles_RoleId_Role.Id]...';


GO
ALTER TABLE [dbo].[UserRoles] DROP CONSTRAINT [FK_UserRoles_RoleId_Role.Id];


GO
PRINT N'Suppression de Clé étrangère [dbo].[FK_Users_MainRoleId_Roles.Id]...';


GO
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_MainRoleId_Roles.Id];


GO
PRINT N'Début de la régénération de la table [dbo].[Roles]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Roles] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50)  NOT NULL,
    [Description]  NVARCHAR (255) NULL,
    [IsSystemRole] BIT            DEFAULT 0 NULL,
    [IsActive]     BIT            DEFAULT 1 NULL,
    [CreatedAt]    DATETIME2 (7)  DEFAULT GETDATE() NOT NULL,
    [UpdatedAt]    DATETIME2 (7)  DEFAULT GETDATE() NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Roles])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Roles] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Roles] ([Id], [Name], [Description], [IsSystemRole], [CreatedAt], [UpdatedAt])
        SELECT   [Id],
                 [Name],
                 [Description],
                 [IsSystemRole],
                 [CreatedAt],
                 [UpdatedAt]
        FROM     [dbo].[Roles]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Roles] OFF;
    END

DROP TABLE [dbo].[Roles];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Roles]', N'Roles';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Création de Clé étrangère [dbo].[FK_RolePermissions_Roles]...';


GO
ALTER TABLE [dbo].[RolePermissions] WITH NOCHECK
    ADD CONSTRAINT [FK_RolePermissions_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]);


GO
PRINT N'Création de Clé étrangère [dbo].[FK_UserRoles_RoleId_Role.Id]...';


GO
ALTER TABLE [dbo].[UserRoles] WITH NOCHECK
    ADD CONSTRAINT [FK_UserRoles_RoleId_Role.Id] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]) ON DELETE CASCADE;


GO
PRINT N'Création de Clé étrangère [dbo].[FK_Users_MainRoleId_Roles.Id]...';


GO
ALTER TABLE [dbo].[Users] WITH NOCHECK
    ADD CONSTRAINT [FK_Users_MainRoleId_Roles.Id] FOREIGN KEY ([MainRoleId]) REFERENCES [dbo].[Roles] ([Id]);


GO
PRINT N'Actualisation de Vue [dbo].[RolesList]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[RolesList]';


GO
-- ============================================
-- CREATION OF DEFAULT ROLES
-- ============================================

-- Insert roles
INSERT INTO Roles (Name, Description,IsSystemRole, IsActive, CreatedAt) 
VALUES 
('SU', 'System administrator with all rights', 1, 1, GETDATE()),
('PA', 'Real estate and rental properties administrator', 1, 1, GETDATE()),
('PM', 'Daily property, owner and tenant manager', 1, 1, GETDATE()),
('OWNER', 'Real estate property owner', 1, 1, GETDATE()),
('TENANT', 'Real estate tenant', 1, 1, GETDATE()),
('REPORTER', 'Support employee for incidents and reports', 1, 1, GETDATE()),
('SECRETARY', 'Secretary for administrative management', 0,  1, GETDATE()),
('FM', 'Financial and payment manager', 0, 1, GETDATE()),
('MM', 'Property maintenance and incident manager', 0, 1, GETDATE());

GO

GO
PRINT N'Vérification de données existantes par rapport aux nouvelles contraintes';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[RolePermissions] WITH CHECK CHECK CONSTRAINT [FK_RolePermissions_Roles];

ALTER TABLE [dbo].[UserRoles] WITH CHECK CHECK CONSTRAINT [FK_UserRoles_RoleId_Role.Id];

ALTER TABLE [dbo].[Users] WITH CHECK CHECK CONSTRAINT [FK_Users_MainRoleId_Roles.Id];


GO
PRINT N'Mise à jour terminée.';


GO
