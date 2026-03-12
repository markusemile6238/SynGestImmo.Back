CREATE TABLE [entity].[EntityAddresses]
(
	[EntityId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[AddressId] INT NOT NULL,
	[AddressType] VARCHAR(20) NOT NULL,

	--CONSTRAINTS
	CONSTRAINT [FK_EntityAddesses_EntityId] FOREIGN KEY (EntityId) REFERENCES [entity].[Entities] (Id),
	CONSTRAINT [FK_EntityAddesses_AddressId] FOREIGN KEY (AddressId) REFERENCES [entity].[Addresses] (Id),
	CONSTRAINT [CK_AddressType] CHECK(AddressType IN ('Billing','Residence','Office'))

)
