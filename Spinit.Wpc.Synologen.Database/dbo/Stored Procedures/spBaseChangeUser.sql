CREATE PROCEDURE [dbo].[spBaseChangeUser] 
					@id INT,
					@password NVARCHAR (100),
					@firstName NVARCHAR (100),
					@lastName NVARCHAR (100),
					@email NVARCHAR (512),
					@defaultLocation INT,
					@active BIT,
					@userId NVARCHAR (100),
					@status INT OUTPUT		
AS	
BEGIN
	DECLARE @dummy INT,
			@grpTpeId INT,
			@grpId INT,
			@found BIT,
			@usrId INT,
			@usrPassword NVARCHAR (100),
			@usrFirstName NVARCHAR (100),
			@usrLastName NVARCHAR (100),
			@usrEmail NVARCHAR (512),
			@usrDefaultLocation INT,
			@usrActive BIT
			
	DECLARE get_usr_id CURSOR LOCAL FOR
		SELECT	cId
		FROM	tblBaseUsers
		WHERE	cUserName = @userId
		
	OPEN get_usr_id
	FETCH NEXT FROM get_usr_id INTO @usrId
	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE get_usr_id
			DEALLOCATE get_usr_id
		
			SET @status = -2
			RETURN
		END
		
	CLOSE get_usr_id
	DEALLOCATE get_usr_id
	
	DECLARE get_usr CURSOR LOCAL FOR
		SELECT	cPassword,
				cFirstName,
				cLastName,
				cEmail,
				cDefaultLocation,
				cActive
		FROM	tblBaseUsers
		WHERE	cId = @id
		
	OPEN get_usr
	FETCH NEXT FROM get_usr INTO	@usrPassword,
									@usrFirstName,
									@usrLastName,
									@usrEmail,
									@usrDefaultLocation,
									@usrActive
	
	IF (@@FETCH_STATUS = -1)
		BEGIN
			CLOSE get_usr
			DEALLOCATE get_usr
			
			SET @status = -1
			RETURN
		END
		
	CLOSE get_usr
	DEALLOCATE get_usr
/*************************************************
CBERG 2011-02-02: Removed default location check 
**************************************************
	IF (@defaultLocation IS NOT NULL)
		BEGIN
			IF (@defaultLocation = -1)
				BEGIN
					SET @usrDefaultLocation = NULL
				END
			ELSE
				BEGIN
					DECLARE chk_exst CURSOR LOCAL FOR
						SELECT	1
						FROM	tblBaseLocations
						WHERE	cId = @defaultLocation
						
					OPEN chk_exst
					FETCH NEXT FROM chk_exst INTO @dummy 
					
					IF (@@FETCH_STATUS = -1)
						BEGIN
							CLOSE chk_exst
							DEALLOCATE chk_exst
							
							SET @status = -2
							RETURN
						END
						
					CLOSE chk_exst
					DEALLOCATE chk_exst
					
					DECLARE get_grpTpe CURSOR LOCAL FOR
						SELECT	cId,
								cGrpTpeId
						FROM	tblBaseGroups
						WHERE	cId IN (SELECT	cGroupId
										FROM	tblBaseUsersGroups
										WHERE	cUserId = @usrId)
						ORDER BY cGrpTpeId ASC
						
					OPEN get_grpTpe
					FETCH NEXT FROM get_grpTpe INTO	@grpId,
													@grpTpeId
					
					SET @found = 0
					
					WHILE (@@FETCH_STATUS <> -1)
						BEGIN
							IF ((@grpTpeId = 1) OR (@grpTpeId = 2))
								BEGIN
									SET @found = 1
									BREAK
								END
								
							DECLARE get_loc CURSOR LOCAL FOR
								SELECT	1
								FROM	tblBaseGroupsLocations
								WHERE	cGroupId = @grpId
									AND	cLocationid = @defaultLocation
									
							OPEN get_loc
							FETCH NEXT FROM get_loc INTO @dummy
									
							IF (@@FETCH_STATUS != -1)
								BEGIN
									CLOSE get_loc
									DEALLOCATE get_loc
									
									SET @found = 1
									BREAK
								END
								
							CLOSE get_loc
							DEALLOCATE get_loc
							
							FETCH NEXT FROM get_grpTpe INTO	@grpId,
															@grpTpeId
						END
						
					CLOSE get_grpTpe
					DEALLOCATE get_grpTpe
					
					IF (@found = 0)
						BEGIN
							SET @status = -2
							RETURN
						END
						
					SET @usrDefaultLocation = @defaultLocation
				END
		END
*/
	IF (@password IS NOT NULL)
		BEGIN
			IF (LEN (@password) = 0)
				BEGIN
					SET @usrPassword = NULL
				END
			ELSE
				BEGIN
					SET @usrPassword = @password
				END
		END
		
	IF (@firstName IS NOT NULL)
		BEGIN
			IF (LEN (@firstName) = 0)
				BEGIN
					SET @usrFirstName = NULL
				END
			ELSE
				BEGIN
					SET @usrFirstName = @firstName
				END
		END
		
	IF (@lastName IS NOT NULL)
		BEGIN
			IF (LEN (@lastName) = 0)
				BEGIN
					SET @usrLastName = NULL
				END
			ELSE
				BEGIN
					SET @usrLastName = @lastName
				END
		END

	IF (@email IS NOT NULL)
		BEGIN
			IF (LEN (@email) = 0)
				BEGIN
					SET @usrEmail = NULL
				END
			ELSE
				BEGIN
					SET @usrEmail = @email
				END
		END
		
	IF (@active IS NOT NULL)
		BEGIN
			SET @usrActive = @active
		END
		
	UPDATE	tblBaseUsers
	SET		cPassword = @usrPassword,
			cFirstName = @usrFirstName,
			cLastName = @usrLastName,
			cEmail = @usrEmail,
			cDefaultLocation = @defaultLocation,
			--@usrDefaultLocation,
			cActive = @usrActive
	WHERE	cId = @id
	
	SET @status = @@ERROR
END
