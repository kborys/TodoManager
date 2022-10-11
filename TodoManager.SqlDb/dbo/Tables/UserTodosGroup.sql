CREATE TABLE [dbo].[UserTodosGroup]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UserID] INT NOT NULL, 
    [TodosGroupID] INT NOT NULL
    CONSTRAINT FK_UserTodosGroup_User FOREIGN KEY ([UserID]) REFERENCES [User]([UserID])
    CONSTRAINT FK_UserTodosGroup_TodosGroup FOREIGN KEY ([TodosGroupID]) REFERENCES [TodosGroup]([TodosGroupID])
    UNIQUE ([UserID], [TodosGroupID])
)
