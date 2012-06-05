CREATE PROCEDURE spSynologenGetOrderStatuses
					@orderStatusId INT,
					@status INT OUTPUT
					
AS BEGIN
	IF (@orderStatusId > 0) BEGIN
		SELECT	tblSynologenOrderStatus.*
		FROM tblSynologenOrderStatus
		WHERE tblSynologenOrderStatus.cId = @orderStatusId
		ORDER BY  cOrder ASC
	END
	ELSE BEGIN
		SELECT	tblSynologenOrderStatus.*
		FROM tblSynologenOrderStatus
		ORDER BY  cOrder ASC		
	END

	SELECT @status = @@ERROR
END
