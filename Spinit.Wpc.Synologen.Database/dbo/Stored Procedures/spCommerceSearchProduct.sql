CREATE PROCEDURE spCommerceSearchProduct
					@type INT,
					@id INT,
					@prdCatId INT,
					@parent INT,
					@dspTpeId INT,
					@attId INT,
					@ordId INT,
					@ordItmId INT,
					@name NVARCHAR (512),
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cId,
					cOrder,
					cParent,
					cPrdNo,
					cName,
					cDescription,
					cKeyWords,
					cPicture,
					cDspTpeId,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceProduct
			ORDER BY cOrder ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	tblCommerceProduct.cId,
					tblCommerceProduct.cOrder,
					tblCommerceProduct.cParent,
					tblCommerceProduct.cPrdNo,
					tblCommerceProduct.cName,
					tblCommerceProduct.cDescription,
					tblCommerceProduct.cKeyWords,
					tblCommerceProduct.cPicture,
					tblCommerceProduct.cDspTpeId,
					tblCommerceProduct.cActive,
					tblCommerceProduct.cApprovedBy,
					tblCommerceProduct.cApprovedDate,
					tblCommerceProduct.cLockedBy,
					tblCommerceProduct.cLockedDate,
					tblCommerceProduct.cCreatedBy,
					tblCommerceProduct.cCreatedDate,
					tblCommerceProduct.cChangedBy,
					tblCommerceProduct.cChangedDate
			FROM	tblCommerceProduct
				INNER JOIN tblCommerceProductProductCategory
					ON tblCommerceProductProductCategory.cPrdId 
							= tblCommerceProduct.cId
			WHERE	tblCommerceProductProductCategory.cPrdCatId = @prdCatId
			ORDER BY dbo.tblCommerceProductProductCategory.cOrder ASC
		END
							
	IF @type = 2
		BEGIN
			SELECT	cId,
					cOrder,
					cParent,
					cPrdNo,
					cName,
					cDescription,
					cKeyWords,
					cPicture,
					cDspTpeId,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceProduct
			WHERE	cParent = @parent
			ORDER BY cOrder ASC
		END

	IF @type = 3
		BEGIN
			SELECT	cId,
					cOrder,
					cParent,
					cPrdNo,
					cName,
					cDescription,
					cKeyWords,
					cPicture,
					cDspTpeId,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceProduct
			WHERE	cDspTpeId = @dspTpeId
			ORDER BY cOrder ASC
		END

	IF @type = 4
		BEGIN
			SELECT	tblCommerceProduct.cId,
					tblCommerceProduct.cOrder,
					tblCommerceProduct.cParent,
					tblCommerceProduct.cPrdNo,
					tblCommerceProduct.cName,
					tblCommerceProduct.cDescription,
					tblCommerceProduct.cKeyWords,
					tblCommerceProduct.cPicture,
					tblCommerceProduct.cDspTpeId,
					tblCommerceProduct.cActive,
					tblCommerceProduct.cApprovedBy,
					tblCommerceProduct.cApprovedDate,
					tblCommerceProduct.cLockedBy,
					tblCommerceProduct.cLockedDate,
					tblCommerceProduct.cCreatedBy,
					tblCommerceProduct.cCreatedDate,
					tblCommerceProduct.cChangedBy,
					tblCommerceProduct.cChangedDate
			FROM	tblCommerceProduct
				INNER JOIN tblCommerceProductAttribute
					ON tblCommerceProductAttribute.cPrdId = tblCommerceProduct.cId
			WHERE	cAttId = @attId
			ORDER BY dbo.tblCommerceProduct.cOrder ASC
		END

	IF @type = 5
		BEGIN
			SELECT	tblCommerceProduct.cId,
					tblCommerceProduct.cOrder,
					tblCommerceProduct.cParent,
					tblCommerceProduct.cPrdNo,
					tblCommerceProduct.cName,
					tblCommerceProduct.cDescription,
					tblCommerceProduct.cKeyWords,
					tblCommerceProduct.cPicture,
					tblCommerceProduct.cDspTpeId,
					tblCommerceProduct.cActive,
					tblCommerceProduct.cApprovedBy,
					tblCommerceProduct.cApprovedDate,
					tblCommerceProduct.cLockedBy,
					tblCommerceProduct.cLockedDate,
					tblCommerceProduct.cCreatedBy,
					tblCommerceProduct.cCreatedDate,
					tblCommerceProduct.cChangedBy,
					tblCommerceProduct.cChangedDate
			FROM	tblCommerceProduct
				INNER JOIN tblCommerceOrderItem
					ON tblCommerceOrderItem.cPrdId = tblCommerceProduct.cId
				INNER JOIN tblCommerceOrder
					ON tblCommerceOrder.cId = tblCommerceOrderItem.cOrdId
			WHERE	tblCommerceOrder.cId = @ordId
			ORDER BY cOrder ASC
		END

	IF @type = 6
		BEGIN
			SELECT	tblCommerceProduct.cId,
					tblCommerceProduct.cOrder,
					tblCommerceProduct.cParent,
					tblCommerceProduct.cPrdNo,
					tblCommerceProduct.cName,
					tblCommerceProduct.cDescription,
					tblCommerceProduct.cKeyWords,
					tblCommerceProduct.cPicture,
					tblCommerceProduct.cDspTpeId,
					tblCommerceProduct.cActive,
					tblCommerceProduct.cApprovedBy,
					tblCommerceProduct.cApprovedDate,
					tblCommerceProduct.cLockedBy,
					tblCommerceProduct.cLockedDate,
					tblCommerceProduct.cCreatedBy,
					tblCommerceProduct.cCreatedDate,
					tblCommerceProduct.cChangedBy,
					tblCommerceProduct.cChangedDate
			FROM	tblCommerceProduct
				INNER JOIN tblCommerceOrderItem
					ON tblCommerceOrderItem.cPrdId = tblCommerceProduct.cId
			WHERE	tblCommerceOrderItem.cId = @ordItmId
			ORDER BY cOrder ASC
		END

	IF @type = 7
		BEGIN
			SELECT	cId,
					cOrder,
					cParent,
					cPrdNo,
					cName,
					cDescription,
					cKeyWords,
					cPicture,
					cDspTpeId,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceProduct
			WHERE	cName LIKE '%' + @name + '%' OR cPrdNo LIKE '%' + @name + '%'
			ORDER BY cOrder ASC
		END

	IF @type = 8
		BEGIN
			SELECT	cId,
					cOrder,
					cParent,
					cPrdNo,
					cName,
					cDescription,
					cKeyWords,
					cPicture,
					cDspTpeId,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceProduct
			WHERE	cParent IS NULL
			ORDER BY cOrder ASC
		END

	IF @type = 9
		BEGIN
			SELECT	cId,
					cOrder,
					cParent,
					cPrdNo,
					cName,
					cDescription,
					cKeyWords,
					cPicture,
					cDspTpeId,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceProduct
			WHERE	cId = @id
			ORDER BY cOrder ASC
		END
		
		IF @type = 10
		BEGIN
		
			DECLARE @temp TABLE
			(
				id int
			)

			INSERT INTO @temp (id)
			(
				SELECT	DISTINCT p.cId
						
				FROM	tblCommerceProduct p
							inner join tblCommerceProductAttribute pa
						ON p.cId = pa.cprdId
				WHERE
					(	
						p.cName LIKE '%' + @name + '%'
						OR 
						p.cprdNo = @name
						OR
						p.cDescription LIKE '%' + @name + '%'
						OR
						p.ckeyWords LIKE '%' + @name + '%'
						OR
						pa.cValue LIKE '%' + @name + '%'
					)
					AND
					( 
						p.cId IN (SELECT cPrdId from tblCommerceProductProductCategory)
						OR
						p.cParent IN (SELECT cPrdId from tblCommerceProductProductCategory)
					)
			)

			SELECT 
					cId,
					cOrder,
					cParent,
					cPrdNo,
					cName,
					cDescription,
					cKeyWords,
					cPicture,
					cDspTpeId,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceProduct, @temp 
			WHERE   cId = id
			ORDER BY cOrder ASC

		END
		IF @type = 11
		BEGIN
			
				SELECT	tblCommerceProduct.cId,
						tblCommerceProduct.cOrder,
						tblCommerceProduct.cParent,
						tblCommerceProduct.cPrdNo,
						tblCommerceProduct.cName,
						tblCommerceProduct.cDescription,
						tblCommerceProduct.cKeyWords,
						tblCommerceProduct.cPicture,
						tblCommerceProduct.cDspTpeId,
						tblCommerceProduct.cActive,
						tblCommerceProduct.cApprovedBy,
						tblCommerceProduct.cApprovedDate,
						tblCommerceProduct.cLockedBy,
						tblCommerceProduct.cLockedDate,
						tblCommerceProduct.cCreatedBy,
						tblCommerceProduct.cCreatedDate,
						tblCommerceProduct.cChangedBy,
						tblCommerceProduct.cChangedDate
				FROM	tblCommerceProduct
					INNER JOIN tblCommerceProductProductCategory
						ON tblCommerceProductProductCategory.cPrdId 
								= tblCommerceProduct.cId
				WHERE	tblCommerceProductProductCategory.cPrdCatId = @prdCatId
				AND tblCommerceProduct.cId NOT IN (
				SELECT tblCommerceProductProductCategory.cPrdId FROM tblCommerceProductCategory
				INNER JOIN tblCommerceProductProductCategory ON 
				tblCommerceProductProductCategory.cPrdCatId = tblCommerceProductCategory.cId
				WHERE cParent = @prdCatId)
				ORDER BY dbo.tblCommerceProductProductCategory.cOrder ASC
			
		END

		

	SET @status = @@ERROR
			
END
