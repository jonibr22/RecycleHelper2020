CREATE PROCEDURE [dbo].[Bahan_GetListByKategori]
	@IdKategori INT
AS
BEGIN
	SELECT IdBahan, NamaBahan, IdKategoriBahan
	FROM msBahan
	WHERE IdKategoriBahan = @IdKategori
END
