/**
 *	Created by: Jonathan Ibrahim
 *	Date: 30 April 2020
 *	Purpose: Get List Bahan by Kategori Id
 */

CREATE PROCEDURE [dbo].[Bahan_GetListByKategori]
	@IdKategori INT
AS
BEGIN
	SELECT IdBahan, NamaBahan, IdKategoriBahan
	FROM msBahan
	WHERE IdKategoriBahan = @IdKategori
END
