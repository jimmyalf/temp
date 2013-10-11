CREATE TABLE [dbo].[tblCommerceProductAttribute] (
    [cPrdId] INT             NOT NULL,
    [cAttId] INT             NOT NULL,
    [cOrder] INT             NOT NULL,
    [cValue] NVARCHAR (1024) NULL,
    CONSTRAINT [PK_tblCommerceProductAttribute] PRIMARY KEY CLUSTERED ([cPrdId] ASC, [cAttId] ASC),
    CONSTRAINT [FK_tblCommerceProductAttribute_tblCommerceAttribute] FOREIGN KEY ([cAttId]) REFERENCES [dbo].[tblCommerceAttribute] ([cId]),
    CONSTRAINT [FK_tblCommerceProductAttribute_tblCommerceProduct] FOREIGN KEY ([cPrdId]) REFERENCES [dbo].[tblCommerceProduct] ([cId])
);


GO
create TRIGGER tblCommerceProductAttribute_AfterDelete ON tblCommerceProductAttribute 
AFTER DELETE AS BEGIN
	SET NOCOUNT ON
	
	DECLARE @productId INT
	DECLARE @orderCounter INT
	DECLARE @cursorOrder INT
	DECLARE @cursorProductId INT
	DECLARE @cursorAttributeId INT	
	
	--Sort all attributes for each unique product in DELETED
	DECLARE get_main_items CURSOR LOCAL FOR SELECT cPrdId FROM DELETED GROUP BY cPrdId
	OPEN  get_main_items
	FETCH NEXT FROM  get_main_items INTO @productId
	WHILE (@@FETCH_STATUS <> -1) BEGIN	
		SET @orderCounter = 0		
		DECLARE get_orders CURSOR LOCAL FOR SELECT cPrdId, cAttId, cOrder FROM tblCommerceProductAttribute WHERE cPrdId = @productId ORDER BY cOrder
		OPEN  get_orders
		FETCH NEXT FROM  get_orders INTO @cursorProductId, @cursorAttributeId, @cursorOrder
		WHILE (@@FETCH_STATUS <> -1) BEGIN	
			SET @orderCounter = @orderCounter + 1
			IF (@cursorOrder <> @orderCounter) BEGIN
				UPDATE tblCommerceProductAttribute SET cOrder = @orderCounter
				WHERE cPrdId = @cursorProductId AND cAttId = @cursorAttributeId
			END
			FETCH NEXT FROM get_orders INTO @cursorProductId, @cursorAttributeId, @cursorOrder
		END
		CLOSE get_orders
		DEALLOCATE get_orders	
		
		FETCH NEXT FROM get_main_items INTO @productId
	END
	CLOSE get_main_items
	DEALLOCATE get_main_items			
		
END

GO
CREATE TRIGGER tblCommerceProductAttribute_UpdateOverride ON tblCommerceProductAttribute 
INSTEAD OF UPDATE AS BEGIN
	SET NOCOUNT ON
	
	DECLARE @newProductId INT
	DECLARE @oldProductId INT
	DECLARE @newAttributeId INT
	DECLARE @oldAttributeId INT
	DECLARE @oldOrder INT
	DECLARE @newOrder INT
	DECLARE @orderCounter INT
	DECLARE @cursorOrder INT
	DECLARE @cursorProductId INT
	DECLARE @cursorAttributeId INT	

	DECLARE get_main_items_new CURSOR LOCAL FOR SELECT cPrdId, cAttId, cOrder FROM INSERTED	
	DECLARE get_main_items_old CURSOR LOCAL FOR SELECT cPrdId, cAttId, cOrder FROM DELETED
	OPEN  get_main_items_new
	OPEN  get_main_items_old
	FETCH NEXT FROM  get_main_items_new INTO @newProductId, @newAttributeId, @newOrder
	FETCH NEXT FROM  get_main_items_old INTO @oldProductId, @oldAttributeId, @oldOrder	
	WHILE (@@FETCH_STATUS <> -1) BEGIN		
		
		/* Start reoganization */
		SET @orderCounter = 0		
		DECLARE get_orders CURSOR LOCAL FOR SELECT cPrdId, cAttId, cOrder FROM tblCommerceProductAttribute WHERE cPrdId = @newProductId ORDER BY cOrder
		OPEN  get_orders
		FETCH NEXT FROM  get_orders INTO @cursorProductId, @cursorAttributeId, @cursorOrder
		WHILE (@@FETCH_STATUS <> -1) BEGIN	
			SET @orderCounter = @orderCounter + 1
			IF (@cursorOrder <> @orderCounter) BEGIN
				UPDATE tblCommerceProductAttribute SET cOrder = @orderCounter
				WHERE cPrdId = @cursorProductId AND cAttId = @cursorAttributeId
			END
			FETCH NEXT FROM get_orders INTO @cursorProductId, @cursorAttributeId, @cursorOrder
		END
		CLOSE get_orders
		DEALLOCATE get_orders	
		/* End reorganization*/
		

		-- Get reorganized order numbers
		IF(@oldOrder > @newOrder) BEGIN --Move Up
			SELECT @oldOrder = cOrder FROM tblCommerceProductAttribute WHERE cPrdId = @newProductId AND cAttId = @newAttributeId
			IF(@oldOrder > 1) BEGIN 
				SET @newOrder = @oldOrder - 1
			END
			ELSE BEGIN --Is already on top
				SET @newOrder = @oldOrder
			END
		END
		ELSE IF(@oldOrder < @newOrder) BEGIN --Move Down
			SELECT @oldOrder = cOrder FROM tblCommerceProductAttribute WHERE cPrdId = @newProductId AND cAttId = @newAttributeId
			IF(@oldOrder < @orderCounter) BEGIN 
				SET @newOrder = @oldOrder + 1
			END
			ELSE BEGIN --Is already on bottom
				SET @newOrder = @oldOrder
			END			
		END
		ELSE BEGIN --No move
			SELECT @oldOrder = cOrder FROM tblCommerceProductAttribute WHERE cPrdId = @newProductId AND cAttId = @newAttributeId
			SET @newOrder = @oldOrder
		END

		IF(@oldOrder <> @newOrder) BEGIN		
			--If new order number is taken by a row, replace it with old order number
			SELECT cOrder FROM tblCommerceProductAttribute WHERE cOrder = @newOrder AND cPrdId = @newProductId
			IF( @@ROWCOUNT > 0 ) BEGIN
				UPDATE tblCommerceProductAttribute SET cOrder = @oldOrder 
				WHERE cOrder = @newOrder AND cPrdId = @newProductId
			END
		END
			
		UPDATE	tblCommerceProductAttribute	SET	
			cPrdId = @newProductId
			,cAttId = @newAttributeId
			,cOrder = @newOrder
			,cValue = ProductAttribute.cValue
			FROM ( SELECT cValue FROM INSERTED WHERE cPrdId = @newProductId AND cAttId = @newAttributeId) AS ProductAttribute
		WHERE cPrdId = @oldProductId AND cAttId = @oldAttributeId

		FETCH NEXT FROM  get_main_items_new INTO @newProductId, @newAttributeId, @newOrder
		FETCH NEXT FROM  get_main_items_old INTO @oldProductId, @oldAttributeId, @oldOrder	
	END
	CLOSE get_main_items_new
	CLOSE get_main_items_old
	DEALLOCATE get_main_items_new
	DEALLOCATE get_main_items_old

END
