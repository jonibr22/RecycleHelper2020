/**
 *	Created by: Christian Soetanto
 *	Date: 26 April 2020
 *	Purpose: Insert atau Update Bahan
 */
CREATE PROCEDURE [dbo].[Bahan_InsertUpdate]
	@IdBahan INT,
	@NamaBahan VARCHAR(MAX)
AS
BEGIN
	DECLARE @RetVal INT
	IF(EXISTS(SELECT 1 FROM msBahan WHERE IdBahan = @IdBahan))
	BEGIN
		UPDATE msBahan
		SET [NamaBahan] = @NamaBahan
		WHERE IdBahan = @IdBahan

		SET @RetVal = @IdBahan
	END
	ELSE
	BEGIN
		INSERT INTO msBahan([NamaBahan])
		VALUES (@NamaBahan)

		SET @RetVal = SCOPE_IDENTITY()
	END
	SELECT @RetVal
END