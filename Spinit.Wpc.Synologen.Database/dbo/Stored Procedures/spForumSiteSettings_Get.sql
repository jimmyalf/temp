CREATE  PROCEDURE spForumSiteSettings_Get
(
	@Application	nvarchar(512)
)
AS

IF @Application = '*'
	SELECT
		* 
	FROM
		tblForumSiteSettings

ELSE IF NOT EXISTS (SELECT SiteID FROM tblForumSiteSettings WHERE Application = @Application)
	RETURN
ELSE
	SELECT
		* 
	FROM
		tblForumSiteSettings
	WHERE
		Application = @Application



