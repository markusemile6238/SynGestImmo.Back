CREATE TABLE [entity].[Companies]
(
	[EntityId] INT NOT NULL PRIMARY KEY,
	[CompanyName] VARCHAR(150) NOT NULL,
	[VatNumber] FLOAT NOT NULL,
	[RegistrationNumber] VARCHAR(20),

	--CONSTRAINTS
	CONSTRAINT [FK_Companies_EntityID] FOREIGN KEY (EntityID) REFERENCES [entity].[Entities] (Id)
)
