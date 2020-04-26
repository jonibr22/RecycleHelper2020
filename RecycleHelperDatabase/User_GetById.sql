/**
 *	Created by: Jonathan Ibrahim
 *	Date: 25 April 2020
 *	Purpose: Get User by Id
 */

CREATE PROCEDURE [dbo].[User_GetById]
	@Id INT
AS
BEGIN
	SELECT [Id],[Name],[Username],[Password],[RoleId]
	FROM msUser
	WHERE Id = @Id
END
