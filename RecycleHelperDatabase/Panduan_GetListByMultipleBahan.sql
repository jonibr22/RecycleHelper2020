/**
 *	Created by: Jonathan Ibrahim
 *	Date: 28 April 2020
 *	Purpose: Get List Panduan by Multiple Bahan
 */

CREATE PROCEDURE [dbo].[Panduan_GetListByMultipleBahan]
	@ListIdBahan VARCHAR(MAX)
AS
BEGIN
	SELECT DISTINCT A.IdPanduan,A.NamaPanduan,A.DeskripsiPanduan,A.IdUser
	FROM trPanduan A JOIN trDetailPanduan B ON A.IdPanduan = B.IdPanduan
	JOIN msBahan C ON B.IdBahan = C.IdBahan
	WHERE C.IdBahan IN (SELECT value FROM dbo.fn_General_Split(@ListIdBahan,','))
END
