CREATE  PROCEDURE spForumsystem_Import_Preperation
AS
BEGIN
	-- used to prep the database before importing.

	
	BEGIN TRANSACTION
	/*
	DELETE FROM nntp_Posts WHERE ForumID = 10
	DELETE FROM tblForumSearchBarrel WHERE ForumID = 10
	DELETE FROM tblForumPosts WHERE ForumID = 10
	DELETE FROM tblForumThreadsRead WHERE ForumID = 10
	DELETE FROM tblForumThreads WHERE ForumID = 10
	DELETE FROM tblForumForumPermissions WHERE ForumID = 10
	DELETE FROM tblForumForumPingBack WHERE ForumID = 10
	DELETE FROM tblForumForumsRead WHERE ForumID = 10
	DELETE FROM tblForumPostAttachments WHERE ForumID = 10
	DELETE FROM tblForumTrackedForums WHERE ForumID = 10
	DELETE FROM nntp_NewsGroups WHERE ForumID = 10
	DELETE FROM tblForumModerationAudit WHERE ForumID = 10
	DELETE FROM tblForumForums WHERE ForumID = 10
	DELETE FROM tblForumForumGroups WHERE ForumGroupID = 2
	*/

	DELETE FROM nntp_Posts WHERE ForumID > 4
	DELETE FROM tblForumSearchBarrel WHERE ForumID > 4
	DELETE FROM tblForumPosts WHERE ForumID > 4
	DELETE FROM tblForumThreadsRead WHERE ForumID > 4
	DELETE FROM tblForumThreads WHERE ForumID > 4
	DELETE FROM tblForumForumPermissions WHERE ForumID > 4
	DELETE FROM tblForumForumPingBack WHERE ForumID > 4
	DELETE FROM tblForumForumsRead WHERE ForumID > 4
	DELETE FROM tblForumPostAttachments WHERE ForumID > 4
	DELETE FROM tblForumTrackedForums WHERE ForumID > 4
	DELETE FROM nntp_NewsGroups WHERE ForumID > 4
	DELETE FROM tblForumModerationAudit WHERE ForumID > 4
	DELETE FROM tblForumForums WHERE ForumID > 4
	DELETE FROM tblForumForumGroups WHERE ForumGroupID > 1

	COMMIT TRANSACTION
END


