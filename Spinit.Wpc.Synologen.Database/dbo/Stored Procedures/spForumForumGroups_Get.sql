CREATE  PROCEDURE spForumForumGroups_Get
(
	@SiteID int = 0
)
AS
BEGIN

SELECT 
	*
FROM
	tblForumForumGroups
WHERE
	(SiteID = @SiteID OR SiteID = 0)
			
END


