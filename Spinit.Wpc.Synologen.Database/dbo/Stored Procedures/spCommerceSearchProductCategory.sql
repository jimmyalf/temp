CREATE PROCEDURE spCommerceSearchProductCategory
					@type INT,
					@id INT,
					@prdId INT,
					@parent INT,
					@attId INT,
					@locId INT,
					@lngId INT,
					@name NVARCHAR (512),
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cId,
					(SELECT TOP 1 cOrder FROM tblCommerceLocationConnection WHERE cLocId = @locId AND cPrdCatId = cId) AS cOrder,
					cParent,
					cName,
					cDescription,
					cPicture,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceProductCategory
			ORDER BY (SELECT TOP 1 cOrder FROM tblCommerceLocationConnection WHERE cLocId = @locId AND cPrdCatId = cId) ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	tblCommerceProductCategory.cId,
					(SELECT TOP 1 cOrder FROM tblCommerceLocationConnection WHERE cLocId = @locId AND cPrdCatId = cId) AS cOrder,
					tblCommerceProductCategory.cParent,
					tblCommerceProductCategory.cName,
					tblCommerceProductCategory.cDescription,
					tblCommerceProductCategory.cPicture,
					tblCommerceProductCategory.cActive,
					tblCommerceProductCategory.cApprovedBy,
					tblCommerceProductCategory.cApprovedDate,
					tblCommerceProductCategory.cLockedBy,
					tblCommerceProductCategory.cLockedDate,
					tblCommerceProductCategory.cCreatedBy,
					tblCommerceProductCategory.cCreatedDate,
					tblCommerceProductCategory.cChangedBy,
					tblCommerceProductCategory.cChangedDate
			FROM	tblCommerceProductCategory
				INNER JOIN tblCommerceProductProductCategory
					ON tblCommerceProductProductCategory.cPrdCatId 
							= tblCommerceProductCategory.cId
			WHERE	tblCommerceProductProductCategory.cPrdId = @prdId
			ORDER BY (SELECT TOP 1 cOrder FROM tblCommerceLocationConnection WHERE cLocId = @locId AND cPrdCatId = cId) ASC
		END
							
	IF @type = 2
		BEGIN
			SELECT	cId,
					(SELECT TOP 1 cOrder FROM tblCommerceLocationConnection WHERE cLocId = @locId AND cPrdCatId = cId) AS cOrder,
					cParent,
					cName,
					cDescription,
					cPicture,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceProductCategory
			WHERE	cParent = @parent
			ORDER BY (SELECT TOP 1 cOrder FROM tblCommerceLocationConnection WHERE cLocId = @locId AND cPrdCatId = cId) ASC
		END

	IF @type = 3
		BEGIN
			SELECT	tblCommerceProductCategory.cId,
					(SELECT TOP 1 cOrder FROM tblCommerceLocationConnection WHERE cLocId = @locId AND cPrdCatId = cId) AS cOrder,
					tblCommerceProductCategory.cParent,
					tblCommerceProductCategory.cName,
					tblCommerceProductCategory.cDescription,
					tblCommerceProductCategory.cPicture,
					tblCommerceProductCategory.cActive,
					tblCommerceProductCategory.cApprovedBy,
					tblCommerceProductCategory.cApprovedDate,
					tblCommerceProductCategory.cLockedBy,
					tblCommerceProductCategory.cLockedDate,
					tblCommerceProductCategory.cCreatedBy,
					tblCommerceProductCategory.cCreatedDate,
					tblCommerceProductCategory.cChangedBy,
					tblCommerceProductCategory.cChangedDate
			FROM	tblCommerceProductCategory
				INNER JOIN tblCommerceProductCategoryAttribute
					ON tblCommerceProductCategoryAttribute.cPrdCatId 
						= tblCommerceProductCategory.cId
			WHERE	cAttId = @attId
			ORDER BY (SELECT TOP 1 cOrder FROM tblCommerceLocationConnection WHERE cLocId = @locId AND cPrdCatId = cId) ASC
		END

	IF @type = 4
		BEGIN
			SELECT	cId,
					tblCommerceLocationConnection.cOrder,					
					cParent,
					cName,
					cDescription,
					cPicture,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceProductCategory
				INNER JOIN tblCommerceLocationConnection
					ON tblCommerceLocationConnection.cPrdCatId 
						= tblCommerceProductCategory.cId
				INNER JOIN tblCommerceLanguageConnection
					ON tblCommerceLanguageConnection.cPrdCatId
						= tblCommerceProductCategory.cId
			WHERE	cLngId = @lngId
				AND	cLocId = @locId
			ORDER BY tblCommerceLocationConnection.cOrder ASC
		END

	IF @type = 5
		BEGIN
			SELECT	cId,
					tblCommerceLocationConnection.cOrder,
					cParent,
					cName,
					cDescription,
					cPicture,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceProductCategory
				INNER JOIN tblCommerceLocationConnection
					ON tblCommerceLocationConnection.cPrdCatId 
						= tblCommerceProductCategory.cId
				INNER JOIN tblCommerceLanguageConnection
					ON tblCommerceLanguageConnection.cPrdCatId
						= tblCommerceProductCategory.cId
			WHERE	cName LIKE '%' + @name + '%'
				AND	cLngId = @lngId
				AND	cLocId = @locId
			ORDER BY tblCommerceLocationConnection.cOrder ASC
		END

	IF @type = 6
		BEGIN
			SELECT	cId,
					tblCommerceLocationConnection.cOrder,
					cParent,
					cName,
					cDescription,
					cPicture,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceProductCategory
				INNER JOIN tblCommerceLocationConnection
					ON tblCommerceLocationConnection.cPrdCatId 
						= tblCommerceProductCategory.cId
				INNER JOIN tblCommerceLanguageConnection
					ON tblCommerceLanguageConnection.cPrdCatId
						= tblCommerceProductCategory.cId
			WHERE	cParent IS NULL
				AND	cLngId = @lngId
				AND	cLocId = @locId
			ORDER BY tblCommerceLocationConnection.cOrder ASC
		END

	IF @type = 7
		BEGIN
			SELECT	cId,
					(SELECT TOP 1 cOrder FROM tblCommerceLocationConnection WHERE cLocId = @locId AND cPrdCatId = cId) AS cOrder,
					cParent,
					cName,
					cDescription,
					cPicture,
					cActive,
					cApprovedBy,
					cApprovedDate,
					cLockedBy,
					cLockedDate,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceProductCategory
			WHERE	cId = @id
			ORDER BY (SELECT TOP 1 cOrder FROM tblCommerceLocationConnection WHERE cLocId = @locId AND cPrdCatId = cId) ASC
		END

	SET @status = @@ERROR
			
END
