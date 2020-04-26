CREATE TABLE trPanduan(
	IdPanduan int PRIMARY KEY identity(1,1),
	NamaPanduan varchar(50) not null,
	DeskripsiPanduan varchar(MAX) not null,
	ListIdBahan varchar(MAX) not null
)