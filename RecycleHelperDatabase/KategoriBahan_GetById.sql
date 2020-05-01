/**
 *	Created by: Jonathan Ibrahim
 *	Date: 28 April 2020
 *	Purpose: Get Kategori Bahan by Id
 */

CREATE PROCEDURE [dbo].[KategoriBahan_GetById]
	@IdKategoriBahan INT
AS
BEGIN
	SELECT IdKategoriBahan, NamaKategoriBahan
	FROM msKategoriBahan
	WHERE IdKategoriBahan = @IdKategoriBahan
END
