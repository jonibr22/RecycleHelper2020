/**
 *	Created by: Jonathan Ibrahim
 *	Date: 28 April 2020
 *	Purpose: Get List Bahan by Panduan Id
 */

CREATE PROCEDURE [dbo].[Bahan_GetListByPanduan]
	@IdPanduan INT
AS
BEGIN
	SELECT DISTINCT A.IdBahan, A.NamaBahan, A.IdKategoriBahan
	FROM msBahan A JOIN trDetailPanduan B ON A.IdBahan = B.IdBahan
	JOIN trPanduan C ON B.IdPanduan = C.IdPanduan
	WHERE C.IdPanduan = @IdPanduan
END
