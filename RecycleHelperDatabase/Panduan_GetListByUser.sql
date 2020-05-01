/**
 *	Created by: Jonathan Ibrahim
 *	Date: 28 April 2020
 *	Purpose: Get List Panduan by User Id
 */

CREATE PROCEDURE [dbo].[Panduan_GetListByUser]
	@IdUser INT
AS
BEGIN
	SELECT IdPanduan,NamaPanduan,DeskripsiPanduan,IdUser
	FROM trPanduan
	WHERE IdUser = @IdUser
END
