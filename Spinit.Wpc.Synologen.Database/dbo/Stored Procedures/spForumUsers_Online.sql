CREATE  PROCEDURE spForumUsers_Online
(
	@PastMinutes int
)
AS
BEGIN
	DELETE
		tblForumUsersOnline
	WHERE
		LastActivity < DateAdd(hour, -1, GetDate())
	
	SELECT
		U.UserID,
		U.UserName,
		U.PasswordFormat,
		U.PasswordQuestion,
		U.Email,
		U.DateCreated,
		U.LastLogin,
		U.LastActivity,
		U.LastAction,
		U.UserAccountStatus,
		U.IsAnonymous,
		P.*	--,
--		IsModerator = (select count(*) from tblForumModerators where UserID = U.UserID)
	FROM
		tblForumUsersOnline O
		JOIN tblForumUsers U ON O.UserID = U.UserID
		JOIN tblForumUserProfile P ON U.UserID = P.UserID
	WHERE
		U.IsAnonymous = 0
		AND O.LastActivity > DateAdd(minute, -@PastMinutes, GetDate())
	
	SELECT
		UserID,
		LastActivity = LastLogin,
		LastAction
	FROM
		tblForumAnonymousUsers
END


