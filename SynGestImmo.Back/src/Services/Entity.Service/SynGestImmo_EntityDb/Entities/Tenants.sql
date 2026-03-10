CREATE TABLE [entity].[Tenants]
(
	[EntityId] INT NOT NULL PRIMARY KEY,
	[MoveInDate] DATETIME NOT NULL,
	[MoveOutDate] DATETIME NULL,

	--CONSTRAINTS	
	CONSTRAINT [FK_Tenants_EntityID] FOREIGN KEY (EntityID) REFERENCES [entity].[Entities]
)
