CREATE TABLE [entity].[Persons]
(
	[EntityId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[FirstName] VARCHAR(80) NOT NULL,  
	[LastName] VARCHAR(100) NOT NULL,
	[BirthDate] DATETIME NOT NULL,
	[NationalId] VARCHAR(30) NOT NULL,

	--CONSTRAINTS
	CONSTRAINT [FK_entity_EntityId] FOREIGN KEY (EntityId) REFERENCES [entity].[Entities] (Id)
	

)
