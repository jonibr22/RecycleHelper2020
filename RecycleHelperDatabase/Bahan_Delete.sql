/**
 *	Created by: Naomi Belinda May Arbai
 *	Date: 30 April 2020
 *	Purpose: Delete Bahan
 */

CREATE PROCEDURE [dbo].[Bahan_Delete]
	@IdBahan INT
AS
BEGIN
	DELETE msBahan
	WHERE IdBahan = @IdBahan
	SELECT 1
END
