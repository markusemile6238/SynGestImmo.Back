CREATE INDEX IX_Users_Email ON Users(Email);
GO
CREATE INDEX IX_Users_UserRef ON Users(UserRef);
GO
CREATE INDEX IX_Users_EntityId ON Users(EntityId);
GO
CREATE INDEX IX_PasswordResets_Email ON PasswordResets(Email);
GO
CREATE INDEX IX_PasswordResets_Code ON PasswordResets(Code);
GO
CREATE INDEX IX_AuditLogs_UserId ON AuditLogs(UserId);
GO
CREATE INDEX IX_AuditLogs_CreatedAt ON AuditLogs(CreatedAt);
GO


