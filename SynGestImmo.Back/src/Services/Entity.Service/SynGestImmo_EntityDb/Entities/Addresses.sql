CREATE TABLE [entity].[Addresses]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Street] VARCHAR(120) NOT NULL,
	[City] VARCHAR(80) NOT NULL,
	[PostalCode] VARCHAR(10) NOT NULL,
	[Country] VARCHAR (50)
)
