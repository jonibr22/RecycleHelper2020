/**
 *	Created by: Jonathan Ibrahim
 *	Date: 28 April 2020
 *	Purpose: Get All Role
 */
CREATE PROCEDURE [dbo].[Role_GetAllRole]
AS
BEGIN
	SELECT IdRole,NamaRole
	FROM msRole
END
