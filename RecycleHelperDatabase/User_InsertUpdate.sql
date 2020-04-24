/**
 *	Created by: Jonathan Ibrahim
 *	Date: 23 April 2020
 *	Purpose: Insert atau Update User
 */
CREATE PROCEDURE [dbo].[User_InsertUpdate]
	@Id INT,
	@Username VARCHAR(MAX),
	@Password VARCHAR(MAX),
	@Name VARCHAR(MAX),
	@RoleId INT
AS
BEGIN
	DECLARE @RetVal INT
	IF(EXISTS(SELECT 1 FROM msUser WHERE Id = @Id))
	BEGIN
		UPDATE msUser
		SET [Username] = @Username,
			[Name] = @Name,
			[Password] = @Password,
			[RoleId] = @RoleId
		WHERE Id = @Id

		SET @RetVal = @Id
	END
	ELSE
	BEGIN
		INSERT INTO msUser([Username],[Password],[Name],[RoleId])
		VALUES (@Username,@Password,@Name,@RoleId)

		SET @RetVal = SCOPE_IDENTITY()
	END
	SELECT @RetVal
END