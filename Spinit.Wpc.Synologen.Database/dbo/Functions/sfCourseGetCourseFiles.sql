create FUNCTION sfCourseGetCourseFiles ( )
	RETURNS @retTbl TABLE (FleId INT,
						   ObjId INT,
						   ObjName NVARCHAR (50),
						   ObjDescription NVARCHAR (4000),
						   Component NVARCHAR (50))
AS
BEGIN
	INSERT INTO @retTbl
			SELECT	tblBaseFile.cId AS FleId,
					tblCourse.cId AS ObjId,
					--tblCourse.cHeading AS ObjName,
					dbo.[sfBaseTruncateString](SUBSTRING(tblCourse.cHeading,0,254),50) AS ObjName,
					NULL AS ObjDescription,
					'Courses' AS Component
			FROM	tblBaseFile
				INNER JOIN	tblCourseFileConnection
					ON tblBaseFile.cId = tblCourseFileConnection.cFileId
				INNER JOIN	tblCourse
					ON tblCourseFileConnection.cCourseId = tblCourse.cId					
	RETURN
END
