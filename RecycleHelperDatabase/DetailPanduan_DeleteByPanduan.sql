/**
 *	Created by: Jonathan Ibrahim
 *	Date: 28 April 2020
 *	Purpose: Delete Detail Panduan by Panduan Id
 */

CREATE PROCEDURE [dbo].[DetailPanduan_DeleteByPanduan]
	@IdPanduan INT
AS
BEGIN
	DELETE trDetailPanduan
	WHERE IdPanduan = @IdPanduan
	SELECT 1
END
