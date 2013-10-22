create PROCEDURE spSynologenGetMembers
					@memberId INT,
					@categoryId INT,
					@shopId INT,
					@orderBy NVARCHAR (255)
					
AS
BEGIN
	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	SELECT @sql=
		'SELECT 
		member.cId,
		memberContent.cDescription,
		memberContent.cAddress,
		memberContent.cZipcode,
		memberContent.cCity,
		memberContent.cPhone,
		memberContent.cFax,
		memberContent.cMobile,
		users.cEmail,
		memberContent.cWww,
		memberContent.cVoip,
		memberContent.cSkype,
		memberContent.cCordless,
		memberContent.cBody ,
		memberContent.cOther1,
		memberContent.cOther2,
		memberContent.cOther3,
		memberContent.cContactFirst,
		memberContent.cContactLast,
		users.cFirstName,
		users.cLastName,
		memberContent.cProfilePictureId,
		memberContent.cDefaultDirectoryId,
		member.cOrgName,
		users.cActive,
		memberContent.cCreatedBy,
		memberContent.cCreatedDate,
		memberContent.cEditedBy,
		memberContent.cEditedDate,
		memberContent.cApprovedBy,
		memberContent.cApprovedDate,
		memberContent.cLockedBy,
		memberContent.cLockedDate,
		(SELECT users.cFirstName + '' '' + users.cLastName) AS cFullUserName 
		FROM tblMembers member
		INNER JOIN tblMembersContent memberContent ON member.cId = memberContent.cMemberId
		INNER JOIN tblMemberUserConnection ON member.cId = tblMemberUserConnection.cMemberId 
		INNER JOIN tblBaseUsers users ON tblMemberUserConnection.cUserId = users.cId'


		IF (@shopId IS NOT NULL AND @shopId > 0)
		BEGIN
			SELECT @sql = @sql + 
			' INNER JOIN tblSynologenShopMemberConnection ON member.cId = tblSynologenShopMemberConnection.cMemberId
			AND tblSynologenShopMemberConnection.cSynologenShopId = @xShopId'
		END
		IF (@categoryId IS NOT NULL AND @categoryId > 0)
		BEGIN
			SELECT @sql = @sql + 
			' INNER JOIN tblMemberCategoryConnection ON member.cId = tblMemberCategoryConnection.cMemberId
			AND tblMemberCategoryConnection.cCategoryId = @xCategoryId'
		END
			
		SELECT @sql = @sql + ' WHERE 1=1'

		IF (@memberId IS NOT NULL AND @memberId > 0) BEGIN
			SELECT @sql = @sql + ' AND member.cId = @xMemberId'
		END
		
		IF (@orderBy IS NOT NULL)
		BEGIN
			IF (SUBSTRING(@orderBy,1,9) = 'cId')
			BEGIN
				SET @orderBy = 'member.' + SUBSTRING(@orderBy,1,LEN(@orderBy))
			END
			IF (SUBSTRING(@orderBy,1,7) = 'cActive')
			BEGIN
				SET @orderBy = 'users.' + SUBSTRING(@orderBy,1,LEN(@orderBy))
			END
			SELECT @sql = @sql + ' ORDER BY ' + @orderBy
			
		END
		SELECT @paramlist = '@xMemberId INT,@xCategoryId INT, @xShopId INT'
		EXEC sp_executesql @sql,
							@paramlist,
							@memberId,
							@categoryId,
							@shopId

END
