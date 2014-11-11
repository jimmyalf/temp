create FUNCTION sfCommerceGetProductPages
( )
RETURNS 
    @retTbl TABLE (
        [LnkId]          INT             NULL,
        [ObjId]          INT             NULL,
        [ObjName]        NVARCHAR (50)   NULL,
        [ObjDescription] NVARCHAR (4000) NULL,
        [Component]      NVARCHAR (50)   NULL,
        [ObjectType]     NVARCHAR (50)   NULL,
        [LocId]          INT             NULL,
        [LngId]          INT             NULL)
AS
BEGIN
	INSERT INTO @retTbl
			SELECT	tblContTree.cId AS LnkId,
					tblCommerceProduct.cId AS ObjId,
					--tblNews.cHeading AS ObjName,
					dbo.[sfBaseTruncateString](tblCommerceProduct.cName,50) As ObjName,
					NULL AS ObjDescription,
					'Commerce' AS Component,
					'Commerce Products' AS ObjectType,
					tblCommerceLocationConnection.cLocId AS LocId,
					tblCommerceLanguageConnection.cLngId AS LngId
			FROM tblContTree	
				INNER JOIN tblContPage
					ON tblContTree.cPgeId = tblContPage.cId
				INNER JOIN	tblCommerceProductPageConnection
					ON tblContPage.cId = tblCommerceProductPageConnection.cPgeId
				INNER JOIN	tblCommerceProduct
					ON tblCommerceProductPageConnection.cPrdId = tblCommerceProduct.cId
				LEFT OUTER JOIN tblCommerceProductProductCategory
					ON tblCommerceProductProductCategory.cPrdId = tblCommerceProduct.cId
				LEFT OUTER JOIN tblCommerceLocationConnection
					ON tblCommerceLocationConnection.cPrdCatId = tblCommerceProductProductCategory.cPrdCatId			
				LEFT OUTER JOIN tblCommerceLanguageConnection
					ON tblCommerceLanguageConnection.cPrdCatId = tblCommerceProductProductCategory.cPrdCatId			
					
	RETURN
END
