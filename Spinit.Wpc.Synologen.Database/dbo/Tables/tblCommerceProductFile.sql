CREATE TABLE [dbo].[tblCommerceProductFile] (
    [cPrdId]                 INT            NOT NULL,
    [cFleId]                 INT            NOT NULL,
    [cLngId]                 INT            NOT NULL,
    [cProductFileCategoryId] INT            NULL,
    [cName]                  NVARCHAR (50)  NULL,
    [cDescription]           NVARCHAR (512) NULL,
    [cOrder]                 INT            CONSTRAINT [DF_tblCommerceProductFile_cOrderId] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tblCommerceProductFile] PRIMARY KEY CLUSTERED ([cPrdId] ASC, [cFleId] ASC, [cLngId] ASC),
    CONSTRAINT [FK_tblCommerceProductFile_tblBaseFile] FOREIGN KEY ([cFleId]) REFERENCES [dbo].[tblBaseFile] ([cId]),
    CONSTRAINT [FK_tblCommerceProductFile_tblBaseLanguages] FOREIGN KEY ([cLngId]) REFERENCES [dbo].[tblBaseLanguages] ([cId]),
    CONSTRAINT [FK_tblCommerceProductFile_tblCommerceProduct] FOREIGN KEY ([cPrdId]) REFERENCES [dbo].[tblCommerceProduct] ([cId]),
    CONSTRAINT [FK_tblCommerceProductFile_tblCommerceProductFileCategory] FOREIGN KEY ([cProductFileCategoryId]) REFERENCES [dbo].[tblCommerceProductFileCategory] ([cId])
);


GO
create TRIGGER tblCommerceProductFile_AfterDelete ON tblCommerceProductFile 
AFTER DELETE AS BEGIN
	SET NOCOUNT ON
	
	DECLARE @productId INT
	DECLARE @orderCounter INT
	DECLARE @cursorOrder INT
	DECLARE @cursorProductId INT
	DECLARE @cursorFileId INT
	DECLARE @cursorLanguageId INT	
		
	--Sort all product files for each unique product in DELETED		
	DECLARE get_main_items CURSOR LOCAL FOR SELECT cPrdId FROM DELETED GROUP BY cPrdId
	OPEN  get_main_items
	FETCH NEXT FROM  get_main_items INTO @productId
	WHILE (@@FETCH_STATUS <> -1) BEGIN
		SET @orderCounter = 0	
		DECLARE get_orders CURSOR LOCAL FOR SELECT cPrdId, cFleId, cLngId, cOrder FROM tblCommerceProductFile WHERE cPrdId = @productId ORDER BY cOrder
		OPEN  get_orders
		FETCH NEXT FROM  get_orders INTO @cursorProductId, @cursorFileId, @cursorLanguageId, @cursorOrder
		WHILE (@@FETCH_STATUS <> -1) BEGIN	
			SET @orderCounter = @orderCounter + 1
			IF (@cursorOrder <> @orderCounter) BEGIN
				UPDATE tblCommerceProductFile SET cOrder = @orderCounter
				WHERE cPrdId = @cursorProductId AND cFleId = @cursorFileId AND cLngId = @cursorLanguageId
			END
			FETCH NEXT FROM get_orders INTO @cursorProductId, @cursorFileId, @cursorLanguageId, @cursorOrder
		END
		CLOSE get_orders
		DEALLOCATE get_orders
		
		FETCH NEXT FROM  get_main_items INTO @productId
	END
	CLOSE get_main_items
	DEALLOCATE get_main_items
END

GO
CREATE TRIGGER tblCommerceProductFile_UpdateOverride ON tblCommerceProductFile 
INSTEAD OF UPDATE AS BEGIN
	SET NOCOUNT ON
	
	DECLARE @newProductId INT
	DECLARE @oldProductId INT
	DECLARE @newFileId INT
	DECLARE @oldFileId INT
	DECLARE @newLanguageId INT
	DECLARE @oldLanguageId INT		
	DECLARE @newOrder INT	
	DECLARE @oldOrder INT
	DECLARE @orderCounter INT
	DECLARE @cursorOrder INT
	DECLARE @cursorProductId INT
	DECLARE @cursorFileId INT
	DECLARE @cursorLanguageId INT	
	
	
	DECLARE get_main_items_new CURSOR LOCAL FOR SELECT cPrdId, cFleId, cLngId, cOrder FROM INSERTED
	DECLARE get_main_items_old CURSOR LOCAL FOR SELECT cPrdId, cFleId, cLngId, cOrder FROM DELETED
	OPEN  get_main_items_new
	OPEN  get_main_items_old
	FETCH NEXT FROM  get_main_items_new INTO @newProductId, @newFileId, @newLanguageId, @newOrder
	FETCH NEXT FROM  get_main_items_old INTO @oldProductId, @oldFileId, @oldLanguageId, @oldOrder
	WHILE (@@FETCH_STATUS <> -1) BEGIN
		SET @orderCounter = 0	
		/* Start reoganization */
		DECLARE get_orders CURSOR LOCAL FOR SELECT cPrdId, cFleId, cLngId, cOrder FROM tblCommerceProductFile WHERE cPrdId = @newProductId ORDER BY cOrder
		OPEN  get_orders
		FETCH NEXT FROM  get_orders INTO @cursorProductId, @cursorFileId, @cursorLanguageId, @cursorOrder
		WHILE (@@FETCH_STATUS <> -1) BEGIN	
			SET @orderCounter = @orderCounter + 1
			IF (@cursorOrder <> @orderCounter) BEGIN
				UPDATE tblCommerceProductFile SET cOrder = @orderCounter
				WHERE cPrdId = @cursorProductId AND cFleId = @cursorFileId AND cLngId = @cursorLanguageId
			END
			FETCH NEXT FROM get_orders INTO @cursorProductId, @cursorFileId, @cursorLanguageId, @cursorOrder
		END
		CLOSE get_orders
		DEALLOCATE get_orders	
		/* End reorganization*/

		-- Get reorganized order numbers
		IF(@oldOrder > @newOrder) BEGIN --Move Up
			SELECT @oldOrder = cOrder FROM tblCommerceProductFile WHERE cPrdId = @newProductId AND cFleId = @newFileId AND cLngId = @newLanguageId
			IF(@oldOrder > 1) BEGIN 
				SET @newOrder = @oldOrder - 1
			END
			ELSE BEGIN --Is already on top
				SET @newOrder = @oldOrder
			END
		END
		ELSE IF(@oldOrder < @newOrder) BEGIN --Move Down
			SELECT @oldOrder = cOrder FROM tblCommerceProductFile WHERE cPrdId = @newProductId AND cFleId = @newFileId AND cLngId = @newLanguageId
			IF(@oldOrder < @orderCounter) BEGIN 
				SET @newOrder = @oldOrder + 1
			END
			ELSE BEGIN --Is already on bottom
				SET @newOrder = @oldOrder
			END			
		END
		ELSE BEGIN --No move
			SELECT @oldOrder = cOrder FROM tblCommerceProductFile WHERE cPrdId = @newProductId AND cFleId = @newFileId AND cLngId = @newLanguageId
			SET @newOrder = @oldOrder
		END

		IF(@oldOrder <> @newOrder) BEGIN		
			--If new order number is taken by a row, replace it with old order number
			SELECT cOrder FROM tblCommerceProductFile WHERE cOrder = @newOrder
			IF( @@ROWCOUNT > 0 ) BEGIN
				UPDATE tblCommerceProductFile SET cOrder = @oldOrder 
				WHERE cOrder = @newOrder AND cPrdId = @newProductId
			END
		END
			
		UPDATE	tblCommerceProductFile	SET	
			cPrdId = @newProductId
			,cFleId = @newFileId
			,cLngId = @newLanguageId
			,cOrder = @newOrder
			,cProductFileCategoryId = ProductFile.cProductFileCategoryId
			,cName = ProductFile.cName
			,cDescription = ProductFile.cDescription
			FROM ( SELECT cProductFileCategoryId, cName, cDescription FROM INSERTED WHERE cPrdId = @newProductId AND cFleId = @newFileId AND cLngId = @newLanguageId) AS ProductFile
		WHERE cPrdId = @oldProductId AND cFleId = @oldFileId AND cLngId = @oldLanguageId

		FETCH NEXT FROM  get_main_items_new INTO @newProductId, @newFileId, @newLanguageId, @newOrder
		FETCH NEXT FROM  get_main_items_old INTO @oldProductId, @oldFileId, @oldLanguageId, @oldOrder
	END
	CLOSE get_main_items_new
	CLOSE get_main_items_old
	DEALLOCATE get_main_items_new
	DEALLOCATE get_main_items_old

END
