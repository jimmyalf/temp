-- =============================================
-- Instead-of update document
-- =============================================

CREATE TRIGGER [SynologenOpqFiles_InsteadOfInsert] 
ON [dbo].[SynologenOpqFiles]
INSTEAD OF INSERT
AS
BEGIN
	SET NOCOUNT ON
	
	DECLARE	@ndeId INT,
			@order INT
			
	SELECT	@ndeId = NdeId
	FROM	INSERTED
	
	SET @order = 1
	
	SELECT	@order = MAX ([Order]) + 1
	FROM	dbo.SynologenOpqFiles
	WHERE	NdeId = @ndeId
			
	INSERT INTO dbo.SynologenOpqFiles (
		[Order], FleCatId, FleId, NdeId, ShpId, CncId, IsActive, CreatedById, CreatedByName, CreatedDate,
		ChangedById, ChangedByName, ChangedDate, ApprovedById, ApprovedByName, ApprovedDate,
		LockedById, LockedByName, LockedDate)
	SELECT
		@order,
		FleCatId,
		FleId,
		NdeId,
		ShpId,
		CncId,
		IsActive,
		CreatedById,
		CreatedByName,
		CreatedDate,
		ChangedById,
		ChangedByName,
		ChangedDate,
		ApprovedById,
		ApprovedByName,
		ApprovedDate,
		LockedById,
		LockedByName,
		LockedDate
	FROM INSERTED
END