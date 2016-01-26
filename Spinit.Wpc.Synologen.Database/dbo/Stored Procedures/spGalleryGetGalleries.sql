/*
	Type 0 - Specific gallery
	Type 1 - All galleries

*/

CREATE PROCEDURE spGalleryGetGalleries
					@type INT,
					@galleryId INT,
					@categoryId INT,
					@locationId INT,
					@languageId INT,
					@owner INT,
					@userId INT,
					@fileId INT = NULL,
					@status INT OUTPUT
	AS
		BEGIN
			IF (@type = 0)
			BEGIN
				SELECT * 
				FROM sfGalleryGetAllGalleries(1, @Locationid, @Languageid) g
				WHERE cId = @galleryId
				AND cId IN (SELECT cId FROM sfGalleryAllowedGalleries(@userId))
			END
			IF (@type = 1)
			BEGIN
				SELECT g.*
				FROM sfGalleryGetAllGalleries(1, @Locationid, @Languageid) g
				INNER JOIN tblGalleryLocationConnection loc
				ON loc.cGalleryId=g.cId
				INNER JOIN tblGalleryLanguageConnection lang
				ON lang.cGalleryId=g.cId
				INNER JOIN tblGalleryCategoryConnection cat
				ON cat.cGalleryId=g.cId
				WHERE cCategoryId = @categoryId
				AND cLocationId = @locationId
				AND cLanguageId = @languageId
				AND cId IN (SELECT cId FROM sfGalleryAllowedGalleries(@userId))
				ORDER BY cCreatedDate ASC
			END
			IF (@type = 2)
			BEGIN
				SELECT g.*
				FROM sfGalleryGetAllGalleries(1, @Locationid, @Languageid) g
				INNER JOIN tblGalleryLocationConnection loc
				ON loc.cGalleryId=g.cId
				INNER JOIN tblGalleryLanguageConnection lang
				ON lang.cGalleryId=g.cId
				WHERE cLocationId = @locationId
				AND cLanguageId = @languageId
				AND cId IN (SELECT cId FROM sfGalleryAllowedGalleries(@userId))
				ORDER BY cCreatedDate ASC
			END
			IF (@type = 3)
			BEGIN
				SELECT g.*
				FROM sfGalleryGetAllGalleries(1, @Locationid, @Languageid) g
				INNER JOIN tblGalleryLocationConnection loc
				ON loc.cGalleryId=g.cId
				INNER JOIN tblGalleryLanguageConnection lang
				ON lang.cGalleryId=g.cId
				INNER JOIN tblGalleryCategoryConnection cat
				ON cat.cGalleryId=g.cId
				WHERE cCategoryId = @categoryId
				AND cLocationId = @locationId
				AND cLanguageId = @languageId
				AND cActive = 1
				AND cId IN (SELECT cId FROM sfGalleryAllowedGalleries(@userId))
				ORDER BY cCreatedDate ASC
			END
			IF (@type = 4)
			BEGIN
				SELECT g.*
				FROM sfGalleryGetAllGalleries(1, @Locationid, @Languageid) g
				INNER JOIN tblGalleryLocationConnection loc
				ON loc.cGalleryId=g.cId
				INNER JOIN tblGalleryLanguageConnection lang
				ON lang.cGalleryId=g.cId
				WHERE cLocationId = @locationId
				AND cLanguageId = @languageId
				AND cActive = 1
				AND cId IN (SELECT cId FROM sfGalleryAllowedGalleries(@userId))
				ORDER BY cCreatedDate ASC
			END
			IF (@type = 5)
			BEGIN
				SELECT g.*
				FROM sfGalleryGetAllGalleries(1, @Locationid, @Languageid) g
				INNER JOIN tblGalleryLocationConnection loc
				ON loc.cGalleryId=g.cId
				INNER JOIN tblGalleryLanguageConnection lang
				ON lang.cGalleryId=g.cId
				INNER JOIN tblGalleryCategoryConnection cat
				ON cat.cGalleryId=g.cId
				WHERE cCategoryId = @categoryId
				AND cLocationId = @locationId
				AND cLanguageId = @languageId
				AND cActive = 1
				AND cOwner = @owner
				AND cId IN (SELECT cId FROM sfGalleryAllowedGalleries(@userId))
				ORDER BY cCreatedDate ASC
			END
			
			IF (@type = 6) --File
			BEGIN
				SELECT g.*
				FROM sfGalleryGetAllGalleries(1, @Locationid, @Languageid) g
				INNER JOIN tblGalleryLocationConnection loc
				ON loc.cGalleryId=g.cId
				INNER JOIN tblGalleryLanguageConnection lang
				ON lang.cGalleryId=g.cId
				INNER JOIN tblGalleryFiles files
				ON files.cGalleryId=g.cId
				WHERE cLocationId = @locationId
				AND cLanguageId = @languageId
				AND files.cFileId = @fileId
				ORDER BY cCreatedDate ASC
			END
			
			
			
			SELECT @status = @@ERROR
		END
