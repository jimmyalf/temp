CREATE PROCEDURE spMemberGetMemberDynamic
					@type INT,
					@LocationId INT,
					@LanguageId INT,
					@CategoryId INT,
					@SearchString NVARCHAR(255),
					@OrderBy NVARCHAR (255)
					
AS
BEGIN
	DECLARE @sql nvarchar(4000),@paramlist	nvarchar(4000)
	IF (@type = 0)
	BEGIN
			SELECT @sql=
			'SELECT 
				member.cId,
				member.cMemberId,
				member.cDescription,
				member.cAddress,
				member.cZipcode,
				member.cCity,
				member.cPhone,
				member.cFax,
				member.cMobile,
				users.cEmail,
				member.cWww,
				member.cVoip,
				member.cSkype,
				member.cCordless,
				member.cBody ,
				member.cOther1,
				member.cOther2,
				member.cOther3,
				users.cFirstName,
				users.cLastName,
				member.cProfilePictureId,
				member.cDefaultDirectoryId,
				member.cOrgName,
				users.cActive,
				member.cLanguageId,
				member.cCreatedBy,
				member.cCreatedDate,
				member.cEditedBy,
				member.cEditedDate,
				member.cApprovedBy,
				member.cApprovedDate,
				member.cLockedBy,
				member.cLockedDate
				FROM sfMemberGetAllMembers(0, @xLocationId, @xLanguageId) member
				INNER JOIN tblMemberUserConnection ON member.cMemberId = tblMemberUserConnection.cMemberId 
				INNER JOIN tblBaseUsers users ON tblMemberUserConnection.cUserId = users.cId'

				IF (@CategoryId IS NOT NULL AND @CategoryId > 0)
				BEGIN
					SELECT @sql = @sql + 
					' INNER JOIN tblMemberCategoryConnection ON member.cMemberId = tblMemberCategoryConnection.cMemberId
					WHERE tblMemberCategoryConnection.cCategoryId = @xCategoryId'
				END
				ELSE
				BEGIN					
					SELECT @sql = @sql + ' WHERE 1=1'
				END
				
				IF (@SearchString IS NOT NULL AND LEN(@SearchString) > 0)
				BEGIN
					SELECT @sql = @sql + ' AND (users.cFirstName LIKE ''%''+@xSearchString+
					''%''OR users.cLastName LIKE ''%''+@xSearchString+''%''+
					''%''OR users.cEmail LIKE ''%''+@xSearchString+''%''+
					''%''OR member.cOrgName LIKE ''%''+@xSearchString+''%'')'
				END
				IF (@OrderBy IS NOT NULL)
				BEGIN
					IF (SUBSTRING(@OrderBy,1,9) = 'cMemberId')
					BEGIN
						SET @OrderBy = 'member.' + SUBSTRING(@OrderBy,1,LEN(@OrderBy))
					END
					IF (SUBSTRING(@OrderBy,1,7) = 'cActive')
					BEGIN
						SET @OrderBy = 'users.' + SUBSTRING(@OrderBy,1,LEN(@OrderBy))
					END
					SELECT @sql = @sql + ' ORDER BY ' + @OrderBy
					
				END
				SELECT @paramlist = '@xSearchString NVARCHAR(255),@xOrderBy NVARCHAR(255),@xLocationId INT, @xLanguageId INT, @xCategoryId INT'
				EXEC sp_executesql @sql,
									@paramlist,                                 
									@SearchString,
									@OrderBy,
									@LocationId,
									@LanguageId,
									@CategoryId
				

	END
	IF (@type = 1)
	BEGIN
			SELECT @sql=
			'SELECT 
				member.cId,
				member.cMemberId,
				member.cDescription,
				member.cAddress,
				member.cZipcode,
				member.cCity,
				member.cPhone,
				member.cFax,
				member.cMobile,
				member.cEmail,
				member.cWww,
				member.cVoip,
				member.cSkype,
				member.cCordless,
				member.cBody ,
				member.cOther1,
				member.cOther2,
				member.cOther3,
				member.cContactFirst AS cFirstName,
				member.cContactLast AS cLastName,
				member.cProfilePictureId,
				member.cDefaultDirectoryId,
				member.cOrgName,
				member.cActive,
				member.cLanguageId,
				member.cCreatedBy,
				member.cCreatedDate,
				member.cEditedBy,
				member.cEditedDate,
				member.cApprovedBy,
				member.cApprovedDate,
				member.cLockedBy,
				member.cLockedDate
				FROM sfMemberGetAllMembers(0, @xLocationId, @xLanguageId) member'

				IF (@CategoryId IS NOT NULL AND @CategoryId > 0)
				BEGIN
					SELECT @sql = @sql + 
					' INNER JOIN tblMemberCategoryConnection ON member.cMemberId = tblMemberCategoryConnection.cMemberId
					WHERE tblMemberCategoryConnection.cCategoryId = @xCategoryId'
				END
				ELSE
				BEGIN					
					SELECT @sql = @sql + ' WHERE 1=1'
				END
				
				IF (@SearchString IS NOT NULL AND LEN(@SearchString) > 0)
				BEGIN
					SELECT @sql = @sql + ' AND (member.cContactFirst LIKE ''%''+@xSearchString+
					''%''OR member.cContactLast LIKE ''%''+@xSearchString+''%''+
					''%''OR member.cEmail LIKE ''%''+@xSearchString+''%''+
					''%''OR member.cOrgName LIKE ''%''+@xSearchString+''%'')'
				END
				IF (@OrderBy IS NOT NULL)
				BEGIN
					SELECT @sql = @sql + ' ORDER BY ' + @OrderBy
					
				END
				SELECT @paramlist = '@xSearchString NVARCHAR(255),@xOrderBy NVARCHAR(255),@xLocationId INT, @xLanguageId INT, @xCategoryId INT'
				EXEC sp_executesql @sql,
									@paramlist,                                 
									@SearchString,
									@OrderBy,
									@LocationId,
									@LanguageId,
									@CategoryId

	END
END
