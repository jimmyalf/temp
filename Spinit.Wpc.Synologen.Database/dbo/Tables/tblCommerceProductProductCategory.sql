CREATE TABLE [dbo].[tblCommerceProductProductCategory] (
    [cPrdCatId] INT NOT NULL,
    [cPrdId]    INT NOT NULL,
    [cOrder]    INT NOT NULL,
    CONSTRAINT [PK_tblCommerceProductProductCategory] PRIMARY KEY CLUSTERED ([cPrdCatId] ASC, [cPrdId] ASC),
    CONSTRAINT [FK_tblCommerceProductProductCategory_tblCommerceProduct] FOREIGN KEY ([cPrdId]) REFERENCES [dbo].[tblCommerceProduct] ([cId]),
    CONSTRAINT [FK_tblCommerceProductProductCategory_tblCommerceProductCategory] FOREIGN KEY ([cPrdCatId]) REFERENCES [dbo].[tblCommerceProductCategory] ([cId])
);


GO
create TRIGGER tblCommerceProductProductCategory_UpdateOverride ON tblCommerceProductProductCategory 
   INSTEAD OF UPDATE
AS 
BEGIN
	SET NOCOUNT ON;
	DECLARE @newProductId INT
	DECLARE @oldProductId INT
	DECLARE @newCatId INT
	DECLARE @oldCatId INT
	DECLARE @newOrder INT
	DECLARE @oldOrder INT	
	DECLARE @orderCounter INT
	DECLARE @cursorOrder INT
	DECLARE @cursorProductId INT
	DECLARE @cursorCatId INT	
	
	DECLARE get_main_items_new CURSOR LOCAL FOR SELECT cPrdId, cPrdCatId, cOrder FROM INSERTED
	DECLARE get_main_items_old CURSOR LOCAL FOR SELECT cPrdId, cPrdCatId, cOrder FROM DELETED
	OPEN  get_main_items_new
	OPEN  get_main_items_old
	FETCH NEXT FROM  get_main_items_new INTO @newProductId, @newCatId, @newOrder
	FETCH NEXT FROM  get_main_items_old INTO @oldProductId, @oldCatId, @oldOrder	
	WHILE (@@FETCH_STATUS <> -1) BEGIN		
	
		SET @orderCounter = 0
		DECLARE get_orders CURSOR LOCAL FOR  SELECT cPrdId, cPrdCatId, cOrder FROM tblCommerceProductProductCategory WHERE cPrdCatId = @newCatId ORDER BY cOrder
		OPEN  get_orders
		FETCH NEXT FROM  get_orders INTO @cursorProductId, @cursorCatId, @cursorOrder
		WHILE (@@FETCH_STATUS <> -1) BEGIN	
			SET @orderCounter = @orderCounter + 1
			IF (@cursorOrder <> @orderCounter) BEGIN
				UPDATE tblCommerceProductProductCategory 
				SET cOrder = @orderCounter
				WHERE cPrdId = @cursorProductId AND cPrdCatId = @cursorCatId
			END
			FETCH NEXT FROM  get_orders INTO @cursorProductId, @cursorCatId, @cursorOrder
		END
		CLOSE get_orders
		DEALLOCATE get_orders	
		
		-- Get reorganized order numbers
		IF(@oldOrder > @newOrder) BEGIN --Move Up
			SELECT @oldOrder = cOrder 
			FROM tblCommerceProductProductCategory 
			WHERE cPrdId = @newProductId AND cPrdCatId = @newCatId
			IF(@oldOrder > 1) BEGIN 
				SET @newOrder = @oldOrder - 1
			END
			ELSE BEGIN --Is already on top
				SET @newOrder = @oldOrder
			END
		END
		ELSE IF(@oldOrder < @newOrder) BEGIN --Move Down
			SELECT @oldOrder = cOrder
			FROM tblCommerceProductProductCategory 
			WHERE cPrdId = @newProductId AND cPrdCatId = @newCatId
			IF(@oldOrder < @orderCounter) BEGIN 
				SET @newOrder = @oldOrder + 1
			END
			ELSE BEGIN --Is already on bottom
				SET @newOrder = @oldOrder
			END			
		END
		ELSE BEGIN --No move
			SELECT @oldOrder = cOrder 
			FROM tblCommerceProductProductCategory 
			WHERE cPrdId = @newProductId AND cPrdCatId = @newCatId
			SET @newOrder = @oldOrder
		END

		IF(@oldOrder <> @newOrder) BEGIN		
			--If new order number is taken by a row, replace it with old order number
			SELECT cOrder 
			FROM tblCommerceProductProductCategory 
			WHERE cOrder = @newOrder AND cPrdCatId = @newCatId
			IF( @@ROWCOUNT > 0 ) BEGIN
				UPDATE tblCommerceProductProductCategory SET cOrder = @oldOrder 
				WHERE cOrder = @newOrder AND cPrdCatId = @newCatId
			END
		END
			
		UPDATE	tblCommerceProductProductCategory SET	
			cPrdCatId = @newCatId
			,cPrdId = @newProductId
			,cOrder = @newOrder
		WHERE cPrdId = @oldProductId AND cPrdCatId = @oldCatId
		
		FETCH NEXT FROM  get_main_items_new INTO @newProductId, @newCatId, @newOrder
		FETCH NEXT FROM  get_main_items_old INTO @oldProductId, @oldCatId, @oldOrder
	END
	CLOSE get_main_items_new
	CLOSE get_main_items_old
	DEALLOCATE get_main_items_new
	DEALLOCATE get_main_items_old		
END

GO
create TRIGGER tblCommerceProductProductCategory_AfterDelete ON tblCommerceProductProductCategory 
AFTER DELETE AS BEGIN
	SET NOCOUNT ON
	
	DECLARE @categoryId INT
	DECLARE @orderCounter INT
	DECLARE @cursorOrder INT
	DECLARE @cursorProductId INT	
	
	--Sort all product-category-connections for each unique category in DELETED		
	DECLARE get_main_items CURSOR LOCAL FOR SELECT cPrdCatId FROM DELETED GROUP BY cPrdCatId
	OPEN  get_main_items
	FETCH NEXT FROM  get_main_items INTO @categoryId
	WHILE (@@FETCH_STATUS <> -1) BEGIN	
		SET @orderCounter = 0
		IF (@categoryId IS NOT NULL) BEGIN
			DECLARE get_connections CURSOR LOCAL FOR SELECT cPrdId,cOrder FROM tblCommerceProductProductCategory WHERE cPrdCatId = @categoryId ORDER BY cOrder
			OPEN  get_connections
			FETCH NEXT FROM  get_connections INTO @cursorProductId,@cursorOrder
			WHILE (@@FETCH_STATUS <> -1) BEGIN	
				SET @orderCounter = @orderCounter + 1
				IF (@cursorOrder <> @orderCounter) BEGIN
					UPDATE tblCommerceProductProductCategory SET cOrder = @orderCounter WHERE cPrdId = @cursorProductId AND cPrdCatId = @categoryId
				END
				FETCH NEXT FROM get_connections INTO @cursorProductId,@cursorOrder
			END
			CLOSE get_connections
			DEALLOCATE get_connections	
		END
		
		FETCH NEXT FROM  get_main_items INTO @categoryId
	END
	CLOSE get_main_items
	DEALLOCATE get_main_items
END
