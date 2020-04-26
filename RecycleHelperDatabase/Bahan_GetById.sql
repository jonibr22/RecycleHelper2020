/**
 *	Created by: Jonathan Ibrahim
 *	Date: 26 April 2020
 *  Purpose: Get Bahan by Id
 */

CREATE PROCEDURE [dbo].[Bahan_GetById]
	@Id INT
AS
BEGIN
	SELECT IdBahan, NamaBahan
	FROM msBahan
	WHERE IdBahan = @Id
END
