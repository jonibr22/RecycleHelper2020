/**
 *	Created by: Jonathan Ibrahim
 *	Date: 28 April 2020
 *	Purpose: Get All User
 */
CREATE PROCEDURE [dbo].[User_GetAllUser]
AS
BEGIN
	SELECT IdUser,Name,IdRole,Username,[Password]
	FROM msUser
END
