/**
 *	Created by: Jonathan Ibrahim
 *	Date: 30 April 2020
 *	Purpose: Delete Detail Panduan by Multiple Panduan Id
 */

CREATE PROCEDURE [dbo].[DetailPanduan_DeleteByMultiplePanduan]
	@ListIdPanduan VARCHAR(MAX)
AS
BEGIN
	DELETE trDetailPanduan
	WHERE IdPanduan IN (SELECT value FROM dbo.fn_General_Split(@ListIdPanduan,','))
	SELECT 1
END

