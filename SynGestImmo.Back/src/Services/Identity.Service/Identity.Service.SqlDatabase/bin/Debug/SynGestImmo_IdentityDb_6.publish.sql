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
PRINT N'Mise à jour terminée.';


GO
