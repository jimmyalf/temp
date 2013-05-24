CREATE PROCEDURE spCommerceAddUpdateDeleteProductFileCategory
			@action INT, 
			@id INT OUTPUT, 
			@name NVARCHAR(50) = NULL,
			@status INT OUTPUT
AS BEGIN
	DECLARE @dummy INT
	--VALIDATION
	IF (@action = 0) BEGIN
		SELECT	@dummy = 1 FROM	tblCommerceProductFileCategory WHERE cId = @id
		IF @@ROWCOUNT > 0 BEGIN
			SET @status = -2
			RETURN
		END		
	END
	ELSE IF @action = 2 BEGIN
		SELECT	@dummy = 1 FROM	tblCommerceProductFileCategory WHERE cId = @id
		IF @@ROWCOUNT = 0 BEGIN
			SET @status = -3
			RETURN
		END
	END 

	--CREATE				
	IF (@action = 0) BEGIN
		INSERT INTO tblCommerceProductFileCategory(cName) VALUES (@name)
		SET @id = @@IDENTITY
	END
	--UPDATE
	ELSE IF (@action = 1) BEGIN	
		UPDATE tblCommerceProductFileCategory SET cName  = @name
		WHERE cId = @id
	END	
	--DELETE
	ELSE IF (@action = 2) BEGIN	
		DELETE FROM tblCommerceProductFileCategory
		WHERE cId = @id
	END
	ELSE BEGIN
		SET @status = -1
		RETURN
	END
	
	SET @status = @@ERROR
END
