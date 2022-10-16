CREATE TABLE [dbo].[Todo]
(
	[TodoId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(250) NULL, 
    [GroupId] INT NOT NULL,
    [OwnerId] INT NOT NULL, 
    [StatusId] INT NOT NULL DEFAULT 1, 
    CONSTRAINT FK_Todo_Group FOREIGN KEY ([GroupId]) REFERENCES [Group]([GroupId]),
    CONSTRAINT FK_Todo_User FOREIGN KEY ([OwnerId]) REFERENCES [User]([UserId]),
    CONSTRAINT FK_Todo_Status FOREIGN KEY ([StatusId]) REFERENCES [Status]([StatusId])
)
