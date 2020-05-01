/**
 *	Created by: Jonathan Ibrahim
 *	Date: 28 April 2020
 *	Purpose: Insert Detail Panduan
 */

CREATE PROCEDURE [dbo].[DetailPanduan_Insert]
	@IdPanduan INT,
	@IdBahan INT
AS
BEGIN
	INSERT INTO trDetailPanduan (IdPanduan,IdBahan)
	VALUES (@IdPanduan,@IdBahan)
	SELECT 1
END
