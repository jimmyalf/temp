CREATE   procedure spForumSearch_IgnoreWords
AS
BEGIN
		SELECT
			WordHash,
			Word
		FROM
			tblForumSearchIgnoreWords
END


