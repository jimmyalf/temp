CREATE PROCEDURE spCommerceAddDeleteLocationConnection
					@action INT,
					@locId INT,
					@prdCatId INT,
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT
							
	IF @action = 0 BEGIN				
		SELECT @dummy = 1 FROM tblBaseLocations WHERE cId = @locId
		IF @@ROWCOUNT = 0 BEGIN
			SET @status = -6
			RETURN
		END

		SELECT	@dummy = 1 FROM tblCommerceProductCategory WHERE cId = @prdCatId
		IF @@ROWCOUNT = 0 BEGIN
			SET @status = -16
			RETURN
		END
		
		--Get new order
		DECLARE @parent INT
		DECLARE @newOrder INT
		SELECT @parent = cParent FROM tblCommerceProductCategory WHERE cId = @prdCatId
		IF @parent IS NOT NULL BEGIN				
			SELECT @newOrder = MAX(cOrder)+1 FROM tblCommerceProductCategory WHERE cParent = @parent
		END	
		ELSE BEGIN
			SELECT @newOrder = MAX(cOrder)+1 FROM tblCommerceProductCategory WHERE cParent IS NULL
		END
		IF @newOrder IS NULL BEGIN
			SET @newOrder = 1
		END	
		--end
		
		INSERT INTO tblCommerceLocationConnection (cLocId, cPrdCatId, cOrder)
		VALUES (@locId, @prdCatId, @newOrder)				
	END
	ELSE IF @action = 2
		BEGIN
			SELECT	@dummy = 1
			FROM	tblCommerceLocationConnection
			WHERE	cLocId = @locId
				AND	cPrdCatId = @prdCatId
						
			IF @@ROWCOUNT = 0
				BEGIN
					SET @status = -3
					RETURN
				END

			DELETE FROM tblCommerceLocationConnection
			WHERE		cLocId = @locId
				AND		cPrdCatId = @prdCatId
		END
	ELSE
		BEGIN
			SET @status = -1
			RETURN
		END
	
	SET @status = @@ERROR
END
