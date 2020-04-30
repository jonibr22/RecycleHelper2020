/**
 *	Created by: Jonathan Ibrahim
 *	Date: 29 April 2020
 *	Purpose: Delete Multiple Kategori Bahan by List Id Kategori Bahan
 */

CREATE PROCEDURE [dbo].[KategoriBahan_DeleteMultiple]
	@ListIdKategoriBahan VARCHAR(MAX)
AS
BEGIN
	DELETE msKategoriBahan
	WHERE IdKategoriBahan IN (SELECT value FROM dbo.fn_General_Split(@ListIdKategoriBahan,','))
	SELECT 1
END
