


CREATE PROCEDURE spCampaignGetCampaignsDynamic
					@LocationId INT,
					@LanguageId INT,
					@CategoryId INT,
					@SearchString NVARCHAR(255),
					@OrderBy NVARCHAR (255)
					
AS
BEGIN
	IF @CategoryId <> 0
	BEGIN
		IF @SearchString IS NOT NULL
		BEGIN
				SELECT	g.*
				FROM tblCampaignCategoryConnection conn
				INNER JOIN fnGetAllCampaigns(1, @Locationid, @Languageid) g
				ON conn.cCampaignId = g.cId
				WHERE cCategoryId = @CategoryId 
				AND cName LIKE @SearchString + '%'
				ORDER BY cName ASC
		END
		ELSE
		BEGIN
				SELECT	g.*
				FROM tblCampaignCategoryConnection conn
				INNER JOIN fnGetAllCampaigns(1, @Locationid, @Languageid) g
				ON conn.cCampaignId = g.cId
				WHERE cCategoryId = @CategoryId 
				ORDER BY cName ASC

		END
	END
	ELSE
	BEGIN
		IF @SearchString IS NOT NULL
		BEGIN
				SELECT	g.*
				FROM tblCampaignCategoryConnection conn
				INNER JOIN fnGetAllCampaigns(1, @Locationid, @Languageid) g
				ON conn.cCampaignId = g.cId
				WHERE cName LIKE @SearchString + '%'
				ORDER BY cName ASC
		END
		ELSE
		BEGIN
				SELECT	g.*
				FROM tblCampaignCategoryConnection conn
				INNER JOIN fnGetAllCampaigns(1, @Locationid, @Languageid) g
				ON conn.cCampaignId = g.cId
				ORDER BY cName ASC

		END	

	END
END
