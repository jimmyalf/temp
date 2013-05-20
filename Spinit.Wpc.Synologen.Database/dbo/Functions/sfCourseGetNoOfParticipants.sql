create FUNCTION [sfCourseGetNoOfParticipants] (@courseId INT) RETURNS INT
AS
BEGIN 
	DECLARE 
	@participants INT, 
	@tempInt INT 

	DECLARE getAllPart CURSOR LOCAL FOR 
			SELECT	cNrOfParticipants 
			FROM	tblCourseApplication 
			WHERE tblCourseApplication.cCourseId = @courseId 
	OPEN getAllPart 
	SET @participants = 0 
	FETCH NEXT FROM getAllPart INTO @tempInt 
	WHILE (@@FETCH_STATUS <> -1) 
	BEGIN 
		SET @participants = @participants + @tempInt 
		FETCH NEXT FROM getAllPart INTO @tempInt 
	END 
	CLOSE getAllPart 
	DEALLOCATE getAllPart 
	RETURN (@participants ) 
END
