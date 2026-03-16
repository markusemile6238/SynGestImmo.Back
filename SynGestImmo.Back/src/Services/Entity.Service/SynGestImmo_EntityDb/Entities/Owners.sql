CREATE TABLE [entity].[Owners]
(
	[EntityId] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[OwnerType] VARCHAR(25) NOT NULL,
	[Notes] TEXT NULL

	--CONSTRAINTS
	CONSTRAINT [FK_Owner_EntityID] FOREIGN KEY (EntityID) REFERENCES [entity].[Entities] (Id)
	
	/*	
	OWNERTYPE
	----------
	Individual
	Company
	JointOwnership
	Organization
	Investor
	*/

)
