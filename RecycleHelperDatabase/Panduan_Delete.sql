/**
 *	Created by: Jonathan Ibrahim
 *	Date: 29 April 2020
 *	Purpose: Delete Panduan by Panduan Id
 */
CREATE PROCEDURE [dbo].[Panduan_Delete]
	@IdPanduan INT
AS
BEGIN
	DELETE trPanduan
	WHERE IdPanduan = @IdPanduan
	SELECT 1
END
