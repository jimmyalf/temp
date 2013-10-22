CREATE procedure spBaseChangeExternalComponent
@componentId INT,
					@adminLink NVARCHAR (500),
					@adminTarget NVARCHAR (256),
					@publicLink NVARCHAR (500),
					@publicTarget NVARCHAR (256),
					@winLogin NVARCHAR (256),
					@winPassword NVARCHAR (256),
					@winCookie BIT,
					@commonLogin NVARCHAR (256),
					@commonPassword NVARCHAR (256),
					@commonCookie BIT,
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE	@cmpAdminLink NVARCHAR (500),
					@cmpAdminTarget NVARCHAR (256),
					@cmpPublicLink NVARCHAR (500),
					@cmpPublicTarget NVARCHAR (256),
					@cmpWinLogin NVARCHAR (256),
					@cmpWinPassword NVARCHAR (256),
					@cmpWinCookie BIT,
					@cmpCommonLogin NVARCHAR (256),
					@cmpCommonPassword NVARCHAR (256),
					@cmpCommonCookie BIT
					
			DECLARE get_comp CURSOR LOCAL FOR
				SELECT	cAdminLink,
						cAdminTarget,
						cPublicLink,
						cPublicTarget,
						cWinLogin,
						cWinPassword,
						cWinCookie,
						cCommonLogin,
						cCommonPassword,
						cCommonCookie
				FROM	tblBaseExternalComponents
				WHERE	cComponentId = @componentId
				
			OPEN get_comp
			FETCH NEXT FROM get_comp INTO	@cmpAdminLink,
											@cmpAdminTarget,
											@cmpPublicLink,
											@cmpPublicTarget,
											@cmpWinLogin,
											@cmpWinPassword,
											@cmpWinCookie,
											@cmpCommonLogin,
											@cmpCommonPassword,
											@cmpCommonCookie
											
			IF (@@FETCH_STATUS = -1)
				BEGIN
					CLOSE get_comp
					DEALLOCATE get_comp
					
					SET @status = -2
					RETURN
				END
				
			CLOSE get_comp
			DEALLOCATE get_comp
			
			IF (@adminLink IS NOT NULL)
				BEGIN
					IF (LEN (@adminLink) = 0)
						BEGIN
							SET @cmpAdminLink = NULL
						END
					ELSE
						BEGIN
							SET @cmpAdminLink = @adminLink
						END
				END
				
			IF (@adminTarget IS NOT NULL)
				BEGIN
					IF (LEN (@adminTarget) = 0)
						BEGIN
							SET @cmpAdminTarget = NULL
						END
					ELSE
						BEGIN
							SET @cmpAdminTarget = @adminTarget
						END
				END
				
			IF (@publicLink IS NOT NULL)
				BEGIN
					IF (LEN (@publicLink) = 0)
						BEGIN
							SET @cmpPublicLink = NULL
						END
					ELSE
						BEGIN
							SET @cmpPublicLink = @publicLink
						END
				END
				
			IF (@publicTarget IS NOT NULL)
				BEGIN
					IF (LEN (@publicTarget) = 0)
						BEGIN
							SET @cmpPublicTarget = NULL
						END
					ELSE
						BEGIN
							SET @cmpPublicTarget = @publicTarget
						END
				END
				
			IF (@winLogin IS NOT NULL)
				BEGIN
					IF (LEN (@winLogin) = 0)
						BEGIN
							SET @cmpWinLogin = NULL
						END
					ELSE
						BEGIN
							SET @cmpWinLogin = @winLogin
						END
				END
				
			IF (@winPassword IS NOT NULL)
				BEGIN
					IF (LEN (@winPassword) = 0)
						BEGIN
							SET @cmpWinPassword = NULL
						END
					ELSE
						BEGIN
							SET @cmpWinPassword = @winPassword
						END
				END
				
			IF (@winCookie IS NOT NULL)
				BEGIN
					SET @cmpWinCookie = @winCookie
				END
				
			IF (@commonLogin IS NOT NULL)
				BEGIN
					IF (LEN (@commonLogin) = 0)
						BEGIN
							SET @cmpCommonLogin = NULL
						END
					ELSE
						BEGIN
							SET @cmpCommonLogin = @commonLogin
						END
				END
			
			IF (@commonPassword IS NOT NULL)
				BEGIN
					IF (LEN (@commonPassword) = 0)
						BEGIN
							SET @cmpCommonPassword = NULL
						END
					ELSE
						BEGIN
							SET @cmpCommonPassword = @commonPassword
						END
				END
				
			IF (@commonCookie IS NOT NULL)
				BEGIN
					SET @cmpCommonCookie = @commonCookie
				END
				
			UPDATE	tblBaseExternalComponents
			SET		cAdminLink = @cmpAdminLink,
					cAdminTarget = @cmpAdminTarget,
					cPublicLink = @cmpPublicLink,
					cPublicTarget = @cmpPublicTarget,
					cWinLogin = @cmpWinLogin,
					cWinPassword = @cmpWinPassword,
					cWinCookie = @cmpWinCookie,
					cCommonLogin = @cmpCommonLogin,
					cCommonPassword = @cmpCommonPassword,
					cCommonCookie = @cmpCommonCookie
			WHERE	cComponentId = @componentId
			
			SET @status = @@ERROR					
		END
