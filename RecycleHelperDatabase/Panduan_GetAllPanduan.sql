/**
 *	Created by: Christian Soetanto
 *	Date: 26 April 2020
 *	Purpose: Get All Panduan
 *
 *	Altered by: Jonathan Ibrahim
 *	Date: 28 April 2020
 *	Purpose: Tambah field IdUser dan menghilangkan ListIdBahan
 */

CREATE PROCEDURE [dbo].[Panduan_GetAllPanduan]
AS
BEGIN
	SELECT [IdPanduan],[NamaPanduan],[DeskripsiPanduan],[IdUser]
	FROM trPanduan
END
