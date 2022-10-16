CREATE TABLE [dbo].[Group]
(
	[GroupId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(20) NOT NULL, 
    [OwnerId] INT NOT NULL,
    CONSTRAINT FK_Group_User FOREIGN KEY ([OwnerId]) REFERENCES [User]([UserId])
)
