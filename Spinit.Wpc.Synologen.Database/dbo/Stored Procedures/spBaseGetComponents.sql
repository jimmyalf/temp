CREATE PROCEDURE spBaseGetComponents
					@type INT,
					@componentid INT,
					@locationid INT,
					@componentname NVARCHAR(256),
					@status INT OUTPUT
					
	AS
		IF (@type = 0)
			BEGIN
				SELECT	*
				FROM	tblBaseComponents
				WHERE	cId = @componentid
			END

		IF (@type = 1)
			BEGIN
				SELECT	*
				FROM	tblBaseComponents
			END

		IF (@type = 2)
			BEGIN
				SELECT	*
				FROM	tblBaseComponents, tblBaseLocationsComponents
				WHERE cLocationId = @locationid
				AND cComponentId = cId
			END
		
		IF (@type = 3)
			BEGIN
				SELECT	*
				FROM	tblBaseComponents
				WHERE cName = @componentname
			END

		IF (@@ERROR = 0)
			BEGIN
				SELECT @status = 0
			END
		ELSE
			BEGIN
				SELECT @status = @@ERROR
			END
