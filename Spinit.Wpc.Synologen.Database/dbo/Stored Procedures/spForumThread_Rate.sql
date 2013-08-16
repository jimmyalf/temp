create procedure spForumThread_Rate
(
	@ThreadID int,
	@UserID int,
	@Rating int
)
AS
BEGIN

	IF EXISTS (SELECT ThreadID FROM tblForumPostRating WHERE UserID = @UserID AND ThreadID = @ThreadID)
	BEGIN
		-- Get the User's Current Rating
		DECLARE @CurrentRating int
		SET @CurrentRating = (SELECT Rating FROM tblForumPostRating WHERE UserID = @UserID AND ThreadID = @ThreadID)

		IF @CurrentRating = @Rating
			RETURN

		-- Update the rating
		UPDATE
			tblForumPostRating
		SET 
			Rating = @Rating
		WHERE
			UserID = @UserID AND
			ThreadID = @ThreadID

		-- Update the thread rating
		UPDATE
			tblForumThreads
		SET
			RatingSum = (RatingSum - @CurrentRating) + @Rating
		WHERE
			ThreadID = @ThreadID

	END
	ELSE
	BEGIN
		-- Add an entry into the post rating table
		INSERT INTO
			tblForumPostRating
		VALUES
			(@UserID, @ThreadID, @Rating)

		-- Update the thread rating
		UPDATE
			tblForumThreads
		SET
			RatingSum = RatingSum + @Rating,
			TotalRatings = TotalRatings + 1
		WHERE
			ThreadID = @ThreadID
	END

END


