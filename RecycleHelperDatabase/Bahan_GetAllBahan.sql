/**
 *	Created by: Christian Soetanto
 *	Date: 26 April 2020
 *	Purpose: Get All Bahan
 */

CREATE PROCEDURE [dbo].[Bahan_GetAllBahan]
AS
BEGIN
	SELECT [IdBahan],[NamaBahan]
	FROM msBahan
END
