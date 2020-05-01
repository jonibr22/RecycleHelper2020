/**
 *	Created by: Christian Soetanto
 *	Date: 26 April 2020
 *	Purpose: Insert atau Update Panduan
 *
 *	Altered by: Jonathan Ibrahim
 *	Date: 28 April 2020
 *	Purpose: Menambah IdUser dan menghapus ListIdBahan
 */
CREATE PROCEDURE [dbo].[Panduan_InsertUpdate]
	@IdPanduan INT,
	@NamaPanduan VARCHAR(MAX),
	@DeskripsiPanduan VARCHAR(MAX),
	@IdUser INT
AS
BEGIN
	DECLARE @RetVal INT
	IF(EXISTS(SELECT 1 FROM trPanduan WHERE IdPanduan = @IdPanduan))
	BEGIN
		UPDATE trPanduan
		SET 
		[NamaPanduan] = @NamaPanduan,
		[DeskripsiPanduan] = @DeskripsiPanduan,
		[IdUser] = @IdUser
 		WHERE IdPanduan = @IdPanduan

		SET @RetVal = @IdPanduan
	END
	ELSE
	BEGIN
		INSERT INTO trPanduan([NamaPanduan],[DeskripsiPanduan],[IdUser])
		VALUES (@NamaPanduan,@DeskripsiPanduan,@IdUser)

		SET @RetVal = SCOPE_IDENTITY()
	END
	SELECT @RetVal
END