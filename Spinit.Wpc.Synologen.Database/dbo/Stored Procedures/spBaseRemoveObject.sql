CREATE PROCEDURE spBaseRemoveObject
@id INT, @status INT OUTPUT
AS
BEGIN
	DELETE FROM	tblBaseGroupsObjects
	WHERE		cObjectId = @id
	
	IF (@@ERROR != 0)
		BEGIN
			SET @status = @@ERROR
			RETURN
		END

	DELETE FROM	tblBaseObjects
	WHERE		cId = @id
		
	SET @status = @@ERROR
END
