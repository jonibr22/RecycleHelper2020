/**
 *	Created by: Jonathan Ibrahim
 *	Date: 28 April 2020
 *	Purpose: Insert Update Kategori Bahan
 */

CREATE PROCEDURE [dbo].[KategoriBahan_InsertUpdate]
	@IdKategoriBahan INT,
	@NamaKategoriBahan VARCHAR(MAX)
AS
BEGIN
	DECLARE @RetVal INT
	IF(EXISTS(SELECT 1 FROM msKategoriBahan WHERE IdKategoriBahan = @IdKategoriBahan))
	BEGIN
		UPDATE msKategoriBahan
		SET [NamaKategoriBahan] = @NamaKategoriBahan
		WHERE IdKategoriBahan = @IdKategoriBahan

		SET @RetVal = @IdKategoriBahan
	END
	ELSE
	BEGIN
		INSERT INTO msKategoriBahan([NamaKategoriBahan])
		VALUES (@NamaKategoriBahan)

		SET @RetVal = SCOPE_IDENTITY()
	END
	SELECT @RetVal
END
