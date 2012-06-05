CREATE PROCEDURE [dbo].[spCommerceSearchAttribute]
					@type INT,
					@id INT,
					@prdCatId INT,
					@prdId INT,
					@name NVARCHAR (512),
					@status INT OUTPUT
AS
BEGIN
	IF @type = 0
		BEGIN
			SELECT	cId,
					cOrder,
					cName,
					cDescription,
					cDefaultValue,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceAttribute
			ORDER BY cOrder ASC
		END
		
	IF @type = 1
		BEGIN
			SELECT	tblCommerceAttribute.cId,
					tblCommerceAttribute.cOrder,
					tblCommerceAttribute.cName,
					tblCommerceAttribute.cDescription,
					tblCommerceAttribute.cDefaultValue,
					tblCommerceAttribute.cCreatedBy,
					tblCommerceAttribute.cCreatedDate,
					tblCommerceAttribute.cChangedBy,
					tblCommerceAttribute.cChangedDate
			FROM	tblCommerceAttribute
				INNER JOIN tblCommerceProductCategoryAttribute
					ON tblCommerceProductCategoryAttribute.cAttId 
						= tblCommerceAttribute.cId
			WHERE	tblCommerceProductCategoryAttribute.cPrdCatId = @prdCatId
			ORDER BY tblCommerceProductCategoryAttribute.cOrder ASC
		END
						
	IF @type = 2
		BEGIN
			SELECT	tblCommerceAttribute.cId,
					tblCommerceAttribute.cOrder,
					tblCommerceAttribute.cName,
					tblCommerceAttribute.cDescription,
					tblCommerceAttribute.cDefaultValue,
					tblCommerceAttribute.cCreatedBy,
					tblCommerceAttribute.cCreatedDate,
					tblCommerceAttribute.cChangedBy,
					tblCommerceAttribute.cChangedDate
			FROM	tblCommerceAttribute
				INNER JOIN tblCommerceProductAttribute
					ON tblCommerceProductAttribute.cAttId 
						= tblCommerceAttribute.cId
			WHERE	tblCommerceProductAttribute.cPrdId = @prdId
			ORDER BY tblCommerceProductAttribute.cOrder ASC
		END

	IF @type = 3
		BEGIN
			SELECT	cId,
					cOrder,
					cName,
					cDescription,
					cDefaultValue,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceAttribute
			WHERE	cId NOT IN (SELECT	cAttId
								FROM	tblCommerceProductCategoryAttribute
								WHERE	cPrdCatId = @prdCatId)
			ORDER BY cOrder ASC
		END
						
	IF @type = 4
		BEGIN
			SELECT	cId,
					cOrder,
					cName,
					cDescription,
					cDefaultValue,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceAttribute
			WHERE	cId NOT IN (SELECT	cAttId
								FROM	tblCommerceProductAttribute
								WHERE	cPrdId = @prdId)
			ORDER BY cOrder ASC
		END

	IF @type = 5
		BEGIN
			SELECT	cId,
					cOrder,
					cName,
					cDescription,
					cDefaultValue,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceAttribute
			WHERE	cName LIKE '%' + @name + '%'
			ORDER BY cOrder ASC
		END

	IF @type = 6
		BEGIN
			SELECT	cId,
					cOrder,
					cName,
					cDescription,
					cDefaultValue,
					cCreatedBy,
					cCreatedDate,
					cChangedBy,
					cChangedDate
			FROM	tblCommerceAttribute
			WHERE	cId = @id
			ORDER BY cOrder ASC
		END

	SET @status = @@ERROR
			
END
