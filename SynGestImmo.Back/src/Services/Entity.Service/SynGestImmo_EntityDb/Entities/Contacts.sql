CREATE TABLE [entity].[Contacts]
(
	[Id] INT NOT NULL IDENTITY PRIMARY KEY,
	[EntityId] INT NOT NULL ,
	[ContactType]  VARCHAR(30) NOT NULL,
	[Value] VARCHAR (150) NOT NULL,

	--CONSTRAINTS
	CONSTRAINT [FK_Contacts_EntityId] FOREIGN KEY (EntityId) REFERENCES [entity].[Entities] (Id),
	CONSTRAINT [CK_Contacts_Value] CHECK(Value IN ('email','mobile','phone','office','office residence','fax'))

)
