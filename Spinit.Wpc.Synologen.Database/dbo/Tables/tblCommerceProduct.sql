CREATE TABLE [dbo].[tblCommerceProduct] (
    [cId]           INT            IDENTITY (1, 1) NOT NULL,
    [cOrder]        INT            NOT NULL,
    [cParent]       INT            NULL,
    [cPrdNo]        NVARCHAR (255) NULL,
    [cName]         NVARCHAR (512) NOT NULL,
    [cDescription]  NTEXT          NULL,
    [cKeyWords]     NVARCHAR (512) NULL,
    [cPicture]      INT            NULL,
    [cDspTpeId]     INT            NULL,
    [cActive]       BIT            NOT NULL,
    [cApprovedBy]   NVARCHAR (100) NULL,
    [cApprovedDate] DATETIME       NULL,
    [cLockedBy]     NVARCHAR (100) NULL,
    [cLockedDate]   DATETIME       NULL,
    [cCreatedBy]    NVARCHAR (100) NOT NULL,
    [cCreatedDate]  DATETIME       NOT NULL,
    [cChangedBy]    NVARCHAR (100) NULL,
    [cChangedDate]  DATETIME       NULL,
    CONSTRAINT [PK_tblCommerceProduct] PRIMARY KEY CLUSTERED ([cId] ASC),
    CONSTRAINT [FK_tblCommerceProduct_tblBaseFile] FOREIGN KEY ([cPicture]) REFERENCES [dbo].[tblBaseFile] ([cId]),
    CONSTRAINT [FK_tblCommerceProduct_tblCommerceDisplayType] FOREIGN KEY ([cDspTpeId]) REFERENCES [dbo].[tblCommerceDisplayType] ([cId]),
    CONSTRAINT [FK_tblCommerceProduct_tblCommerceProduct] FOREIGN KEY ([cParent]) REFERENCES [dbo].[tblCommerceProduct] ([cId])
);


GO
create TRIGGER tblCommerceProduct_UpdateOverride ON tblCommerceProduct 
INSTEAD OF UPDATE AS BEGIN
	SET NOCOUNT ON
	
	DECLARE @newProductId INT
	DECLARE @oldProductId INT
	DECLARE @newParent INT
	DECLARE @oldParent INT
	DECLARE @oldOrder INT
	DECLARE @newOrder INT
	DECLARE @orderCounter INT
	DECLARE @cursorOrder INT
	DECLARE @cursorProductId INT
	DECLARE @cursorFileId INT
	DECLARE @cursorLanguageId INT	
	
	DECLARE get_main_items_new CURSOR LOCAL FOR SELECT cId, cParent, cOrder FROM INSERTED
	DECLARE get_main_items_old CURSOR LOCAL FOR SELECT cId, cParent, cOrder FROM DELETED
	OPEN  get_main_items_new
	OPEN  get_main_items_old
	FETCH NEXT FROM  get_main_items_new INTO @newProductId, @newParent, @newOrder
	FETCH NEXT FROM  get_main_items_old INTO @oldProductId, @oldParent, @oldOrder
	WHILE (@@FETCH_STATUS <> -1) BEGIN			
		
		SET @orderCounter = 0
		IF (@newParent IS NOT NULL) BEGIN
			DECLARE get_orders CURSOR LOCAL FOR SELECT cId, cOrder FROM tblCommerceProduct WHERE cParent = @newParent ORDER BY cOrder
			OPEN  get_orders
			FETCH NEXT FROM  get_orders INTO @cursorProductId, @cursorOrder
			WHILE (@@FETCH_STATUS <> -1) BEGIN	
				SET @orderCounter = @orderCounter + 1
				IF (@cursorOrder <> @orderCounter) BEGIN
					UPDATE tblCommerceProduct SET cOrder = @orderCounter
					WHERE cId = @cursorProductId
				END
				FETCH NEXT FROM get_orders INTO @cursorProductId, @cursorOrder
			END
			CLOSE get_orders
			DEALLOCATE get_orders
		

			-- Get reorganized order numbers
			IF(@oldOrder > @newOrder) BEGIN --Move Up
				SELECT @oldOrder = cOrder FROM tblCommerceProduct WHERE cId = @newProductId
				IF(@oldOrder > 1) BEGIN 
					SET @newOrder = @oldOrder - 1
				END
				ELSE BEGIN --Is already on top
					SET @newOrder = @oldOrder
				END
			END
			ELSE IF(@oldOrder < @newOrder) BEGIN --Move Down
				SELECT @oldOrder = cOrder FROM tblCommerceProduct WHERE cId = @newProductId		
				IF(@oldOrder < @orderCounter) BEGIN 
					SET @newOrder = @oldOrder + 1
				END
				ELSE BEGIN --Is already on bottom
					SET @newOrder = @oldOrder
				END			
			END
			ELSE BEGIN --No move
				SELECT @oldOrder = cOrder FROM tblCommerceProduct WHERE cId = @newProductId		
				SET @newOrder = @oldOrder
			END

			IF(@oldOrder <> @newOrder) BEGIN		
				--If new order number is taken by a row, replace it with old order number
				SELECT cOrder FROM tblCommerceProduct WHERE cOrder = @newOrder AND cParent = @newParent
				IF( @@ROWCOUNT > 0 ) BEGIN
					UPDATE tblCommerceProduct SET cOrder = @oldOrder 
					WHERE cOrder = @newOrder AND cParent = @newParent
				END
			END
		END	
		ELSE BEGIN --If row is Product (no parent) then order is handled in category connection and should be 'null/0' here
			SET @newOrder = 0
		END
			
		UPDATE	tblCommerceProduct	SET	
				cOrder = @newOrder,
				cParent = Product.cParent,
				cPrdNo = Product.cPrdNo,
				cName = Product.cName,
				cDescription = Product.cDescription,
				ckeyWords = Product.ckeyWords,
				cPicture = Product.cPicture,
				cDspTpeId = Product.cDspTpeId,
				cActive = Product.cActive,
				cApprovedBy = Product.cApprovedBy,
				cApprovedDate = Product.cApprovedDate,
				cLockedBy = Product.cLockedBy,
				cLockedDate = Product.cLockedDate,
				cChangedBy = Product.cChangedBy,
				cChangedDate = Product.cChangedDate
			FROM ( SELECT cParent, cPrdNo, cName, cDescription,ckeyWords, cPicture, cDspTpeId, cActive, cApprovedBy, cApprovedDate, cLockedBy, cLockedDate, cChangedBy, cChangedDate FROM INSERTED WHERE cId = @newProductId ) AS Product
		WHERE cId = @oldProductId

		FETCH NEXT FROM get_main_items_new INTO @newProductId, @newParent, @newOrder
		FETCH NEXT FROM get_main_items_old INTO @oldProductId, @oldParent, @newOrder
	END
	CLOSE get_main_items_new
	CLOSE get_main_items_old
	DEALLOCATE get_main_items_new
	DEALLOCATE get_main_items_old
END

GO
create TRIGGER tblCommerceProduct_AfterDelete ON tblCommerceProduct 
AFTER DELETE AS BEGIN
	SET NOCOUNT ON
	
	DECLARE @parentId INT
	DECLARE @orderCounter INT
	DECLARE @cursorOrder INT
	DECLARE @cursorProductId INT	
	
	--Sort all articles for each unique parent in DELETED (do not sort products where parent is null)
	DECLARE get_main_items CURSOR LOCAL FOR SELECT cParent FROM DELETED WHERE cParent IS NOT NULL
	OPEN  get_main_items
	FETCH NEXT FROM  get_main_items INTO @parentId
	WHILE (@@FETCH_STATUS <> -1) BEGIN	
		SET @orderCounter = 0	
		DECLARE get_products CURSOR LOCAL FOR SELECT cId, cOrder FROM tblCommerceProduct WHERE cParent = @parentId ORDER BY cOrder
		OPEN  get_products
		FETCH NEXT FROM  get_products INTO @cursorProductId, @cursorOrder
		WHILE (@@FETCH_STATUS <> -1) BEGIN	
			SET @orderCounter = @orderCounter + 1
			IF (@cursorOrder <> @orderCounter) BEGIN
				UPDATE tblCommerceProduct SET cOrder = @orderCounter WHERE cId = @cursorProductId
			END
			FETCH NEXT FROM get_products INTO @cursorProductId, @cursorOrder
		END
		CLOSE get_products
		DEALLOCATE get_products	
		
		FETCH NEXT FROM get_main_items INTO @parentId
	END
	CLOSE get_main_items
	DEALLOCATE get_main_items			
		
END
