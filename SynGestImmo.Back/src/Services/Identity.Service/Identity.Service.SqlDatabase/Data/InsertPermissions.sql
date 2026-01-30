-- ============================================
-- PERMISSIONS INSERT SCRIPT - ENGLISH VERSION
-- Project: SynGestImmo
-- ============================================

-- 1. Delete old data (if needed)
-- DELETE FROM Permissions;
-- DBCC CHECKIDENT ('Permissions', RESEED, 0);

-- ============================================
-- CATEGORY 1: USER MANAGEMENT
-- ============================================
INSERT INTO Permissions (Code, Name, Category, Description, CreatedAt) 
VALUES 
('USER_VIEW', 'View users', 'User Management', 'Allows viewing the list of users', GETDATE()),
('USER_CREATE', 'Create user', 'User Management', 'Allows creating a new user', GETDATE()),
('USER_EDIT', 'Edit user', 'User Management', 'Allows modifying user information', GETDATE()),
('USER_DELETE', 'Delete user', 'User Management', 'Allows deleting a user', GETDATE()),
('USER_ACTIVATE_DEACTIVATE', 'Activate/Deactivate user', 'User Management', 'Allows activating or deactivating a user account', GETDATE()),
('USER_RESET_PASSWORD', 'Reset password', 'User Management', 'Allows resetting a user''s password', GETDATE()),
('USER_ASSIGN_ROLE', 'Assign role', 'User Management', 'Allows assigning a role to a user', GETDATE());

-- ============================================
-- CATEGORY 2: EMPLOYEE MANAGEMENT
-- ============================================
INSERT INTO Permissions (Code, Name, Category, Description, CreatedAt) 
VALUES 
('EMPLOYEE_VIEW', 'View employees', 'Employee Management', 'Allows viewing the list of employees', GETDATE()),
('EMPLOYEE_CREATE', 'Create employee', 'Employee Management', 'Allows creating a new employee', GETDATE()),
('EMPLOYEE_EDIT', 'Edit employee', 'Employee Management', 'Allows modifying employee information', GETDATE()),
('EMPLOYEE_DELETE', 'Delete employee', 'Employee Management', 'Allows deleting an employee', GETDATE());

-- ============================================
-- CATEGORY 3: ROLES AND PERMISSIONS
-- ============================================
INSERT INTO Permissions (Code, Name, Category, Description, CreatedAt) 
VALUES 
('ROLE_VIEW', 'View roles', 'Roles and Permissions', 'Allows viewing the list of roles', GETDATE()),
('ROLE_CREATE', 'Create role', 'Roles and Permissions', 'Allows creating a new role', GETDATE()),
('ROLE_EDIT', 'Edit role', 'Roles and Permissions', 'Allows modifying an existing role', GETDATE()),
('ROLE_DELETE', 'Delete role', 'Roles and Permissions', 'Allows deleting a role', GETDATE()),
('ROLE_ASSIGN_PERMISSION', 'Assign permissions to role', 'Roles and Permissions', 'Allows assigning permissions to a role', GETDATE()),
('PERMISSION_MANAGE', 'Manage permissions', 'Roles and Permissions', 'Allows managing all system permissions', GETDATE());

-- ============================================
-- CATEGORY 4: REAL ESTATE MANAGEMENT
-- ============================================
INSERT INTO Permissions (Code, Name, Category, Description, CreatedAt) 
VALUES 
('PROPERTY_VIEW', 'View property', 'Property Management', 'Allows viewing the list of property properties', GETDATE()),
('PROPERTY_CREATE', 'Create property', 'Property Management', 'Allows adding a new property property', GETDATE()),
('PROPERTY_EDIT', 'Edit property', 'Property Management', 'Allows modifying property property information', GETDATE()),
('PROPERTY_DELETE', 'Delete property', 'Property Management', 'Allows deleting a property property', GETDATE()),
('PROPERTY_EXPORT', 'Export property', 'Property Management', 'Allows exporting the list of property properties', GETDATE()),
('PROPERTY_DETAIL_VIEW', 'View property details', 'Property Management', 'Allows viewing complete details of a property', GETDATE());

-- ============================================
-- CATEGORY 5: LANDLORD MANAGEMENT
-- ============================================
INSERT INTO Permissions (Code, Name, Category, Description, CreatedAt) 
VALUES 
('OWNER_VIEW', 'View owners', 'Owner Management', 'Allows viewing the list of owners', GETDATE()),
('OWNER_CREATE', 'Create owner', 'Owner Management', 'Allows adding a new owner', GETDATE()),
('OWNER_EDIT', 'Edit owner', 'Owner Management', 'Allows modifying owner information', GETDATE()),
('OWNER_DELETE', 'Delete owner', 'Owner Management', 'Allows deleting a owner', GETDATE()),
('OWNER_PROPERTIES_VIEW', 'View owner properties', 'Owner Management', 'Allows viewing properties associated with a owner', GETDATE());

-- ============================================
-- CATEGORY 6: TENANT MANAGEMENT
-- ============================================
INSERT INTO Permissions (Code, Name, Category, Description, CreatedAt) 
VALUES 
('TENANT_VIEW', 'View tenants', 'Tenant Management', 'Allows viewing the list of tenants', GETDATE()),
('TENANT_CREATE', 'Create tenant', 'Tenant Management', 'Allows adding a new tenant', GETDATE()),
('TENANT_EDIT', 'Edit tenant', 'Tenant Management', 'Allows modifying tenant information', GETDATE()),
('TENANT_DELETE', 'Delete tenant', 'Tenant Management', 'Allows deleting a tenant', GETDATE()),
('TENANT_CONTRACT_VIEW', 'View rental contracts', 'Tenant Management', 'Allows viewing rental contracts', GETDATE()),
('TENANT_CONTRACT_CREATE', 'Create rental contract', 'Tenant Management', 'Allows creating a new rental contract', GETDATE()),
('TENANT_CONTRACT_EDIT', 'Edit rental contract', 'Tenant Management', 'Allows modifying an existing rental contract', GETDATE()),
('TENANT_CONTRACT_DELETE', 'Delete rental contract', 'Tenant Management', 'Allows deleting a rental contract', GETDATE());

-- ============================================
-- CATEGORY 7: SUBDEED MANAGEMENT
-- ============================================
INSERT INTO Permissions (Code, Name, Category, Description, CreatedAt) 
VALUES 
('SUBDEED_VIEW', 'View subdeeds', 'Subdeed Management', 'Allows viewing property subdeeds', GETDATE()),
('SUBDEED_CREATE', 'Create subdeed', 'Subdeed Management', 'Allows creating a new property subdeed', GETDATE()),
('SUBDEED_EDIT', 'Edit subdeed', 'Subdeed Management', 'Allows modifying a property subdeed', GETDATE()),
('SUBDEED_DELETE', 'Delete subdeed', 'Subdeed Management', 'Allows deleting a property subdeed', GETDATE());

-- ============================================
-- CATEGORY 8: LEASE MANAGEMENT
-- ============================================
INSERT INTO Permissions (Code, Name, Category, Description, CreatedAt) 
VALUES 
('LEASE_VIEW', 'View leases', 'Lease Management', 'Allows viewing the list of leases', GETDATE()),
('LEASE_CREATE', 'Create lease', 'Lease Management', 'Allows creating a new lease', GETDATE()),
('LEASE_EDIT', 'Edit lease', 'Lease Management', 'Allows modifying an existing lease', GETDATE()),
('LEASE_DELETE', 'Delete lease', 'Lease Management', 'Allows deleting a lease', GETDATE()),
('LEASE_RENEW', 'Renew lease', 'Lease Management', 'Allows renewing an expiring lease', GETDATE()),
('LEASE_TERMINATE', 'Terminate lease', 'Lease Management', 'Allows terminating a lease before its term', GETDATE()),
('LEASE_PAYMENTS_VIEW', 'View lease payments', 'Lease Management', 'Allows viewing payment history of a lease', GETDATE());

-- ============================================
-- CATEGORY 9: INCIDENT MANAGEMENT
-- ============================================
INSERT INTO Permissions (Code, Name, Category, Description, CreatedAt) 
VALUES 
('INCIDENT_VIEW', 'View incidents', 'Incident Management', 'Allows viewing the list of incidents', GETDATE()),
('INCIDENT_CREATE', 'Create incident', 'Incident Management', 'Allows reporting a new incident', GETDATE()),
('INCIDENT_EDIT', 'Edit incident', 'Incident Management', 'Allows modifying incident information', GETDATE()),
('INCIDENT_DELETE', 'Delete incident', 'Incident Management', 'Allows deleting an incident', GETDATE()),
('INCIDENT_ASSIGN', 'Assign incident', 'Incident Management', 'Allows assigning an incident to a responsible person', GETDATE()),
('INCIDENT_RESOLVE', 'Resolve incident', 'Incident Management', 'Allows marking an incident as resolved', GETDATE()),
('INCIDENT_COMMENT', 'Comment on incident', 'Incident Management', 'Allows adding comments to an incident', GETDATE()),
('INCIDENT_PRIORITY_CHANGE', 'Change priority', 'Incident Management', 'Allows modifying incident priority', GETDATE());

-- ============================================
-- CATEGORY 10: REPORT MANAGEMENT
-- ============================================
INSERT INTO Permissions (Code, Name, Category, Description, CreatedAt) 
VALUES 
('REPORT_VIEW', 'View reports', 'Report Management', 'Allows viewing reports', GETDATE()),
('REPORT_CREATE', 'Create report', 'Report Management', 'Allows creating a new report', GETDATE()),
('REPORT_EDIT', 'Edit report', 'Report Management', 'Allows modifying an existing report', GETDATE()),
('REPORT_DELETE', 'Delete report', 'Report Management', 'Allows deleting a report', GETDATE()),
('REPORT_EXPORT', 'Export reports', 'Report Management', 'Allows exporting reports in different formats', GETDATE()),
('REPORT_GENERATE', 'Generate automatic reports', 'Report Management', 'Allows generating automatic reports', GETDATE()),
('REPORT_ANALYTICS_VIEW', 'View analytics', 'Report Management', 'Allows accessing analytics and statistics', GETDATE());

-- ============================================
-- CATEGORY 11: REPORTER MANAGEMENT
-- ============================================
INSERT INTO Permissions (Code, Name, Category, Description, CreatedAt) 
VALUES 
('REPORTER_VIEW', 'View reporters', 'Reporter Management', 'Allows viewing the list of reporters', GETDATE()),
('REPORTER_CREATE', 'Create reporter', 'Reporter Management', 'Allows adding a new reporter', GETDATE()),
('REPORTER_EDIT', 'Edit reporter', 'Reporter Management', 'Allows modifying reporter information', GETDATE()),
('REPORTER_DELETE', 'Delete reporter', 'Reporter Management', 'Allows deleting a reporter', GETDATE());

-- ============================================
-- CATEGORY 12: AGENDA MANAGEMENT
-- ============================================
INSERT INTO Permissions (Code, Name, Category, Description, CreatedAt) 
VALUES 
('AGENDA_VIEW', 'View agendas', 'Agenda Management', 'Allows viewing agendas', GETDATE()),
('AGENDA_CREATE', 'Create agenda', 'Agenda Management', 'Allows creating a new agenda', GETDATE()),
('AGENDA_EDIT', 'Edit agenda', 'Agenda Management', 'Allows modifying an existing agenda', GETDATE()),
('AGENDA_DELETE', 'Delete agenda', 'Agenda Management', 'Allows deleting an agenda', GETDATE()),
('AGENDA_PUBLISH', 'Publish agenda', 'Agenda Management', 'Allows publishing an agenda', GETDATE()),
('AGENDA_ITEM_ADD', 'Add agenda item', 'Agenda Management', 'Allows adding an item to the agenda', GETDATE());

-- ============================================
-- CATEGORY 13: MEETING MANAGEMENT
-- ============================================
INSERT INTO Permissions (Code, Name, Category, Description, CreatedAt) 
VALUES 
('MEETING_VIEW', 'View meetings', 'Meeting Management', 'Allows viewing the list of meetings', GETDATE()),
('MEETING_CREATE', 'Create meeting', 'Meeting Management', 'Allows creating a new meeting', GETDATE()),
('MEETING_EDIT', 'Edit meeting', 'Meeting Management', 'Allows modifying an existing meeting', GETDATE()),
('MEETING_DELETE', 'Delete meeting', 'Meeting Management', 'Allows deleting a meeting', GETDATE()),
('MEETING_MINUTES_CREATE', 'Create meeting minutes', 'Meeting Management', 'Allows creating meeting minutes', GETDATE()),
('MEETING_MINUTES_EDIT', 'Edit meeting minutes', 'Meeting Management', 'Allows modifying meeting minutes', GETDATE()),
('MEETING_INVITE', 'Invite participants', 'Meeting Management', 'Allows inviting participants to a meeting', GETDATE()),
('MEETING_ATTENDANCE_MANAGE', 'Manage attendance', 'Meeting Management', 'Allows managing attendance list', GETDATE());

-- ============================================
-- CATEGORY 14: DECISION MANAGEMENT
-- ============================================
INSERT INTO Permissions (Code, Name, Category, Description, CreatedAt) 
VALUES 
('DECISION_VIEW', 'View decisions', 'Decision Management', 'Allows viewing decisions made', GETDATE()),
('DECISION_CREATE', 'Create decision', 'Decision Management', 'Allows recording a new decision', GETDATE()),
('DECISION_EDIT', 'Edit decision', 'Decision Management', 'Allows modifying an existing decision', GETDATE()),
('DECISION_APPROVE', 'Approve decision', 'Decision Management', 'Allows approving a decision', GETDATE()),
('DECISION_VOTE', 'Vote on decision', 'Decision Management', 'Allows voting on a decision', GETDATE()),
('DECISION_IMPLEMENT_TRACK', 'Track implementation', 'Decision Management', 'Allows tracking decision implementation', GETDATE());

-- ============================================
-- CATEGORY 15: SYSTEM ADMINISTRATION
-- ============================================
INSERT INTO Permissions (Code, Name, Category, Description, CreatedAt) 
VALUES 
('SYSTEM_SETTINGS_VIEW', 'View system settings', 'System Administration', 'Allows viewing system settings', GETDATE()),
('SYSTEM_SETTINGS_EDIT', 'Edit system settings', 'System Administration', 'Allows modifying system settings', GETDATE()),
('AUDIT_LOG_VIEW', 'View audit logs', 'System Administration', 'Allows viewing system audit logs', GETDATE()),
('BACKUP_MANAGE', 'Manage backups', 'System Administration', 'Allows managing database backups', GETDATE()),
('SYSTEM_HEALTH_VIEW', 'View system health', 'System Administration', 'Allows viewing system health status', GETDATE());

-- ============================================
-- CATEGORY 16: NOTIFICATIONS
-- ============================================
INSERT INTO Permissions (Code, Name, Category, Description, CreatedAt) 
VALUES 
('NOTIFICATION_SEND', 'Send notifications', 'Notifications', 'Allows sending notifications to users', GETDATE()),
('NOTIFICATION_MANAGE', 'Manage notifications', 'Notifications', 'Allows managing notification templates', GETDATE()),
('EMAIL_TEMPLATE_MANAGE', 'Manage email templates', 'Notifications', 'Allows managing email templates', GETDATE()),
('NOTIFICATION_CONFIG_EDIT', 'Configure notifications', 'Notifications', 'Allows configuring notification settings', GETDATE());

-- ============================================
-- CATEGORY 17: PAYMENT MANAGEMENT
-- ============================================
INSERT INTO Permissions (Code, Name, Category, Description, CreatedAt) 
VALUES 
('PAYMENT_VIEW', 'View payments', 'Payment Management', 'Allows viewing payments', GETDATE()),
('PAYMENT_CREATE', 'Create payment', 'Payment Management', 'Allows recording a new payment', GETDATE()),
('PAYMENT_EDIT', 'Edit payment', 'Payment Management', 'Allows modifying an existing payment', GETDATE()),
('PAYMENT_DELETE', 'Delete payment', 'Payment Management', 'Allows deleting a payment', GETDATE()),
('PAYMENT_VALIDATE', 'Validate payment', 'Payment Management', 'Allows validating a payment', GETDATE()),
('PAYMENT_EXPORT', 'Export payments', 'Payment Management', 'Allows exporting the payment list', GETDATE());

-- ============================================
-- CATEGORY 18: DOCUMENT MANAGEMENT
-- ============================================
INSERT INTO Permissions (Code, Name, Category, Description, CreatedAt) 
VALUES 
('DOCUMENT_VIEW', 'View documents', 'Document Management', 'Allows viewing documents', GETDATE()),
('DOCUMENT_UPLOAD', 'Upload document', 'Document Management', 'Allows uploading a new document', GETDATE()),
('DOCUMENT_EDIT', 'Edit document', 'Document Management', 'Allows modifying document metadata', GETDATE()),
('DOCUMENT_DELETE', 'Delete document', 'Document Management', 'Allows deleting a document', GETDATE()),
('DOCUMENT_DOWNLOAD', 'Download document', 'Document Management', 'Allows downloading a document', GETDATE()),
('DOCUMENT_SHARE', 'Share document', 'Document Management', 'Allows sharing a document with other users', GETDATE());