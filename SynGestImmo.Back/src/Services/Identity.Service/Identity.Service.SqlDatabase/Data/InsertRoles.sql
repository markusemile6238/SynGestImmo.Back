-- ============================================
-- CREATION OF DEFAULT ROLES
-- ============================================

-- Insert roles
INSERT INTO Roles (Name, Description,IsSystemRole, IsActive, CreatedAt,Prefixe) 
VALUES 
('System Admin', 'System administrator with all rights', 1, 1, GETDATE(),'SA'),
('Property Administrtor', 'Real estate and rental properties administrator', 1, 1, GETDATE(),'PA'),
('Property Manager', 'Daily property, owner and tenant manager', 1, 1, GETDATE(),'PM'),
('Owner', 'Real estate property owner', 1, 1, GETDATE(),'OWN'),
('Tenant', 'Real estate tenant', 1, 1, GETDATE(),'TEN'),
('Reporter', 'Support employee for incidents and reports', 1, 1, GETDATE(),'RPT'),
('Secretary', 'Secretary for administrative management', 0,  1, GETDATE(),'SCT'),
('Financial manager', 'Financial and payment manager', 0, 1, GETDATE(),'FM'),
('Maintenance Manager', 'Property maintenance and incident manager', 0, 1, GETDATE(),'MM');

