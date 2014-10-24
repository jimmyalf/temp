CREATE PROCEDURE spCommerceAddUpdateDeleteOrderStatus
					@action INT,
					@id INT OUTPUT,
					@name NVARCHAR (512),
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
							
	IF @action = 0
		BEGIN				
			INSERT INTO tblCommerceOrderStatus
				(cName)
			VALUES
				(@name)
				
			SET @id = @@IDENTITY
		 END
	ELSE IF @action = 1
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceOrderStatus
			WHERE	cId = @id
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			UPDATE	tblCommerceOrderStatus
			SET		cName = @name
			WHERE	cId = @id
		END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceOrderStatus
			WHERE	cId = @id
			
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END
				
			DELETE FROM tblCommerceOrderStatus
			WHERE		cId = @id
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
