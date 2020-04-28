/**
 *	Created by: Jonathan Ibrahim
 *	Date: 26 April 2020
 *	Purpose: Get All Kategori Bahan
 */
CREATE PROCEDURE [dbo].[KategoriBahan_GetAllKategoriBahan]
AS
BEGIN
	SELECT IdKategoriBahan,NamaKategoriBahan
	FROM msKategoriBahan
END