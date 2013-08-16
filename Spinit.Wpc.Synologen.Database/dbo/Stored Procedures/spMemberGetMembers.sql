/*
	Type 0 - Specific Member
	Type 1 - All Members TODO Must Update sfMemberGetAllMembers to support location = 0 
	Type 2 - All Members for a specific location and language ordered by cOrgName
	Type 3 - All Members for a specific location and language ordered by cContactLast
	Type 4 - All Active Members for a specific location and language ordered by cOrgName
	Type 5 - All Active Members for a specific location and language ordered by cContactLast
	Type 6 - All Active Members for a specific location and language ordered by cOrgName, returns only correct language
	Type 7 - All Active Members for a specific location and language ordered by cContactLast, returns only correct language
	Type 8 - All Active Members for a specific location,language and category ordered by cContactLast, returns only correct language
	Type 9 - All Active Members for a specific location,language and category ordered by cOrgName, returns only correct language
	
	/// Summary
	If the language requested dosen't exist for a member the function should return the 
	default language post for the member if possible.
	
*/


CREATE PROCEDURE spMemberGetMembers
					@type INT,
					@memberId INT,
					@locationid INT,
					@languageid INT,
					@categoryid INT,
					@status INT OUTPUT
	AS
		BEGIN
			DECLARE @defaultLanguageId INT
			IF (@locationid > 0)
			BEGIN
				SELECT @defaultLanguageId = cLanguageId
				FROM tblBaseLocationsLanguages
				WHERE cIsDefault = 1 AND cLocationId = @locationid
			END
			
			IF (@type = 0)
				BEGIN
					SELECT	*
					FROM	sfMemberGetAllMembers(0, @locationid, @languageid)
					WHERE cMemberId = @memberId
				END


			IF (@type = 2)
				BEGIN
					SELECT	*
					FROM	sfMemberGetAllMembers(0, @locationid, @languageid)
					ORDER BY cOrgName ASC
				END			
				IF (@type = 3)
				BEGIN
					SELECT	*
					FROM	sfMemberGetAllMembers(0, @locationid, @languageid) 
					ORDER BY cContactLast ASC	
				END	
			IF (@type = 4)
				BEGIN
					SELECT	*
					FROM sfMemberGetAllMembers(0, @locationid, @languageid) 					
					WHERE cActive = 1
					ORDER BY cOrgName ASC	
				END
			IF (@type = 5)
				BEGIN
					SELECT	*
					FROM sfMemberGetAllMembers(0, @locationid, @languageid) 					
					WHERE cActive = 1
					ORDER BY cContactLast ASC	
				END					
			IF (@type = 6)
				BEGIN
					SELECT	*,tblBaseUsers.cLastName,tblBaseUsers.cFirstName,tblBaseUsers.cEmail
					FROM sfMemberGetAllMembers(1, @locationid, @languageid) mem					
					INNER JOIN tblMembers
					ON mem.cMemberId=tblMembers.cId
					INNER JOIN tblMemberUserConnection 
					ON tblMemberUserConnection.cMemberId=tblMembers.cId
					INNER JOIN tblBaseUsers 
					ON tblMemberUserConnection.cUserId=tblBaseUsers.cId
					WHERE mem.cActive = 1
					ORDER BY tblBaseUsers.cLastName, mem.cOrgName ASC	
				END
			IF (@type = 7)
				BEGIN
					SELECT	*
					FROM sfMemberGetAllMembers(1, @locationid, @languageid) 					
					WHERE cActive = 1
					ORDER BY cContactLast ASC	
				END					
			IF (@type = 8)
				BEGIN
					SELECT	*,tblBaseUsers.cId AS UserId
					FROM tblMemberCategoryConnection conn
					INNER JOIN sfMemberGetAllMembers(1, @locationid, @languageid) member
					ON conn.cMemberId = member.cMemberId
					INNER JOIN tblMembers
					ON member.cMemberId=tblMembers.cId
					INNER JOIN tblMemberUserConnection 
					ON tblMemberUserConnection.cMemberId=tblMembers.cId
					INNER JOIN tblBaseUsers 
					ON tblMemberUserConnection.cUserId=tblBaseUsers.cId
					WHERE member.cActive = 1 AND cCategoryId = @categoryid 
					ORDER BY cLastName,cFirstName ASC	
				END
			IF (@type = 9)
				BEGIN
					SELECT	*
					FROM tblMemberCategoryConnection conn
					INNER JOIN sfMemberGetAllMembers(1, @locationid, @languageid) member
					ON conn.cMemberId = member.cMemberId
					WHERE cActive = 1 AND cCategoryId = @categoryid 
					ORDER BY cContactLast ASC	
				END	
			IF (@type = 10)
				BEGIN
					SELECT	*
					FROM tblMemberCategoryConnection conn
					INNER JOIN sfMemberGetAllMembers(1, @locationid, @languageid) member
					ON conn.cMemberId = member.cMemberId
					WHERE cActive = 1 AND cCategoryId = @categoryid 
					ORDER BY cCity ASC	
				END	
			IF (@type = 11)
				BEGIN
					SELECT	*,tblBaseUsers.cId AS UserId, tblBaseUsers.cLastName,tblBaseUsers.cFirstName,tblBaseUsers.cEmail
					FROM sfMemberGetAllMembers(1, @locationid, @languageid) mem					
					INNER JOIN tblMembers
					ON mem.cMemberId=tblMembers.cId
					INNER JOIN tblMemberUserConnection 
					ON tblMemberUserConnection.cMemberId=tblMembers.cId
					INNER JOIN tblBaseUsers 
					ON tblMemberUserConnection.cUserId=tblBaseUsers.cId
					WHERE mem.cActive = 1
					ORDER BY cEditedDate DESC	
				END					
			
			SELECT @status = @@ERROR
		END
