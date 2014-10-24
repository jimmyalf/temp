create PROCEDURE spBaseCleanLog
AS
BEGIN
	DELETE FROM tblBaseLog
	WHERE		cClearedBy IS NOT NULL
		AND		cClearedDate > DATEADD (DAY, -2, GETDATE ())
		
	DELETE FROM	tblBaseLog
	WHERE		cLgTpeId < 2
		AND		cCreatedDate > DATEADD (DAY, -5, GETDATE ())
	
	DELETE FROM	tblBaseLog
	WHERE		cCreatedDate > DATEADD (MONTH, -1, GETDATE ())
END
