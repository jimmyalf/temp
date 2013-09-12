create FUNCTION sfCommerceGetProductFiles
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
					tblCommerceProduct.cId AS ObjId,
					--tblContPage.cName AS ObjName,
					dbo.[sfBaseTruncateString](tblCommerceProduct.cName,50) As ObjName, 
					NULL AS ObjDescription,
					'Commerce' AS Component
			FROM	tblBaseFile
				INNER JOIN tblCommerceProduct
					ON tblCommerceProduct.cPicture = tblBaseFile.cId
		UNION
			SELECT	tblBaseFile.cId AS FleId,
					tblCommerceProduct.cId AS ObjId,
					--tblContPage.cName AS ObjName,
					dbo.[sfBaseTruncateString](tblCommerceProduct.cName,50) As ObjName, 
					NULL AS ObjDescription,
					'Commerce' AS Component
			FROM	tblBaseFile
				INNER JOIN	tblCommerceProductFile
					ON tblCommerceProductFile.cFleId = tblBaseFile.cId
				INNER JOIN tblCommerceProduct
					ON tblCommerceProduct.cId = tblCommerceProductFile.cPrdId
		UNION
			SELECT	tblBaseFile.cId AS FleId,
					tblCommerceProduct.cId AS ObjId,
					--tblContPage.cName AS ObjName,
					dbo.[sfBaseTruncateString](tblCommerceProduct.cName,50) As ObjName, 
					NULL AS ObjDescription,
					'Commerce' AS Component
			FROM	tblBaseFile
				INNER JOIN	tblCommerceProductFileConnection
					ON tblCommerceProductFileConnection.cFleId = tblBaseFile.cId
				INNER JOIN tblCommerceProduct
					ON tblCommerceProduct.cId = tblCommerceProductFileConnection.cPrdId
					
	RETURN
END
