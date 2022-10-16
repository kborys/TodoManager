CREATE TABLE [dbo].[UserGroupRelation]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [GroupId] INT NOT NULL
    CONSTRAINT FK_UserGroupRelation_User FOREIGN KEY ([UserId]) REFERENCES [User]([UserId]),
    CONSTRAINT FK_UserGroupRelation_Group FOREIGN KEY ([GroupId]) REFERENCES [Group]([GroupId]),
)