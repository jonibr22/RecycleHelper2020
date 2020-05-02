/**
 *	Created by: Jonathan Ibrahim
 *	Date: 23 April 2020
 *	Purpose: Get User by Username
 */
/**
 *	Altered by: Jonathan Ibrahim
 *	Date: 02 Mei 2020
 *	Purpose: Tambah field Photo URL
 */
CREATE PROCEDURE [dbo].[User_GetByUsername]
	@Username VARCHAR(MAX)
AS
BEGIN
	SELECT [IdUser],[Username],[Password],[Name],[IdRole],[PhotoUrl]
	FROM msUser
	WHERE Username = @Username
END
