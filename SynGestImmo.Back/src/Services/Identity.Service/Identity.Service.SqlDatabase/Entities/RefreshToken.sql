ALTER TABLE [dbo].[SomeTableOrView]
	ADD CONSTRAINT [RefreshToken]
	FOREIGN KEY (SomeColumn)
	REFERENCES [SomeTable] (SomeColumn)
