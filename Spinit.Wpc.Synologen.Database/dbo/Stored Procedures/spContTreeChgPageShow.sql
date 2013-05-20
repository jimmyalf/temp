CREATE PROCEDURE spContTreeChgPageShow
					@id INT,
					@changedBy NVARCHAR (100),
					@hideInMenu BIT,
					@status INT OUTPUT
	AS
		UPDATE	tblContTree
		SET		cChangedBy = @changedBy,
				cChangedDate = GETDATE (),
--				cPublishedDate = NULL,
--				cUnPublishedDate = NULL,
				cHideInMenu = @hideInMenu
		WHERE	cId = @id
		
		SELECT @status = @@ERROR
