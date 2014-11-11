create      procedure spForumsystem_UpdateUserPostRank
AS
BEGIN
DECLARE @Usercount int
DECLARE @LoopCounter int

	SET NOCOUNT ON

	CREATE Table #PostRank (
	  Rank int IDENTITY (1, 1) NOT NULL,
	  UserID int
	)

	-- Select into temp table
	INSERT INTO #PostRank (UserID)
	SELECT TOP 500
		P.UserID
	FROM
		tblForumUserProfile P
	ORDER BY
		P.TotalPosts DESC

	-- How many users did we select?
	SELECT @Usercount = count(*) FROM #PostRank


	-- First clear all the users
	UPDATE 
		tblForumUserProfile
	SET 
		PostRank = 0x0
	WHERE 
		PostRank > 0

	-- Top 10
	UPDATE
		tblForumUserProfile
	SET
		PostRank = 0x01
	WHERE
		UserID in (SELECT UserID FROM #PostRank WHERE Rank < 11)

	-- Top 25
	UPDATE
		tblForumUserProfile
	SET
		PostRank = 0x02
	WHERE
		UserID in (SELECT UserID FROM #PostRank WHERE Rank > 10 AND Rank < 26)


	-- Top 50
	UPDATE
		tblForumUserProfile
	SET
		PostRank = 0x04
	WHERE
		UserID in (SELECT UserID FROM #PostRank WHERE Rank > 25 AND Rank < 51)


	-- Top 75
	UPDATE
		tblForumUserProfile
	SET
		PostRank = 0x08
	WHERE
		UserID in (SELECT UserID FROM #PostRank WHERE Rank > 50 AND Rank < 76)

	-- Top 100
	UPDATE
		tblForumUserProfile
	SET
		PostRank = 0x10
	WHERE
		UserID in (SELECT UserID FROM #PostRank WHERE Rank > 75 AND Rank < 101)

	-- Top 150
	UPDATE
		tblForumUserProfile
	SET
		PostRank = 0x20
	WHERE
		UserID in (SELECT UserID FROM #PostRank WHERE Rank > 100 AND Rank < 151)

	-- Top 200
	UPDATE
		tblForumUserProfile
	SET
		PostRank = 0x40
	WHERE
		UserID in (SELECT UserID FROM #PostRank WHERE Rank > 150 AND Rank < 200)


	-- Top 500
	UPDATE
		tblForumUserProfile
	SET
		PostRank = 0x80
	WHERE
		UserID in (SELECT UserID FROM #PostRank WHERE Rank > 200)

END



