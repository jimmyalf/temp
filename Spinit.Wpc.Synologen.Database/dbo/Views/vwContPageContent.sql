create VIEW vwContPageContent
	AS
		SELECT	cId,
				cPgeTpeId,
				cName,
				cSize,
				cCreatedBy,
				cCreatedDate,
				cChangedBy,
				cChangedDate
		FROM	tblContPage
