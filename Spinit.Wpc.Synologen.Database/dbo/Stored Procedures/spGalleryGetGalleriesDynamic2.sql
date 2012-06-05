/*

*/


create PROCEDURE spGalleryGetGalleriesDynamic2
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
				FROM tblGalleryCategoryConnection conn
				INNER JOIN sfGalleryGetAllGalleries(1, @Locationid, @Languageid) g
				ON conn.cGalleryId = g.cId
				WHERE cCategoryId = @CategoryId 
				AND cName LIKE @SearchString + '%'
				ORDER BY cName ASC
		END
		ELSE
		BEGIN
				SELECT	g.*
				FROM tblGalleryCategoryConnection conn
				INNER JOIN sfGalleryGetAllGalleries(1, @Locationid, @Languageid) g
				ON conn.cGalleryId = g.cId
				WHERE cCategoryId = @CategoryId 
				ORDER BY cName ASC

		END
	END
	ELSE
	BEGIN
		IF @SearchString IS NOT NULL
		BEGIN
				SELECT	g.*
				FROM sfGalleryGetAllGalleries(1, @Locationid, @Languageid) g
				WHERE cName LIKE @SearchString + '%'
				ORDER BY cName ASC
		END
		ELSE
		BEGIN
				SELECT	g.*
				FROM sfGalleryGetAllGalleries(1, @Locationid, @Languageid) g
				ORDER BY cName ASC

		END	

	END
END
