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

