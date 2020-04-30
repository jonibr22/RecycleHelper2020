/**
 *	Created by: Jonathan Ibrahim
 *	Date: 29 April 2020
 *	Purpose: Delete Multiple Bahan by List Bahan Id
 */

CREATE PROCEDURE [dbo].[Bahan_DeleteMultiple]
	@ListIdBahan VARCHAR(MAX)
AS
BEGIN
	DELETE msBahan
	WHERE IdBahan IN (SELECT value FROM dbo.fn_General_Split(@ListIdBahan,','))
	SELECT 1
END
