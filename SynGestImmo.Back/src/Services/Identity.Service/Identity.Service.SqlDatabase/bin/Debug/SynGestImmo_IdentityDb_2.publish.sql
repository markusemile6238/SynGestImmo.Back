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
/*
La colonne [dbo].[Permissions].[Scope] est en cours de suppression, des données risquent d'être perdues.
*/

IF EXISTS (select top 1 1 from [dbo].[Permissions])
    RAISERROR (N'Lignes détectées. Arrêt de la mise à jour du schéma en raison d''''un risque de perte de données.', 16, 127) WITH NOWAIT

GO
PRINT N'Suppression de Contrainte unique [dbo].[UQ_Permissions_Code_Scope]...';


GO
ALTER TABLE [dbo].[Permissions] DROP CONSTRAINT [UQ_Permissions_Code_Scope];


GO
PRINT N'Modification de Table [dbo].[Permissions]...';


GO
ALTER TABLE [dbo].[Permissions] DROP COLUMN [Scope];


GO
PRINT N'Création de Contrainte unique contrainte sans nom sur [dbo].[Permissions]...';


GO
ALTER TABLE [dbo].[Permissions]
    ADD UNIQUE NONCLUSTERED ([Code] ASC);


GO
PRINT N'Création de Clé étrangère [dbo].[FK_RolePermissions_Permissions]...';


GO
ALTER TABLE [dbo].[RolePermissions] WITH NOCHECK
    ADD CONSTRAINT [FK_RolePermissions_Permissions] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permissions] ([Id]);


GO
PRINT N'Création de Clé étrangère [dbo].[FK_RolePermissions_Roles]...';


GO
ALTER TABLE [dbo].[RolePermissions] WITH NOCHECK
    ADD CONSTRAINT [FK_RolePermissions_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]);


GO
-- ============================================
-- CREATION OF DEFAULT ROLES
-- ============================================

-- Insert roles
INSERT INTO Roles (Name, Description, IsActive, CreatedAt) 
VALUES 
('SU', 'System administrator with all rights', 1, GETDATE()),
('PA', 'Real estate and rental properties administrator', 1, GETDATE()),
('PM', 'Daily property, owner and tenant manager', 1, GETDATE()),
('OWNER', 'Real estate property owner', 1, GETDATE()),
('TENANT', 'Real estate tenant', 1, GETDATE()),
('REPORTER', 'Support employee for incidents and reports', 1, GETDATE()),
('SECRETARY', 'Secretary for administrative management', 1, GETDATE()),
('FM', 'Financial and payment manager', 1, GETDATE()),
('MM', 'Property maintenance and incident manager', 1, GETDATE());

GO

GO
PRINT N'Vérification de données existantes par rapport aux nouvelles contraintes';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[RolePermissions] WITH CHECK CHECK CONSTRAINT [FK_RolePermissions_Permissions];

ALTER TABLE [dbo].[RolePermissions] WITH CHECK CHECK CONSTRAINT [FK_RolePermissions_Roles];


GO
PRINT N'Mise à jour terminée.';


GO
