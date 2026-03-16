CREATE TABLE [entity].[Companies]
(
	[EntityId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[CompanyName] VARCHAR(150) NOT NULL,
	[VatNumber] FLOAT NOT NULL,
	[RegistrationNumber] VARCHAR(20),

	--CONSTRAINTS
	CONSTRAINT [FK_Companies_EntityId] FOREIGN KEY (EntityId) REFERENCES [entity].[Entities] (Id)
)
