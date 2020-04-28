CREATE TABLE [dbo].[msUser]
(
	[IdUser] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Username] VARCHAR(MAX) NOT NULL, 
    [Password] VARCHAR(MAX) NOT NULL, 
    [IdRole] INT NOT NULL, 
    [Name] VARCHAR(MAX) NOT NULL
	FOREIGN KEY (IdRole) REFERENCES msRole(IdRole)
)
