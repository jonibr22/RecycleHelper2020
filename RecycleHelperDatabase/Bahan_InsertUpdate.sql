/**
 *	Created by: Christian Soetanto
 *	Date: 26 April 2020
 *	Purpose: Insert atau Update Bahan
 */
CREATE PROCEDURE [dbo].[Bahan_InsertUpdate]
	@IdBahan INT,
	@NamaBahan VARCHAR(MAX),
	@IdKategoriBahan INT
AS
BEGIN
	DECLARE @RetVal INT
	IF(EXISTS(SELECT 1 FROM msBahan WHERE IdBahan = @IdBahan))
	BEGIN
		UPDATE msBahan
		SET [NamaBahan] = @NamaBahan, [IdKategoriBahan] = @IdKategoriBahan
		WHERE IdBahan = @IdBahan

		SET @RetVal = @IdBahan
	END
	ELSE
	BEGIN
		INSERT INTO msBahan([NamaBahan],[IdKategoriBahan])
		VALUES (@NamaBahan,@IdKategoriBahan)

		SET @RetVal = SCOPE_IDENTITY()
	END
	SELECT @RetVal
END