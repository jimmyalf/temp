create FUNCTION sfCourseGetCoursePages ( )
	RETURNS @retTbl TABLE (LnkId INT,
						   ObjId INT,
						   ObjName NVARCHAR (50),
						   ObjDescription NVARCHAR (4000),
						   Component NVARCHAR (50),
						   ObjectType NVARCHAR (50),
						   LocId INT,
						   LngId INT)
AS
BEGIN
	INSERT INTO @retTbl
			SELECT	tblContTree.cId AS LnkId,
					tblCourse.cId AS ObjId,
					--tblCourse.cHeading AS ObjName,
					dbo.[sfBaseTruncateString](SUBSTRING(tblCourse.cHeading,0,254),50) As ObjName,					
					NULL AS ObjDescription,
					'Courses' AS Component,
					'Courses' AS ObjectType,
					tblCourseLocationConnection.cLocationId AS LocId,
					tblCourseLanguageConnection.cLanguageId AS LngId
			FROM tblContTree	
				INNER JOIN tblContPage
					ON tblContTree.cPgeId = tblContPage.cId
				INNER JOIN	tblCoursePageConnection
					ON tblContPage.cId = tblCoursePageConnection.cPageId
				INNER JOIN	tblCourse
					ON tblCoursePageConnection.cCourseId = tblCourse.cId
				LEFT OUTER JOIN tblCourseLocationConnection
					ON tblCourseLocationConnection.cCourseId = tblCourse.cId			
				LEFT OUTER JOIN tblCourseLanguageConnection
					ON tblCourseLanguageConnection.cCourseId = tblCourse.cId			
	RETURN
END
