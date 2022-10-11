CREATE TABLE [dbo].[User] (
    [UserID]    INT           IDENTITY (1, 1) NOT NULL,
    [UserName]  NVARCHAR (50) NOT NULL,
    [FirstName] NVARCHAR (50) NOT NULL,
    [LastName]  NVARCHAR (50) NOT NULL,
    [Password]  CHAR (90)     NOT NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC),
    UNIQUE NONCLUSTERED ([UserName] ASC)
);