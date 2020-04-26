/**
 *	Created by: Christian Soetanto
 *	Date: 26 April 2020
 *	Purpose: Get All Panduan
 */

CREATE PROCEDURE [dbo].[Panduan_GetAllPanduan]
AS
BEGIN
	SELECT [IdPanduan],[NamaPanduan],[DeskripsiPanduan],[ListIdBahan]
	FROM trPanduan
END
