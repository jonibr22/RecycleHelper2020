/**
 *	Created by: Jonathan Ibrahim
 *	Date: 28 April 2020
 *	Purpose: Delete Detail Panduan
 */

CREATE PROCEDURE [dbo].[DetailPanduan_Delete]
	@IdPanduan INT,
	@IdBahan INT
AS
BEGIN
	DELETE trDetailPanduan
	WHERE IdPanduan = @IdPanduan AND IdBahan = @IdBahan
	SELECT 1
END
