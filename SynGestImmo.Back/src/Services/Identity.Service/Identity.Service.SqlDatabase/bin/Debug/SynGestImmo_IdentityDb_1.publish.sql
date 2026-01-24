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
PRINT N'Création de Table [dbo].[Permissions]...';


GO
CREATE TABLE [dbo].[Permissions] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Code]        VARCHAR (50)   NOT NULL,
    [Name]        NVARCHAR (100) NOT NULL,
    [Category]    VARCHAR (50)   NOT NULL,
    [Description] NVARCHAR (500) NULL,
    [CreatedAt]   DATETIME2 (7)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
PRINT N'Création de Contrainte par défaut contrainte sans nom sur [dbo].[Permissions]...';


GO
ALTER TABLE [dbo].[Permissions]
    ADD DEFAULT GETDATE() FOR [CreatedAt];


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
/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT INTO Roles (Name, Description, IsSystemRole) VALUES
('Syndic', 'Administrateur principal du système', 1),
('Manager', 'Gestionnaire d''immeuble', 1),
('Employee', 'Employé du syndic', 1),
('Owner', 'Propriétaire', 1),
('Lodger', 'Locataire', 1),
('Auditor', 'Auditeur comptable', 0),
('Technician', 'Technicien de maintenance', 0),
('Secretary', 'Secrétaire', 0);
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
