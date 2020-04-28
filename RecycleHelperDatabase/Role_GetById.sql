/**
 *	Created by: Jonathan Ibrahim
 *	Date: 28 April 2020
 *	Purpose: Get Role by Id
 */

CREATE PROCEDURE [dbo].[Role_GetById]
	@IdRole INT
AS
BEGIN
	SELECT IdRole,NamaRole
	FROM msRole
	WHERE IdRole = @IdRole
END
