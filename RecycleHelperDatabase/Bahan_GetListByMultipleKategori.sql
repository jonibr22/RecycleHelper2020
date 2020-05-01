/**
 *	Created by: Jonathan Ibrahim
 *	Date: 30 April 2020
 *	Purpose: Get List Bahan by Kategori Id
 */

CREATE PROCEDURE [dbo].[Bahan_GetListByMultipleKategori]
	@ListIdKategori VARCHAR(MAX)
AS
BEGIN
	SELECT IdBahan, NamaBahan, IdKategoriBahan
	FROM msBahan
	WHERE IdKategoriBahan IN (SELECT value FROM dbo.fn_General_Split(@ListIdKategori,','))
END
