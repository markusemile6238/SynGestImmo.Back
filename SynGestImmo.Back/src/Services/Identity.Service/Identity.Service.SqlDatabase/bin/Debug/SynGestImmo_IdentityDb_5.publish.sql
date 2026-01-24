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
PRINT N'Création de Table [dbo].[AuditLogs]...';


GO
CREATE TABLE [dbo].[AuditLogs] (
    [Id]         UNIQUEIDENTIFIER NOT NULL,
    [UserId]     UNIQUEIDENTIFIER NULL,
    [Action]     NVARCHAR (100)   NOT NULL,
    [EntityType] NVARCHAR (100)   NOT NULL,
    [EntityId]   UNIQUEIDENTIFIER NULL,
    [OldValues]  NVARCHAR (MAX)   NULL,
    [NewValues]  NVARCHAR (MAX)   NULL,
    [CreatedAt]  DATETIME2 (7)    NULL,
    [IPAddress]  NVARCHAR (45)    NULL,
    [UserAgent]  NVARCHAR (500)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Création de Index [dbo].[AuditLogs].[IX_AuditLogs_UserId]...';


GO
CREATE NONCLUSTERED INDEX [IX_AuditLogs_UserId]
    ON [dbo].[AuditLogs]([UserId] ASC);


GO
PRINT N'Création de Index [dbo].[AuditLogs].[IX_AuditLogs_CreatedAt]...';


GO
CREATE NONCLUSTERED INDEX [IX_AuditLogs_CreatedAt]
    ON [dbo].[AuditLogs]([CreatedAt] ASC);


GO
PRINT N'Création de Table [dbo].[PasswordResets]...';


GO
CREATE TABLE [dbo].[PasswordResets] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [Email]     NVARCHAR (150)   NOT NULL,
    [Code]      NVARCHAR (100)   NOT NULL,
    [CreatedAt] DATETIME2 (7)    NULL,
    [ExpiresAt] DATETIME2 (7)    NULL,
    [IsUsed]    BIT              NULL,
    CONSTRAINT [PK_PasswordResets] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Création de Index [dbo].[PasswordResets].[IX_PasswordResets_Email]...';


GO
CREATE NONCLUSTERED INDEX [IX_PasswordResets_Email]
    ON [dbo].[PasswordResets]([Email] ASC);


GO
PRINT N'Création de Index [dbo].[PasswordResets].[IX_PasswordResets_Code]...';


GO
CREATE NONCLUSTERED INDEX [IX_PasswordResets_Code]
    ON [dbo].[PasswordResets]([Code] ASC);


GO
PRINT N'Création de Table [dbo].[Permissions]...';


GO
CREATE TABLE [dbo].[Permissions] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Code]        VARCHAR (50)   NOT NULL,
    [Name]        NVARCHAR (100) NOT NULL,
    [Category]    VARCHAR (50)   NOT NULL,
    [Description] NVARCHAR (500) NOT NULL,
    [CreatedAt]   DATETIME2 (7)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
PRINT N'Création de Table [dbo].[RolePermissions]...';


GO
CREATE TABLE [dbo].[RolePermissions] (
    [RoleId]       INT NOT NULL,
    [PermissionId] INT NOT NULL,
    [AgencyId]     INT NULL,
    CONSTRAINT [PK_RolePermissions] PRIMARY KEY CLUSTERED ([RoleId] ASC, [PermissionId] ASC)
);


GO
PRINT N'Création de Table [dbo].[Roles]...';


GO
CREATE TABLE [dbo].[Roles] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50)  NOT NULL,
    [Description]  NVARCHAR (255) NULL,
    [IsSystemRole] BIT            NULL,
    [CreatedAt]    DATETIME2 (7)  NOT NULL,
    [UpdatedAt]    DATETIME2 (7)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Name] ASC)
);


GO
PRINT N'Création de Table [dbo].[UserRoles]...';


GO
CREATE TABLE [dbo].[UserRoles] (
    [UserId]     UNIQUEIDENTIFIER NOT NULL,
    [RoleId]     INT              NOT NULL,
    [AssignedAt] DATETIME2 (7)    NULL,
    [AssignedBy] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC)
);


GO
PRINT N'Création de Table [dbo].[Users]...';


GO
CREATE TABLE [dbo].[Users] (
    [Id]                 UNIQUEIDENTIFIER NOT NULL,
    [Email]              NVARCHAR (150)   NOT NULL,
    [PasswordHash]       NVARCHAR (255)   NOT NULL,
    [UserRef]            NVARCHAR (50)    NOT NULL,
    [EntityId]           UNIQUEIDENTIFIER NULL,
    [RefreshToken]       NVARCHAR (255)   NULL,
    [RefreshTokenExpiry] DATETIME2 (7)    NULL,
    [MainRoleId]         INT              NULL,
    [IsActive]           BIT              NULL,
    [CreatedAt]          DATETIME2 (7)    NULL,
    [UpdatedAt]          DATETIME2 (7)    NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Email] ASC),
    UNIQUE NONCLUSTERED ([UserRef] ASC)
);


GO
PRINT N'Création de Index [dbo].[Users].[IX_Users_Email]...';


GO
CREATE NONCLUSTERED INDEX [IX_Users_Email]
    ON [dbo].[Users]([Email] ASC);


GO
PRINT N'Création de Index [dbo].[Users].[IX_Users_UserRef]...';


GO
CREATE NONCLUSTERED INDEX [IX_Users_UserRef]
    ON [dbo].[Users]([UserRef] ASC);


GO
PRINT N'Création de Index [dbo].[Users].[IX_Users_EntityId]...';


GO
CREATE NONCLUSTERED INDEX [IX_Users_EntityId]
    ON [dbo].[Users]([EntityId] ASC);


GO
PRINT N'Création de Contrainte par défaut contrainte sans nom sur [dbo].[AuditLogs]...';


GO
ALTER TABLE [dbo].[AuditLogs]
    ADD DEFAULT NEWID() FOR [Id];


GO
PRINT N'Création de Contrainte par défaut contrainte sans nom sur [dbo].[AuditLogs]...';


GO
ALTER TABLE [dbo].[AuditLogs]
    ADD DEFAULT GETDATE() FOR [CreatedAt];


GO
PRINT N'Création de Contrainte par défaut contrainte sans nom sur [dbo].[PasswordResets]...';


GO
ALTER TABLE [dbo].[PasswordResets]
    ADD DEFAULT NEWID() FOR [Id];


GO
PRINT N'Création de Contrainte par défaut contrainte sans nom sur [dbo].[PasswordResets]...';


GO
ALTER TABLE [dbo].[PasswordResets]
    ADD DEFAULT GETDATE() FOR [CreatedAt];


GO
PRINT N'Création de Contrainte par défaut contrainte sans nom sur [dbo].[PasswordResets]...';


GO
ALTER TABLE [dbo].[PasswordResets]
    ADD DEFAULT DATEADD(HOUR, 1, GETDATE()) FOR [ExpiresAt];


GO
PRINT N'Création de Contrainte par défaut contrainte sans nom sur [dbo].[PasswordResets]...';


GO
ALTER TABLE [dbo].[PasswordResets]
    ADD DEFAULT 0 FOR [IsUsed];


GO
PRINT N'Création de Contrainte par défaut contrainte sans nom sur [dbo].[Permissions]...';


GO
ALTER TABLE [dbo].[Permissions]
    ADD DEFAULT GETUTCDATE() FOR [CreatedAt];


GO
PRINT N'Création de Contrainte par défaut contrainte sans nom sur [dbo].[Roles]...';


GO
ALTER TABLE [dbo].[Roles]
    ADD DEFAULT 0 FOR [IsSystemRole];


GO
PRINT N'Création de Contrainte par défaut contrainte sans nom sur [dbo].[Roles]...';


GO
ALTER TABLE [dbo].[Roles]
    ADD DEFAULT GETDATE() FOR [CreatedAt];


GO
PRINT N'Création de Contrainte par défaut contrainte sans nom sur [dbo].[Roles]...';


GO
ALTER TABLE [dbo].[Roles]
    ADD DEFAULT GETDATE() FOR [UpdatedAt];


GO
PRINT N'Création de Contrainte par défaut contrainte sans nom sur [dbo].[UserRoles]...';


GO
ALTER TABLE [dbo].[UserRoles]
    ADD DEFAULT GETDATE() FOR [AssignedAt];


GO
PRINT N'Création de Contrainte par défaut contrainte sans nom sur [dbo].[Users]...';


GO
ALTER TABLE [dbo].[Users]
    ADD DEFAULT NEWID() FOR [Id];


GO
PRINT N'Création de Contrainte par défaut contrainte sans nom sur [dbo].[Users]...';


GO
ALTER TABLE [dbo].[Users]
    ADD DEFAULT 1 FOR [IsActive];


GO
PRINT N'Création de Contrainte par défaut contrainte sans nom sur [dbo].[Users]...';


GO
ALTER TABLE [dbo].[Users]
    ADD DEFAULT GETDATE() FOR [CreatedAt];


GO
PRINT N'Création de Contrainte par défaut contrainte sans nom sur [dbo].[Users]...';


GO
ALTER TABLE [dbo].[Users]
    ADD DEFAULT GETDATE() FOR [UpdatedAt];


GO
PRINT N'Création de Clé étrangère [dbo].[FK_RolePermissions_Roles]...';


GO
ALTER TABLE [dbo].[RolePermissions] WITH NOCHECK
    ADD CONSTRAINT [FK_RolePermissions_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]);


GO
PRINT N'Création de Clé étrangère [dbo].[FK_RolePermissions_Permissions]...';


GO
ALTER TABLE [dbo].[RolePermissions] WITH NOCHECK
    ADD CONSTRAINT [FK_RolePermissions_Permissions] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permissions] ([Id]);


GO
PRINT N'Création de Clé étrangère [dbo].[FK_UserRoles_UserId_User.Id]...';


GO
ALTER TABLE [dbo].[UserRoles] WITH NOCHECK
    ADD CONSTRAINT [FK_UserRoles_UserId_User.Id] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE;


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
ALTER TABLE [dbo].[RolePermissions] WITH CHECK CHECK CONSTRAINT [FK_RolePermissions_Roles];

ALTER TABLE [dbo].[RolePermissions] WITH CHECK CHECK CONSTRAINT [FK_RolePermissions_Permissions];

ALTER TABLE [dbo].[UserRoles] WITH CHECK CHECK CONSTRAINT [FK_UserRoles_UserId_User.Id];

ALTER TABLE [dbo].[UserRoles] WITH CHECK CHECK CONSTRAINT [FK_UserRoles_RoleId_Role.Id];

ALTER TABLE [dbo].[Users] WITH CHECK CHECK CONSTRAINT [FK_Users_MainRoleId_Roles.Id];


GO
PRINT N'Mise à jour terminée.';


GO
