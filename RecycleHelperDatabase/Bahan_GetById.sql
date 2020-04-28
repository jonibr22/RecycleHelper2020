/**
 *	Created by: Jonathan Ibrahim
 *	Date: 26 April 2020
 *  Purpose: Get Bahan by Id
 */

CREATE PROCEDURE [dbo].[Bahan_GetById]
	@IdBahan INT
AS
BEGIN
	SELECT IdBahan, NamaBahan, IdKategoriBahan
	FROM msBahan
	WHERE IdBahan = @IdBahan
END
