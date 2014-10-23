CREATE PROCEDURE spCommerceAddUpdateDeleteProduct
					@action INT,
					@id INT OUTPUT,
					@order INT,
					@parent INT,
					@prdNo NVARCHAR (255),
					@name NVARCHAR (512),
					@description NTEXT,
					@keyWords NTEXT,
					@picture INT,
					@dspTpeId INT,
					@active BIT,
					@approvedBy NVARCHAR (100),
					@approvedDate DATETIME,
					@lockedBy NVARCHAR (100),
					@lockedDate DATETIME,
					@userName NVARCHAR (100),
					@status INT OUTPUT
AS
BEGIN
	DECLARE @dummy INT,
			@max INT,
			@pOrder INT,
			@maxOrder INT
	
	--VALIDATE			
	IF (@action = 0 OR @action = 1) BEGIN --Check selected parent/picture/display-type exists
		IF @parent IS NOT NULL BEGIN
			SELECT	@dummy = 1 FROM tblCommerceProduct WHERE cId = @parent
			IF @@ROWCOUNT = 0 BEGIN
				SET @status = -7
				RETURN
			END
		END
		IF @picture IS NOT NULL BEGIN
			SELECT @dummy = 1 FROM	tblBaseFile WHERE cId = @picture
			IF @@ROWCOUNT = 0 BEGIN
				SET @status = -8
				RETURN
			END
		END		
		IF @dspTpeId IS NOT NULL BEGIN
			SELECT @dummy = 1 FROM tblCommerceDisplayType WHERE	cId = @dspTpeId
			IF @@ROWCOUNT = 0 BEGIN
				SET @status = -9
				RETURN
			END
		END				
	END
	IF (@action = 1 OR @action = 2) BEGIN --Check selected product exsists
		SELECT @dummy = 1 FROM tblCommerceProduct WHERE cId = @id
		IF @@ROWCOUNT = 0 BEGIN
			SET @status = -3
			RETURN
		END	
	END
				
				
	--CREATE			
	IF @action = 0 	BEGIN	
		IF @parent IS NOT NULL BEGIN				
			SELECT @max = MAX(cOrder)+1 FROM tblCommerceProduct WHERE cParent = @parent
		END	
		ELSE BEGIN --If parent is null then sorting is handled in category connection
			SET @max = 0
		END
		IF @max IS NULL BEGIN
			SET @max = 1
		END	
		
		INSERT INTO tblCommerceProduct
			(cOrder, cParent, cPrdNo, cName, cDescription, cKeyWords, cPicture,
			 cDspTpeId, cActive, cApprovedBy, cApprovedDate, cLockedBy,
			 cLockedDate, cCreatedBy, cCreatedDate)
		VALUES
			(@max, @parent, @prdNo, @name, @description, @keyWords, @picture,
			 @dspTpeId, @active, @approvedBy, @approvedDate, @lockedBy,
			 @lockedDate, @userName, GETDATE ())
			
		SET @id = @@IDENTITY
	 END
	 
	--UPDATE
	ELSE IF @action = 1 BEGIN	
		UPDATE tblCommerceProduct SET
			cOrder = @order,
			cParent = @parent,
			cPrdNo = @prdNo,
			cName = @name,
			cDescription = @description,
			ckeyWords = @keyWords,
			cPicture = @picture,
			cDspTpeId = @dspTpeId,
			cActive = @active,
			cApprovedBy = @approvedBy,
			cApprovedDate = @approvedDate,
			cLockedBy = @lockedBy,
			cLockedDate = @lockedDate,
			cChangedBy = @userName,
			cChangedDate = GETDATE ()
		WHERE cId = @id
	END
	--DELETE
	ELSE IF @action = 2 BEGIN
		DECLARE @statusRet INT	
		EXECUTE spCommerceDeleteProductRecursive @id, @statusRet OUTPUT

		IF (@statusRet <> 0) BEGIN
			SELECT @status = @statusRet
		END			

	END
	--IF NOT CREATE/UPDATE/DELETE
	ELSE BEGIN
		SET @status = -1
		RETURN
	END
	
	SET @status = @@ERROR
END
