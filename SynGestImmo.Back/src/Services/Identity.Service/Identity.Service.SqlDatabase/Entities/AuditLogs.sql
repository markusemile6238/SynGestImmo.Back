CREATE TABLE AuditLogs (
        [Id] UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
        [UserId] UNIQUEIDENTIFIER NULL,
        [Action] NVARCHAR(100) NOT NULL,
        [EntityType] NVARCHAR(100) NOT NULL,
        [EntityId] UNIQUEIDENTIFIER NULL,
        [OldValues] NVARCHAR(MAX) NULL,
        [NewValues] NVARCHAR(MAX) NULL,
        [CreatedAt] DATETIME2 DEFAULT GETDATE(),
        [IPAddress] NVARCHAR(45) NULL,
        [UserAgent] NVARCHAR(500) NULL
    );