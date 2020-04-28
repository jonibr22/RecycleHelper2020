/**
 *	Created by: Jonathan Ibrahim
 *	Date: 23 April 2020
 *	Purpose: Get User by Username
 */

CREATE PROCEDURE [dbo].[User_GetByUsername]
	@Username VARCHAR(MAX)
AS
BEGIN
	SELECT [IdUser],[Username],[Password],[Name],[IdRole]
	FROM msUser
	WHERE Username = @Username
END
