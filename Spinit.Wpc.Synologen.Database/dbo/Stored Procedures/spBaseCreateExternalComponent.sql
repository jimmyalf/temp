CREATE procedure spBaseCreateExternalComponent
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
			INSERT INTO tblBaseExternalComponents
				(cComponentId, cAdminLink, cAdminTarget, cPublicLink,
				 cPublicTarget, cWinLogin, cWinPassword, cWinCookie,
				 cCommonLogin, cCommonPassword, cCommonCookie)
			VALUES
				(@componentId, @adminLink, @adminTarget, @publicLink,
				 @publicTarget, @winLogin, @winPassword, @winCookie,
				 @commonLogin, @commonPassword, @commonCookie)
				 
			SET @status = @@ERROR
		END
