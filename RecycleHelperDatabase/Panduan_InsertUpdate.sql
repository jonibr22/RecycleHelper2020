/**
 *	Created by: Christian Soetanto
 *	Date: 26 April 2020
 *	Purpose: Insert atau Update Panduan
 */
CREATE PROCEDURE [dbo].[Panduan_InsertUpdate]
	@IdPanduan INT,
	@NamaPanduan VARCHAR(MAX),
	@DeskripsiPanduan VARCHAR(MAX),
	@ListIdBahan VARCHAR(MAX)
AS
BEGIN
	DECLARE @RetVal INT
	IF(EXISTS(SELECT 1 FROM trPanduan WHERE IdPanduan = @IdPanduan))
	BEGIN
		UPDATE trPanduan
		SET 
		[NamaPanduan] = @NamaPanduan,
		[DeskripsiPanduan] = @DeskripsiPanduan,
		[ListIdBahan] = @ListIdBahan
 		WHERE IdPanduan = @IdPanduan

		SET @RetVal = @IdPanduan
	END
	ELSE
	BEGIN
		INSERT INTO trPanduan([NamaPanduan],[DeskripsiPanduan],[ListIdBahan])
		VALUES (@NamaPanduan,@DeskripsiPanduan,@ListIdBahan)

		SET @RetVal = SCOPE_IDENTITY()
	END
	SELECT @RetVal
END