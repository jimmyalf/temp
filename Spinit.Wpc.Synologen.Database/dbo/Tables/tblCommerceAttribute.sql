CREATE TABLE [dbo].[tblCommerceAttribute] (
    [cId]           INT             IDENTITY (1, 1) NOT NULL,
    [cOrder]        INT             NOT NULL,
    [cName]         NVARCHAR (512)  NOT NULL,
    [cDescription]  NTEXT           NULL,
    [cDefaultValue] NVARCHAR (1024) NULL,
    [cCreatedBy]    NVARCHAR (100)  NULL,
    [cCreatedDate]  DATETIME        NULL,
    [cChangedBy]    NVARCHAR (100)  NULL,
    [cChangedDate]  DATETIME        NULL,
    CONSTRAINT [PK_tblCommerceAttribute] PRIMARY KEY CLUSTERED ([cId] ASC)
);


GO
create TRIGGER tblCommerceAttribute_UpdateOverride ON tblCommerceAttribute 
INSTEAD OF UPDATE AS BEGIN
	SET NOCOUNT ON
	
	DECLARE @newAttributeId INT
	DECLARE @oldAttributeId INT
	DECLARE @oldOrder INT
	DECLARE @newOrder INT
	DECLARE @orderCounter INT
	DECLARE @cursorOrder INT
	DECLARE @cursorAttributeId INT	
	
	DECLARE get_main_items_new CURSOR LOCAL FOR SELECT cId, cOrder FROM INSERTED
	DECLARE get_main_items_old CURSOR LOCAL FOR SELECT cId, cOrder FROM DELETED
	OPEN  get_main_items_new
	OPEN  get_main_items_old
	FETCH NEXT FROM get_main_items_new INTO @newAttributeId, @newOrder
	FETCH NEXT FROM get_main_items_old INTO @oldAttributeId, @oldOrder
	WHILE (@@FETCH_STATUS <> -1) BEGIN		
	
		SET @orderCounter = 0
		DECLARE get_orders CURSOR LOCAL FOR SELECT cId, cOrder FROM tblCommerceAttribute ORDER BY cOrder
		OPEN  get_orders
		FETCH NEXT FROM  get_orders INTO @cursorAttributeId, @cursorOrder
		WHILE (@@FETCH_STATUS <> -1) BEGIN	
			SET @orderCounter = @orderCounter + 1
			IF (@cursorOrder <> @orderCounter) BEGIN
				UPDATE tblCommerceAttribute SET cOrder = @orderCounter WHERE cId = @cursorAttributeId
			END
			FETCH NEXT FROM get_orders INTO @cursorAttributeId, @cursorOrder
		END
		CLOSE get_orders
		DEALLOCATE get_orders	
		

		-- Get reorganized order numbers
		IF(@oldOrder > @newOrder) BEGIN --Move Up
			SELECT @oldOrder = cOrder FROM tblCommerceAttribute WHERE cId = @newAttributeId
			IF(@oldOrder > 1) BEGIN 
				SET @newOrder = @oldOrder - 1
			END
			ELSE BEGIN --Is already on top
				SET @newOrder = @oldOrder
			END
		END
		ELSE IF(@oldOrder < @newOrder) BEGIN --Move Down
			SELECT @oldOrder = cOrder FROM tblCommerceAttribute WHERE cId = @newAttributeId
			IF(@oldOrder < @orderCounter) BEGIN 
				SET @newOrder = @oldOrder + 1
			END
			ELSE BEGIN --Is already on bottom
				SET @newOrder = @oldOrder
			END			
		END
		ELSE BEGIN --No move
			SELECT @oldOrder = cOrder FROM tblCommerceAttribute WHERE cId = @newAttributeId
			SET @newOrder = @oldOrder
		END

		IF(@oldOrder <> @newOrder) BEGIN		
			--If new order number is taken by a row, replace it with old order number
			SELECT cOrder FROM tblCommerceAttribute WHERE cOrder = @newOrder
			IF( @@ROWCOUNT > 0 ) BEGIN
				UPDATE tblCommerceAttribute SET cOrder = @oldOrder 
				WHERE cOrder = @newOrder
			END
		END
		
		UPDATE tblCommerceAttribute SET	
			cOrder = @newOrder			
			,cName = NewAttribute.cName
			,cDescription = NewAttribute.cDescription
			,cDefaultValue = NewAttribute.cDefaultValue
			,cCreatedBy = NewAttribute.cCreatedBy
			,cCreatedDate = NewAttribute.cCreatedDate
			,cChangedBy = NewAttribute.cChangedBy
			,cChangedDate = NewAttribute.cChangedDate
			FROM ( SELECT cName, cDescription, cDefaultValue, cCreatedBy, cCreatedDate, cChangedBy, cChangedDate FROM INSERTED WHERE cId = @newAttributeId) AS NewAttribute
		WHERE cId = @oldAttributeId
	
		FETCH NEXT FROM get_main_items_new INTO @newAttributeId, @newOrder
		FETCH NEXT FROM get_main_items_old INTO @oldAttributeId, @oldOrder		
	END
	CLOSE get_main_items_new
	CLOSE get_main_items_old
	DEALLOCATE get_main_items_new
	DEALLOCATE get_main_items_old

END

GO
create TRIGGER tblCommerceAttribute_AfterDelete ON tblCommerceAttribute 
AFTER DELETE AS BEGIN
	SET NOCOUNT ON

	DECLARE @orderCounter INT
	SET @orderCounter = 0
	
	/* Start reoganization */
	DECLARE @cursorOrder INT
	DECLARE @cursorAttributeId INT
	DECLARE get_orders CURSOR LOCAL FOR SELECT cId, cOrder FROM tblCommerceAttribute ORDER BY cOrder
	OPEN  get_orders
	FETCH NEXT FROM  get_orders INTO @cursorAttributeId, @cursorOrder
	WHILE (@@FETCH_STATUS <> -1) BEGIN	
		SET @orderCounter = @orderCounter + 1
		IF (@cursorOrder <> @orderCounter) BEGIN
			UPDATE tblCommerceAttribute SET cOrder = @orderCounter WHERE cId = @cursorAttributeId
		END
		FETCH NEXT FROM get_orders INTO @cursorAttributeId, @cursorOrder
	END
	CLOSE get_orders
	DEALLOCATE get_orders	
	/* End reorganization*/	
		

END
