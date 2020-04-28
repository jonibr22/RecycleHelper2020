/**
 *	Created by: Jonathan Ibrahim
 *	Date: 28 April 2020
 *	Purpose: Insert Update Role
 */
CREATE PROCEDURE [dbo].[Role_InsertUpdate]
	@IdRole INT,
	@NamaRole VARCHAR(MAX)
AS
BEGIN
	DECLARE @RetVal INT
	IF(EXISTS(SELECT 1 FROM msRole WHERE IdRole = @IdRole))
	BEGIN
		UPDATE msRole
		SET [NamaRole] = @NamaRole
		WHERE IdRole = @IdRole

		SET @RetVal = @IdRole
	END
	ELSE
	BEGIN
		INSERT INTO msRole([NamaRole])
		VALUES (@NamaRole)

		SET @RetVal = SCOPE_IDENTITY()
	END
	SELECT @RetVal
END