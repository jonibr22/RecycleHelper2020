/**
 *	Created by: Jonathan Ibrahim
 *	Date: 23 April 2020
 *	Purpose: Get User by Username
 */

CREATE PROCEDURE [dbo].[User_GetByUsername]
	@Username VARCHAR(MAX) = NULL
AS
BEGIN
	SELECT [Id],[Username],[Password],[Name],[RoleId]
	FROM msUser
	WHERE Username = @Username
END
