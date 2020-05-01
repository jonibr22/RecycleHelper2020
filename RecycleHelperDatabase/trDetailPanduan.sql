CREATE TABLE [dbo].[trDetailPanduan]
(
	[IdBahan] INT NOT NULL, 
    [IdPanduan] INT NOT NULL
	PRIMARY KEY([IdBahan],[IdPanduan]),
	FOREIGN KEY([IdBahan]) REFERENCES msBahan([IdBahan]),
	FOREIGN KEY([IdPanduan]) REFERENCES trPanduan([IdPanduan])
)
