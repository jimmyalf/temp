create PROCEDURE spBaseClearLog
					@id INT,
					@userId NVARCHAR (100),
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
	
	DECLARE chk_log CURSOR LOCAL FOR
		SELECT	1
		FROM	tblBaseLog
		WHERE	cId = @id
		
	OPEN chk_log
	FETCH NEXT FROM chk_log INTO @dummy
	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE chk_log
			DEALLOCATE chk_log
			
			SET @status = -1
			RETURN
		END
		
	CLOSE chk_log
	DEALLOCATE chk_log
	
	UPDATE	tblBaseLog
	SET		cClearedBy = @userId,
			cClearedDate = GETDATE ()
	WHERE	cId = @id
	
	SET @status = @@ERROR
END
