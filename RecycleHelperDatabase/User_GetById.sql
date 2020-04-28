/**
 *	Created by: Jonathan Ibrahim
 *	Date: 25 April 2020
 *	Purpose: Get User by Id
 */

CREATE PROCEDURE [dbo].[User_GetById]
	@IdUser INT
AS
BEGIN
	SELECT [IdUser],[Name],[Username],[Password],[IdRole]
	FROM msUser
	WHERE IdUser = @IdUser
END
