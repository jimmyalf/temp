CREATE TABLE [dbo].[tblCommerceLocationConnection] (
    [cLocId]    INT NOT NULL,
    [cPrdCatId] INT NOT NULL,
    [cOrder]    INT CONSTRAINT [DF_tblCommerceLocationConnection_cOrder] DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_tblCommerceLocationConnection] PRIMARY KEY CLUSTERED ([cLocId] ASC, [cPrdCatId] ASC),
    CONSTRAINT [FK_tblCommerceLocationConnection_tblBaseLocations] FOREIGN KEY ([cLocId]) REFERENCES [dbo].[tblBaseLocations] ([cId]),
    CONSTRAINT [FK_tblCommerceLocationConnection_tblCommerceProductCategory] FOREIGN KEY ([cPrdCatId]) REFERENCES [dbo].[tblCommerceProductCategory] ([cId])
);


GO
create TRIGGER tblCommerceLocationConnection_AfterDelete ON tblCommerceLocationConnection 
AFTER DELETE AS BEGIN
	SET NOCOUNT ON
	
	DECLARE @location INT
	DECLARE @category INT
	DECLARE @parent INT
	DECLARE @orderCounter INT
	DECLARE @cursorOrder INT
	DECLARE @cursorLocationId INT
	DECLARE @cursorCategoryId INT	
	
	--Sort all categories with unique location and parent in DELETED 
	DECLARE get_main_items CURSOR LOCAL FOR SELECT cLocId, cPrdCatId FROM DELETED GROUP BY cLocId, cPrdCatId
	OPEN  get_main_items
	FETCH NEXT FROM  get_main_items INTO @location, @category
	WHILE (@@FETCH_STATUS <> -1) BEGIN		
	
		SET @orderCounter = 0
		SELECT @parent = cParent FROM tblCommerceProductCategory WHERE cId = @category	
		IF (@parent IS NULL) BEGIN
			DECLARE get_orders CURSOR LOCAL FOR	SELECT cLocId, cPrdCatId, tblCommerceLocationConnection.cOrder FROM tblCommerceLocationConnection INNER JOIN tblCommerceProductCategory ON tblCommerceProductCategory.cId = tblCommerceLocationConnection.cPrdCatId WHERE cLocId = @location AND tblCommerceProductCategory.cParent IS NULL ORDER BY tblCommerceLocationConnection.cOrder
		END
		ELSE BEGIN
			DECLARE get_orders CURSOR LOCAL FOR	SELECT cLocId, cPrdCatId, tblCommerceLocationConnection.cOrder FROM tblCommerceLocationConnection INNER JOIN tblCommerceProductCategory ON tblCommerceProductCategory.cId = tblCommerceLocationConnection.cPrdCatId WHERE cLocId = @location AND tblCommerceProductCategory.cParent = @parent ORDER BY tblCommerceLocationConnection.cOrder
		END
		OPEN  get_orders
		FETCH NEXT FROM  get_orders INTO @cursorLocationId, @cursorCategoryId, @cursorOrder
		WHILE (@@FETCH_STATUS <> -1) BEGIN	
			SET @orderCounter = @orderCounter + 1
			IF (@cursorOrder <> @orderCounter) BEGIN
				UPDATE tblCommerceLocationConnection SET cOrder = @orderCounter
				WHERE cLocId = @cursorLocationId AND cPrdCatId = @cursorCategoryId
			END
			FETCH NEXT FROM get_orders INTO @cursorLocationId, @cursorCategoryId, @cursorOrder
		END
		CLOSE get_orders
		DEALLOCATE get_orders
		
		FETCH NEXT FROM get_main_items INTO @location, @category
	END
	CLOSE get_main_items
	DEALLOCATE get_main_items			
		

END

GO
create TRIGGER tblCommerceLocationConnection_UpdateOverride ON tblCommerceLocationConnection 
INSTEAD OF UPDATE AS BEGIN
	SET NOCOUNT ON
	
	DECLARE @newLocation INT
	DECLARE @oldLocation INT	
	DECLARE @newCategory INT
	DECLARE @oldCategory INT	
	DECLARE @newParent INT
	DECLARE @oldParent INT
	DECLARE @newOrder INT
	DECLARE @oldOrder INT
	DECLARE @orderCounter INT

	DECLARE get_main_items_new CURSOR LOCAL FOR SELECT  cPrdCatId,  cLocId, cOrder FROM INSERTED
	DECLARE get_main_items_old CURSOR LOCAL FOR SELECT  cPrdCatId,  cLocId, cOrder FROM DELETED
	OPEN  get_main_items_new
	OPEN  get_main_items_old
	FETCH NEXT FROM  get_main_items_new INTO @newCategory, @newLocation, @newOrder
	FETCH NEXT FROM  get_main_items_old INTO @oldCategory, @oldLocation, @oldOrder
	WHILE (@@FETCH_STATUS <> -1) BEGIN			
		
		SET @orderCounter = 0
		SELECT @newParent = cParent FROM tblCommerceProductCategory WHERE cId = @newCategory
		SELECT @oldParent = cParent FROM tblCommerceProductCategory WHERE cId = @oldCategory
		
		--Sort all categories sharing location and parent
		DECLARE @cursorOrder INT
		DECLARE @cursorLocationId INT
		DECLARE @cursorCategoryId INT
		IF (@newParent IS NULL) BEGIN
			DECLARE get_orders CURSOR LOCAL FOR	
			SELECT cLocId, cPrdCatId, tblCommerceLocationConnection.cOrder FROM tblCommerceLocationConnection INNER JOIN tblCommerceProductCategory ON tblCommerceProductCategory.cId = tblCommerceLocationConnection.cPrdCatId WHERE cLocId = @newLocation AND tblCommerceProductCategory.cParent IS NULL ORDER BY tblCommerceLocationConnection.cOrder
		END
		ELSE BEGIN
			DECLARE get_orders CURSOR LOCAL FOR	
			SELECT cLocId, cPrdCatId, tblCommerceLocationConnection.cOrder FROM tblCommerceLocationConnection INNER JOIN tblCommerceProductCategory ON tblCommerceProductCategory.cId = tblCommerceLocationConnection.cPrdCatId WHERE cLocId = @newLocation AND tblCommerceProductCategory.cParent = @newParent ORDER BY tblCommerceLocationConnection.cOrder
		END
		OPEN  get_orders
		FETCH NEXT FROM  get_orders INTO @cursorLocationId, @cursorCategoryId, @cursorOrder
		WHILE (@@FETCH_STATUS <> -1) BEGIN	
			SET @orderCounter = @orderCounter + 1
			IF (@cursorOrder <> @orderCounter) BEGIN
				UPDATE tblCommerceLocationConnection SET cOrder = @orderCounter
				WHERE cLocId = @cursorLocationId AND cPrdCatId = @cursorCategoryId
			END
			FETCH NEXT FROM get_orders INTO @cursorLocationId, @cursorCategoryId, @cursorOrder
		END
		CLOSE get_orders
		DEALLOCATE get_orders
		

		-- Get reorganized order numbers
		IF(@oldOrder > @newOrder) BEGIN --Move Up
			SELECT @oldOrder = cOrder FROM tblCommerceLocationConnection WHERE cLocId = @newLocation AND cPrdCatId = @newCategory
			IF(@oldOrder > 1) BEGIN 
				SET @newOrder = @oldOrder - 1
			END
			ELSE BEGIN --Is already on top
				SET @newOrder = @oldOrder
			END
		END
		ELSE IF(@oldOrder < @newOrder) BEGIN --Move Down
			SELECT @oldOrder = cOrder FROM tblCommerceLocationConnection WHERE cLocId = @newLocation AND cPrdCatId = @newCategory
			IF(@oldOrder < @orderCounter) BEGIN 
				SET @newOrder = @oldOrder + 1
			END
			ELSE BEGIN --Is already on bottom
				SET @newOrder = @oldOrder
			END			
		END
		ELSE BEGIN --No move
			SELECT @oldOrder = cOrder FROM tblCommerceLocationConnection WHERE cLocId = @newLocation AND cPrdCatId = @newCategory
			SET @newOrder = @oldOrder
		END

		IF(@oldOrder <> @newOrder) BEGIN		
			--If new order number is taken by a row (sharing location and parent), replace it with old order number
			DECLARE @takenCategory INT
			DECLARE @takenLocation INT
			IF (@newParent IS NULL) BEGIN		
				SELECT @takenCategory = cPrdCatId, @takenLocation = cLocId FROM tblCommerceLocationConnection INNER JOIN tblCommerceProductCategory ON tblCommerceProductCategory.cId = tblCommerceLocationConnection.cPrdCatId WHERE cLocId = @newLocation AND tblCommerceProductCategory.cParent IS NULL AND tblCommerceLocationConnection.cOrder = @newOrder
			END
			ELSE BEGIN
				SELECT @takenCategory = cPrdCatId, @takenLocation = cLocId FROM tblCommerceLocationConnection INNER JOIN tblCommerceProductCategory ON tblCommerceProductCategory.cId = tblCommerceLocationConnection.cPrdCatId WHERE cLocId = @newLocation AND tblCommerceProductCategory.cParent = @newParent AND tblCommerceLocationConnection.cOrder = @newOrder
			END
			IF( @@ROWCOUNT > 0 ) BEGIN
				UPDATE tblCommerceLocationConnection SET cOrder = @oldOrder 
				WHERE cLocId = @takenLocation AND cPrdCatId = @takenCategory
			END
		END
			
		UPDATE tblCommerceLocationConnection SET	
			cLocId = @newLocation
			,cPrdCatId = @newCategory			
			,cOrder = @newOrder
		WHERE cLocId = @oldLocation AND cPrdCatId = @oldCategory

		FETCH NEXT FROM get_main_items_new INTO @newCategory, @newLocation, @newOrder
		FETCH NEXT FROM get_main_items_old INTO @oldCategory, @oldLocation, @oldOrder		
	END
	CLOSE get_main_items_new
	CLOSE get_main_items_old
	DEALLOCATE get_main_items_new	
	DEALLOCATE get_main_items_old	
		
END
