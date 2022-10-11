CREATE TABLE [dbo].[Todo]
(
	[TodoID] INT NOT NULL PRIMARY KEY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(250) NULL, 
    [TodosGroupID] INT NOT NULL,
    CONSTRAINT FK_Todo_TodosGroup FOREIGN KEY ([TodosGroupID]) REFERENCES TodosGroup([TodosGroupID])
)
