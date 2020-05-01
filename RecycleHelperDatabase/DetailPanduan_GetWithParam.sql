/**
 *	Created by: Jonathan Ibrahim
 *	Date: 28 April 2020
 *	Purpose: Get Detail Panduan by Panduan Id / Bahan Id / Both
 */

CREATE PROCEDURE [dbo].[DetailPanduan_GetWithParam]
	@IdPanduan INT = 0,
	@IdBahan INT = 0
AS
BEGIN
	SELECT IdPanduan,IdBahan
	FROM trDetailPanduan
	WHERE IdPanduan = CASE WHEN @IdPanduan = 0 THEN IdPanduan ELSE @IdPanduan END AND
	IdBahan = CASE WHEN @IdBahan = 0 THEN IdBahan ELSE @IdBahan END
END
