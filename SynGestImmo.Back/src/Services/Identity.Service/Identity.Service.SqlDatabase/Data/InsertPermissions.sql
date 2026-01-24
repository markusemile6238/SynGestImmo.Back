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


