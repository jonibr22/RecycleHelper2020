/**
 *	Created by: Jonathan Ibrahim
 *	Date: 30 April 2020
 *	Purpose: Delete Panduan by Multiple Panduan Id
 */
CREATE PROCEDURE [dbo].[Panduan_DeleteMultiple]
	@ListIdPanduan VARCHAR(MAX)
AS
BEGIN
	DELETE trPanduan
	WHERE IdPanduan IN (SELECT value FROM dbo.fn_General_Split(@ListIdPanduan,','))
	SELECT 1
END
