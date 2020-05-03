/**
 *	Created by: Jonathan Ibrahim
 *	Date: 23 April 2020
 *	Purpose: Insert atau Update User
 */
/**
 *	Altered by: Jonathan Ibrahim
 *	Date: 02 Mei 2020
 *	Purpose: Tambah field Photo URL
 */
CREATE PROCEDURE [dbo].[User_InsertUpdate]
	@IdUser INT,
	@Username VARCHAR(MAX),
	@Password VARCHAR(MAX),
	@Name VARCHAR(MAX),
	@IdRole INT,
	@PhotoUrl VARCHAR(MAX)
AS
BEGIN
	DECLARE @RetVal INT
	IF(EXISTS(SELECT 1 FROM msUser WHERE IdUser = @IdUser))
	BEGIN
		UPDATE msUser
		SET [Username] = @Username,
			[Name] = @Name,
			[Password] = @Password,
			[IdRole] = @IdRole,
			[PhotoUrl] = @PhotoUrl
		WHERE IdUser = @IdUser

		SET @RetVal = @IdUser
	END
	ELSE
	BEGIN
		INSERT INTO msUser([Username],[Password],[Name],[IdRole],[PhotoUrl])
		VALUES (@Username,@Password,@Name,@IdRole,@PhotoUrl)

		SET @RetVal = SCOPE_IDENTITY()
	END
	SELECT @RetVal
END