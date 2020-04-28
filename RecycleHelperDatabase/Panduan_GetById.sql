/**
 *	Created by: Jonathan Ibrahim
 *	Date: 28 April 2020
 *	Purpose: Get Panduan by Id
 */

CREATE PROCEDURE [dbo].[Panduan_GetById]
	@IdPanduan INT
AS
BEGIN
	SELECT IdPanduan,NamaPanduan,DeskripsiPanduan,IdUser
	FROM trPanduan
	WHERE IdPanduan = @IdPanduan
END
