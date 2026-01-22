CREATE PROCEDURE [dbo].[CreateUser]
	 @Email NVARCHAR(150),
     @PasswordHash NVARCHAR(255),
     @UserRef NVARCHAR(50),
     @MainRoleId INT,
     @EntityId UNIQUEIDENTIFIER = NULL
AS
	BEGIN
        INSERT INTO Users (Email, PasswordHash, UserRef, MainRoleId, EntityId)
        OUTPUT Inserted.Id
        VALUES (@Email, @PasswordHash, @UserRef, @MainRoleId, @EntityId);
    END
RETURN 0
