CREATE procedure spForumSiteSettings_Save
(
	@Application 		nvarchar(512),
	@ForumsDisabled		smallint,
	@Settings 		varbinary(8000)
)
AS
BEGIN

	IF EXISTS (SELECT SiteID FROM tblForumSiteSettings WHERE Application = @Application)
		UPDATE
			tblForumSiteSettings
		SET
			ForumsDisabled = @ForumsDisabled,
			Settings = @Settings
		WHERE
			Application = @Application
	ELSE
		INSERT INTO
			tblForumSiteSettings
			(
				Application,
				ForumsDisabled,
				Settings
			)
		VALUES
			(
				@Application,
				@ForumsDisabled,
				@Settings
			)
	
END


