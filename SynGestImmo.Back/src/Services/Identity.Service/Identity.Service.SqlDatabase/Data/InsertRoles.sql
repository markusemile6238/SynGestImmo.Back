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
