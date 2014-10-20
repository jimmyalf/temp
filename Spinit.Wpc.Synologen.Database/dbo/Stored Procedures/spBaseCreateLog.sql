CREATE PROCEDURE spBaseCreateLog
					@lgTpeId INT,
					@locId INT,
					@cmpId INT,
					@admin BIT,
					@exception NVARCHAR (500),
					@message NVARCHAR (2000),
					@ipAddress VARCHAR (15),
					@userAgent NVARCHAR (64),
					@httpReferrer NVARCHAR (256),
					@hash INT,
					@userId NVARCHAR (100),
					@id INT OUTPUT,
					@status INT OUTPUT
					
AS
BEGIN
	DECLARE @dummy INT,
			@background BIT
	
	SET @id = 0
	
	DECLARE chk_tpe CURSOR LOCAL FOR
		SELECT	1
		FROM	tblBaseLogTypes
		WHERE	cId = @lgTpeId
		
	OPEN chk_tpe
	FETCH NEXT FROM chk_tpe INTO @dummy
	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE chk_tpe
			DEALLOCATE chk_tpe
			
			SET @status = -1
			RETURN
		END
		
	CLOSE chk_tpe
	DEALLOCATE chk_tpe

	IF (@locId IS NOT NULL)
		BEGIN	
			DECLARE chk_loc CURSOR LOCAL FOR
				SELECT	1
				FROM	tblBaseLocations
				WHERE	cId = @locId
				
			OPEN chk_loc
			FETCH NEXT FROM chk_loc INTO @dummy
			
			IF (@@FETCH_STATUS = -1)
				BEGIN
					CLOSE chk_loc
					DEALLOCATE chk_loc
					
					SET @status = -2
					RETURN
				END
				
			CLOSE chk_loc
			DEALLOCATE chk_loc
		END
	
	IF (@cmpId IS NOT NULL)
		BEGIN
			DECLARE chk_cmp CURSOR LOCAL FOR
				SELECT	1
				FROM	tblBaseComponents
				WHERE	cId = @cmpId
				
			OPEN chk_cmp
			FETCH NEXT FROM chk_cmp INTO @dummy
			
			IF (@@FETCH_STATUS = -1)
				BEGIN
					CLOSE chk_cmp
					DEALLOCATE chk_cmp
					
					SET @status = -3
					RETURN
				END
				
			CLOSE chk_cmp
			DEALLOCATE chk_cmp
			
			SET @background = 0
		END
	ELSE
		BEGIN
			SET @background = 1
		END
		
	DECLARE chk_exist CURSOR LOCAL FOR
		SELECT	cId
		FROM	tblBaseLog
		WHERE	cHash = @hash
			AND	cCreatedDate IS NOT NULL
			AND cCreatedDate > DATEADD (HOUR, -1, GETDATE ())
			
	OPEN chk_exist
	FETCH NEXT FROM chk_exist INTO @id
	
	IF (@@FETCH_STATUS <> -1)
		BEGIN
			UPDATE	tblBaseLog
			SET		cCount = cCount + 1,
					cChangedBy = @userId,
					cChangedDate = GETDATE ()
			WHERE	cId = @id
		END
	ELSE
		BEGIN
			INSERT INTO tblBaseLog
				(cLgTpeId, cLocId, cCmpId, cBackground, cAdmin, cHash, cCount,
				 cException, cMessage, cIpAddress, cUserAgent, cHttpReferrer,
				 cCreatedBy, cCreatedDate)
			VALUES
				(@lgTpeId, @locId, @cmpId, @background, @admin, @hash, 1,
				 @exception, @message, @ipAddress, @userAgent, @httpReferrer,
				 @userId, GETDATE ())
				 
			SET @id = @@IDENTITY
		END
		
	SET @status = @@ERROR
END
