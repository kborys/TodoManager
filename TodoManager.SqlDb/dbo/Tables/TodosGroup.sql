CREATE TABLE [dbo].[TodosGroup]
(
	[TodosGroupID] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(20) NOT NULL, 
    [OwnerID] INT NOT NULL,
    CONSTRAINT FK_TodosGroup_User FOREIGN KEY ([OwnerID]) REFERENCES [User]([UserID])
)
