/**
 *	Created by: Jonathan Ibrahim
 *	Date: 28 April 2020
 *	Purpose: Get All User
 */
/**
 *	Altered by: Jonathan Ibrahim
 *	Date: 02 Mei 2020
 *	Purpose: Tambah field Photo URL
 */
CREATE PROCEDURE [dbo].[User_GetAllUser]
AS
BEGIN
	SELECT IdUser,Name,IdRole,Username,[Password],PhotoUrl
	FROM msUser
END
