CREATE PROCEDURE spMemberGetConnectedCategories
					@memberid INT,
					@locationid INT,
					@languageid INT,
					@status INT OUTPUT
	AS
		BEGIN
			SELECT * FROM sfMemberGetMembersCategories(@memberid, 
			@locationid,
			@languageid)
			SELECT @status = @@ERROR
		END
