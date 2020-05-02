/**
 *	Created by: Jonathan Ibrahim
 *	Date: 25 April 2020
 *	Purpose: Get User by Id
 */
/**
 *	Altered by: Jonathan Ibrahim
 *	Date: 02 Mei 2020
 *	Purpose: Tambah field Photo URL
 */
CREATE PROCEDURE [dbo].[User_GetById]
	@IdUser INT
AS
BEGIN
	SELECT [IdUser],[Name],[Username],[Password],[IdRole],[PhotoUrl]
	FROM msUser
	WHERE IdUser = @IdUser
END
