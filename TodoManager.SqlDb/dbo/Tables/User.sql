CREATE TABLE [dbo].[User] (
    [UserID]    INT           IDENTITY (1, 1) NOT NULL PRIMARY KEY,
    [UserName]  NVARCHAR (50) NOT NULL UNIQUE,
    [FirstName] NVARCHAR (50) NOT NULL,
    [LastName]  NVARCHAR (50) NOT NULL,
    [Password]  CHAR (90)     NOT NULL,
    [EmailAddress] NVARCHAR(319) NULL UNIQUE
);