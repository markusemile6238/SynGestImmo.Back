-- ============================================
-- ROLE PERMISSIONS INSERT SCRIPT
-- Project: SynGestImmo
-- Roles based on provided list
-- ============================================

-- Suppression des anciennes associations (si nécessaire)
-- DELETE FROM RolePermissions;
-- DBCC CHECKIDENT ('RolePermissions', RESEED, 0);

-- ============================================
-- RÔLE 1: SUPER UTILISATEUR (Toutes les permissions)
-- ============================================
DECLARE @SuperUtilisateurRoleId INT = 1;

INSERT INTO RolePermissions (RoleId, PermissionId)
SELECT @SuperUtilisateurRoleId, Id 
FROM Permissions;

PRINT 'Super Utilisateur: ' + CAST(@@ROWCOUNT AS VARCHAR) + ' permissions assignées';

-- ============================================
-- RÔLE 2: PROPERTIES ADMINISTRATOR
-- ============================================
DECLARE @PropertiesAdminRoleId INT = 2;

INSERT INTO RolePermissions (RoleId, PermissionId)
SELECT @PropertiesAdminRoleId, p.Id 
FROM Permissions p
WHERE p.Category IN (
    'Property Management',
    'Owner Management', 
    'Tenant Management',
    'Subdeed Management',
    'Lease Management',
    'Incident Management',
    'Report Management',
    'Payment Management',
    'Document Management'
) OR p.Code IN (
    -- Gestion utilisateurs limitée
    'USER_VIEW',
    'USER_CREATE',
    'USER_EDIT',
    'USER_ACTIVATE_DEACTIVATE',
    'USER_RESET_PASSWORD',
    -- Gestion employés
    'EMPLOYEE_VIEW',
    'EMPLOYEE_CREATE',
    'EMPLOYEE_EDIT',
    -- Gestion rôles limitée
    'ROLE_VIEW',
    -- Notifications
    'NOTIFICATION_SEND',
    'NOTIFICATION_MANAGE'
);

PRINT 'Properties Administrator: ' + CAST(@@ROWCOUNT AS VARCHAR) + ' permissions assignées';

-- ============================================
-- RÔLE 3: PROPERTY MANAGER
-- ============================================
DECLARE @PropertyManagerRoleId INT = 3;

INSERT INTO RolePermissions (RoleId, PermissionId)
SELECT @PropertyManagerRoleId, p.Id 
FROM Permissions p
WHERE p.Category IN (
    'Tenant Management',
    'Lease Management',
    'Incident Management',
    'Report Management',
    'Payment Management',
    'Document Management'
) OR p.Code IN (
    -- Vue sur les biens
    'PROPERTY_VIEW',
    'PROPERTY_DETAIL_VIEW',
    -- Vue sur les propriétaires
    'OWNER_VIEW',
    'OWNER_PROPERTIES_VIEW',
    -- Vue sur les sous-titres
    'SUBDEED_VIEW',
    -- Gestion des rapports
    'REPORT_CREATE',
    'REPORT_VIEW',
    'REPORT_EXPORT',
    -- Notifications
    'NOTIFICATION_SEND'
);

PRINT 'Property Manager: ' + CAST(@@ROWCOUNT AS VARCHAR) + ' permissions assignées';

-- ============================================
-- RÔLE 4: OWNER
-- ============================================
DECLARE @OwnerRoleId INT = 4;

INSERT INTO RolePermissions (RoleId, PermissionId)
SELECT @OwnerRoleId, p.Id 
FROM Permissions p
WHERE p.Code IN (
    -- Vue sur SES propres biens
    'PROPERTY_VIEW',
    'PROPERTY_DETAIL_VIEW',
    -- Vue sur SES locataires
    'TENANT_VIEW',
    'TENANT_CONTRACT_VIEW',
    -- Vue sur SES baux
    'LEASE_VIEW',
    'LEASE_PAYMENTS_VIEW',
    -- Gestion des incidents
    'INCIDENT_CREATE',
    'INCIDENT_VIEW',
    'INCIDENT_COMMENT',
    -- Vue des rapports et analytics
    'REPORT_VIEW',
    'REPORT_ANALYTICS_VIEW',
    -- Vue des paiements
    'PAYMENT_VIEW',
    -- Gestion des documents
    'DOCUMENT_VIEW',
    'DOCUMENT_DOWNLOAD',
    'DOCUMENT_UPLOAD',
    'DOCUMENT_SHARE'
);

PRINT 'Owner: ' + CAST(@@ROWCOUNT AS VARCHAR) + ' permissions assignées';

-- ============================================
-- RÔLE 5: TENANT
-- ============================================
DECLARE @TenantRoleId INT = 5;

INSERT INTO RolePermissions (RoleId, PermissionId)
SELECT @TenantRoleId, p.Id 
FROM Permissions p
WHERE p.Code IN (
    -- Vue sur SON bail
    'LEASE_VIEW',
    'LEASE_PAYMENTS_VIEW',
    -- Gestion des incidents (création et vue)
    'INCIDENT_CREATE',
    'INCIDENT_VIEW',
    'INCIDENT_COMMENT',
    -- Vue des paiements
    'PAYMENT_VIEW',
    -- Gestion des documents
    'DOCUMENT_VIEW',
    'DOCUMENT_DOWNLOAD',
    'DOCUMENT_UPLOAD'
);

PRINT 'Tenant: ' + CAST(@@ROWCOUNT AS VARCHAR) + ' permissions assignées';

-- ============================================
-- RÔLE 6: REPORTER
-- ============================================
DECLARE @ReporterRoleId INT = 6;

INSERT INTO RolePermissions (RoleId, PermissionId)
SELECT @ReporterRoleId, p.Id 
FROM Permissions p
WHERE p.Category IN (
    'Incident Management',
    'Report Management'
) OR p.Code IN (
    -- Vue basique pour contexte
    'TENANT_VIEW',
    'PROPERTY_VIEW',
    'PROPERTY_DETAIL_VIEW',
    -- Documents liés aux incidents
    'DOCUMENT_VIEW',
    'DOCUMENT_UPLOAD',
    'DOCUMENT_DOWNLOAD'
);

PRINT 'Reporter: ' + CAST(@@ROWCOUNT AS VARCHAR) + ' permissions assignées';

-- ============================================
-- RÔLE 7: SECRETARY
-- ============================================
DECLARE @SecretaryRoleId INT = 7;

INSERT INTO RolePermissions (RoleId, PermissionId)
SELECT @SecretaryRoleId, p.Id 
FROM Permissions p
WHERE p.Category IN (
    'Agenda Management',
    'Meeting Management',
    'Decision Management',
    'Notifications',
    'Document Management'
) OR p.Code IN (
    -- Vue administrative
    'USER_VIEW',
    'EMPLOYEE_VIEW',
    'TENANT_VIEW',
    'OWNER_VIEW',
    'PROPERTY_VIEW',
    -- Gestion des réunions et décisions
    'MEETING_CREATE',
    'MEETING_EDIT',
    'MEETING_INVITE',
    'MEETING_ATTENDANCE_MANAGE',
    'DECISION_CREATE',
    'DECISION_EDIT',
    -- Notifications
    'NOTIFICATION_SEND',
    'EMAIL_TEMPLATE_MANAGE'
);

PRINT 'Secretary: ' + CAST(@@ROWCOUNT AS VARCHAR) + ' permissions assignées';

-- ============================================
-- RÔLE 8: FINANCIAL MANAGER
-- ============================================
DECLARE @FinancialManagerRoleId INT = 8;

INSERT INTO RolePermissions (RoleId, PermissionId)
SELECT @FinancialManagerRoleId, p.Id 
FROM Permissions p
WHERE p.Category IN (
    'Payment Management',
    'Report Management'
) OR p.Code IN (
    -- Accès aux données financières
    'LEASE_VIEW',
    'LEASE_PAYMENTS_VIEW',
    'TENANT_VIEW',
    'OWNER_VIEW',
    'PROPERTY_VIEW',
    -- Gestion complète des paiements
    'PAYMENT_CREATE',
    'PAYMENT_EDIT',
    'PAYMENT_DELETE',
    'PAYMENT_VALIDATE',
    'PAYMENT_EXPORT',
    -- Rapports et analytics
    'REPORT_VIEW',
    'REPORT_EXPORT',
    'REPORT_ANALYTICS_VIEW',
    'REPORT_GENERATE',
    -- Documents financiers
    'DOCUMENT_VIEW',
    'DOCUMENT_DOWNLOAD',
    'DOCUMENT_UPLOAD'
);

PRINT 'Financial Manager: ' + CAST(@@ROWCOUNT AS VARCHAR) + ' permissions assignées';

-- ============================================
-- RÔLE 9: MAINTENANCE
-- ============================================
DECLARE @MaintenanceRoleId INT = 9;

INSERT INTO RolePermissions (RoleId, PermissionId)
SELECT @MaintenanceRoleId, p.Id 
FROM Permissions p
WHERE p.Category IN (
    'Incident Management'
) OR p.Code IN (
    -- Vue des propriétés pour maintenance
    'PROPERTY_VIEW',
    'PROPERTY_DETAIL_VIEW',
    -- Gestion complète des incidents
    'INCIDENT_VIEW',
    'INCIDENT_CREATE',
    'INCIDENT_EDIT',
    'INCIDENT_ASSIGN',
    'INCIDENT_RESOLVE',
    'INCIDENT_COMMENT',
    'INCIDENT_PRIORITY_CHANGE',
    -- Rapports de maintenance
    'REPORT_CREATE',
    'REPORT_VIEW',
    'REPORT_EXPORT',
    -- Documents techniques
    'DOCUMENT_VIEW',
    'DOCUMENT_DOWNLOAD',
    'DOCUMENT_UPLOAD',
    'DOCUMENT_EDIT'
);

PRINT 'Maintenance: ' + CAST(@@ROWCOUNT AS VARCHAR) + ' permissions assignées';

-- ============================================
-- RÔLE 12: ACCOUNTANT
-- ============================================
DECLARE @AccountantRoleId INT = 12;

INSERT INTO RolePermissions (RoleId, PermissionId)
SELECT @AccountantRoleId, p.Id 
FROM Permissions p
WHERE p.Category IN (
    'Payment Management',
    'Report Management'
) OR p.Code IN (
    -- Données financières
    'LEASE_VIEW',
    'LEASE_PAYMENTS_VIEW',
    'PAYMENT_VIEW',
    'PAYMENT_CREATE',
    'PAYMENT_EDIT',
    'PAYMENT_VALIDATE',
    'PAYMENT_EXPORT',
    -- Rapports comptables
    'REPORT_VIEW',
    'REPORT_CREATE',
    'REPORT_EXPORT',
    'REPORT_GENERATE',
    'REPORT_ANALYTICS_VIEW',
    -- Documents comptables
    'DOCUMENT_VIEW',
    'DOCUMENT_DOWNLOAD',
    'DOCUMENT_UPLOAD',
    -- Vue contextuelle
    'TENANT_VIEW',
    'OWNER_VIEW',
    'PROPERTY_VIEW'
);

PRINT 'Accountant: ' + CAST(@@ROWCOUNT AS VARCHAR) + ' permissions assignées';

---- ============================================
---- VÉRIFICATION DES ASSIGNATIONS
---- ============================================
--SELECT 
--    r.Name AS 'Rôle',
--    r.Id AS 'ID Rôle',
--    COUNT(rp.PermissionId) AS 'Nombre de permissions'
--FROM Roles r
--LEFT JOIN RolePermissions rp ON r.Id = rp.RoleId
--WHERE r.Id IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 12)
--GROUP BY r.Id, r.Name
--ORDER BY r.Id;

---- ============================================
---- DÉTAIL DES PERMISSIONS PAR RÔLE
---- ============================================
--SELECT 
--    r.Name AS 'Rôle',
--    p.Code AS 'Permission Code',
--    p.Name AS 'Permission Name',
--    p.Category AS 'Category'
--FROM Roles r
--JOIN RolePermissions rp ON r.Id = rp.RoleId
--JOIN Permissions p ON rp.PermissionId = p.Id
--WHERE r.Id = 3  -- Changer l'ID pour voir un rôle spécifique
--ORDER BY p.Category, p.Code;

---- ============================================
---- RÉSUMÉ PAR CATÉGORIE POUR CHAQUE RÔLE
---- ============================================
--SELECT 
--    r.Name AS 'Rôle',
--    p.Category AS 'Catégorie',
--    COUNT(p.Id) AS 'Nombre de permissions',
--    STRING_AGG(p.Code, ', ') AS 'Permissions'
--FROM Roles r
--JOIN RolePermissions rp ON r.Id = rp.RoleId
--JOIN Permissions p ON rp.PermissionId = p.Id
--WHERE r.Id IN (2, 3, 4, 5, 6, 7, 8, 9, 12)
--GROUP BY r.Id, r.Name, p.Category
--ORDER BY r.Id, p.Category;