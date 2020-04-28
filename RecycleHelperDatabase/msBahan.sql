CREATE TABLE msBahan(
	IdBahan INT PRIMARY KEY IDENTITY(1,1),
	NamaBahan VARCHAR(MAX) NOT NULL,
	IdKategoriBahan INT NOT NULL,
	FOREIGN KEY (IdKategoriBahan) REFERENCES msKategoriBahan(IdKategoriBahan)
)