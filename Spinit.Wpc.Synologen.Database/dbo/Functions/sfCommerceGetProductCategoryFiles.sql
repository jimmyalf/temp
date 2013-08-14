create FUNCTION sfCommerceGetProductCategoryFiles
( )
RETURNS 
    @retTbl TABLE (
        [FleId]          INT             NULL,
        [ObjId]          INT             NULL,
        [ObjName]        NVARCHAR (50)   NULL,
        [ObjDescription] NVARCHAR (4000) NULL,
        [Component]      NVARCHAR (50)   NULL)
AS
BEGIN
	INSERT INTO @retTbl
			SELECT	tblBaseFile.cId AS FleId,
					tblCommerceProductCategory.cId AS ObjId,
					--tblContPage.cName AS ObjName,
					dbo.[sfBaseTruncateString](tblCommerceProductCategory.cName,50) As ObjName, 
					NULL AS ObjDescription,
					'Commerce' AS Component
			FROM	tblBaseFile
				INNER JOIN tblCommerceProductCategory
					ON tblCommerceProductCategory.cPicture = tblBaseFile.cId
		UNION
			SELECT	tblBaseFile.cId AS FleId,
					tblCommerceProductCategory.cId AS ObjId,
					--tblContPage.cName AS ObjName,
					dbo.[sfBaseTruncateString](tblCommerceProductCategory.cName,50) As ObjName, 
					NULL AS ObjDescription,
					'Commerce' AS Component
			FROM	tblBaseFile
				INNER JOIN	tblCommerceProductCategoryFile
					ON tblCommerceProductCategoryFile.cFleId = tblBaseFile.cId
				INNER JOIN tblCommerceProductCategory
					ON tblCommerceProductCategory.cId = tblCommerceProductCategoryFile.cPrdCatId
					
	RETURN
END
