using System.Collections.Generic;
using System.Threading;

using NUnit.Framework;

using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.OPQ.Core.Exceptions;
using Spinit.Wpc.Synologen.OPQ.Data.Test.Properties;

namespace Spinit.Wpc.Synologen.OPQ.Data.Test
{
	[TestFixture, Description ("The unit tests for the data file-manager.")]
	public class FileManagerTests
	{
		private Configuration _configuration;
		private Context _context;

		[SetUp, Description ("Initialize.")]
		public void NodeManagerInit ()
		{
			_configuration = new Configuration (
				Settings.Default.ConnectionString,
				Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);

			_context = new Context (
				Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
				string.Empty,
				PropertyValues.UserId,
				PropertyValues.UserName);
		}

		[TearDown, Description ("Close.")]
		public void NodeManagerCleanUp ()
		{
			_configuration = null;
			_context = null;
		}

		#region File tests

		[Test, Description ("Creates, fetches, updates and deletes a file."), Category ("Internal")]
		public void FileAddUpdateDeleteTest ()
		{
			File file;
			File fetchFile;
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {

				// Create a new file
				synologenRepository.File.Insert (
					new File
					{
						NdeId = PropertyValues.NodeId,
						FleId = PropertyValues.BaseFileId,
						FleCatId = PropertyValues.FileCategoryId,
						IsActive = true
					});

				synologenRepository.SubmitChanges ();

				file = synologenRepository.File.GetInsertedFile ();

				Assert.IsNotNull (file, "File is null.");

				// Fetch the file
				fetchFile = synologenRepository.File.GetFileById (file.Id);

				Assert.IsNotNull (fetchFile, "Fetched file is null.");
				Assert.AreEqual (PropertyValues.FileCategoryId, fetchFile.FleCatId, "File-category not correct!");
			}
			
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {

				// Update file
				fetchFile.FleId = PropertyValues.UpdatedBaseFileId;
				synologenRepository.File.Update (fetchFile);

				synologenRepository.SubmitChanges ();

				// ReFetch the file
				fetchFile = synologenRepository.File.GetFileById (file.Id);

				Assert.IsNotNull (fetchFile, "Fetched file is null.");
				Assert.AreEqual (PropertyValues.UpdatedBaseFileId, fetchFile.FleId, "Base-file not correct!");

				// Delete the file
				synologenRepository.File.Delete (fetchFile);

				synologenRepository.SubmitChanges ();

				bool found = true;
				try {
					// ReFetch the file
					fetchFile = synologenRepository.File.GetFileById (file.Id);

					Assert.IsNull (fetchFile, "Deleted file is not null.");
				}
				catch (ObjectNotFoundException e) {
					if (ObjectNotFoundErrors.FileNotFound == (ObjectNotFoundErrors) e.ErrorCode) {
						found = false;
					}
				}

				Assert.AreEqual (false, found, "Object still exist.");
			}
		}

		[Test, Description ("Moves files up and down."), Category ("Internal")]
		public void FileMoveUpDownTest ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.File.MoveFile (
					new File
					{
						Id = PropertyValues.MoveFileId,
						NdeId = PropertyValues.FileMoveNodeId,
						Order = PropertyValues.FileOrder2
					});
				synologenRepository.SubmitChanges ();

				File file = synologenRepository.File.GetFileById (PropertyValues.MoveFileId);
				Assert.AreEqual (PropertyValues.FileOrder2, file.Order, "Move down failed (1).");

				file = synologenRepository.File.GetFileById (PropertyValues.MovedFileId);
				Assert.AreEqual (PropertyValues.FileOrder1, file.Order, "Move down failed (2).");
			}
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.File.MoveFile (
					new File
					{
						Id = PropertyValues.MoveFileId,
						NdeId = PropertyValues.FileMoveNodeId,
						Order = PropertyValues.FileOrder1
					});
				synologenRepository.SubmitChanges ();

				File file = synologenRepository.File.GetFileById (PropertyValues.MoveFileId);
				Assert.AreEqual (PropertyValues.FileOrder1, file.Order, "Move up failed (1).");

				file = synologenRepository.File.GetFileById (PropertyValues.MovedNodeId);
				Assert.AreEqual (PropertyValues.FileOrder2, file.Order, "Move up failed (2).");
			}
		}

		[Test, Description ("Fetches the numner of files for a specified node (active)."), Category ("CruiseControl")]
		public void FileSearchNoOfFilesNodeActive ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				int count = synologenRepository.File.GetNumberOfFilesForNode (PropertyValues.NodeId, true, true);

				Assert.AreEqual (PropertyValues.NoOfFilesCategoryNode, count, "Wrong number of files (active)!");
			}
		}

		[Test, Description ("Fetches the numner of files for a specified category (active)."), Category ("CruiseControl")]
		public void FileSearchNoOfFilesCategoryActive ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				int count = synologenRepository.File.GetNumberOfFilesForFileCategory (PropertyValues.FileCategoryId, true, true);

				Assert.AreEqual (PropertyValues.NoOfFilesCategoryNode, count, "Wrong number of files (active)!");
			}
		}

		[Test, Description ("Fetches the numner of files for a specified node (all)."), Category ("CruiseControl")]
		public void FileSearchNoOfFilesNodeAll ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				int count = synologenRepository.File.GetNumberOfFilesForNode (PropertyValues.NodeId, false, false);

				Assert.AreEqual (PropertyValues.NoOfFilesCategoryNodeAll, count, "Wrong number of files (all)!");
			}
		}

		[Test, Description ("Fetches the numner of files for a specified category (all)."), Category ("CruiseControl")]
		public void FileSearchNoOfFilesCategoryAll ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				int count = synologenRepository.File.GetNumberOfFilesForFileCategory (PropertyValues.FileCategoryId, false, false);

				Assert.AreEqual (PropertyValues.NoOfFilesCategoryNodeAll, count, "Wrong number of files (all)!");
			}
		}

		[Test, Description ("Fetches the files for a specified node (active)."), Category ("CruiseControl")]
		public void FileSearchFilesNodeActive ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				IList<File> files = synologenRepository.File.GetFilesByNodeId (PropertyValues.FileMoveNodeId, true, true);

				Assert.IsNotNull (files, "Fetched files is null.");
				Assert.AreEqual (PropertyValues.NoOfFilesCategoryNode, files.Count, "Wrong number of files (active)!");
			}
		}

		[Test, Description ("Fetches the files for a specified node (all)."), Category ("CruiseControl")]
		public void FileSearchFilesNodeAll ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				IList<File> files = synologenRepository.File.GetFilesByNodeId (PropertyValues.FileMoveNodeId, false, false);

				Assert.IsNotNull (files, "Fetched files is null.");
				Assert.AreEqual (PropertyValues.NoOfFilesCategoryNodeAll, files.Count, "Wrong number of files (all)!");
			}
		}

		[Test, Description ("Fetches the files for a specified node and category=shop-routine (all)."), Category ("CruiseControl")]
		public void FileSearchFilesNodeCategoryAllNull ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				IList<File> files = synologenRepository.File.GetFilesByNodeId (
					PropertyValues.NodeNullCategory,
					FileCategories.ShopRoutineDocuments, 
					false, 
					false);

				Assert.IsNotNull (files, "Fetched files is null.");
				Assert.AreEqual (PropertyValues.NoOfFilesCategoryNodeShopRoutine, files.Count, "Wrong number of files (all)!");
			}
		}

		[Test, Description ("Fetches the files for a specified node and category (active)."), Category ("CruiseControl")]
		public void FileSearchFilesNodeCategoryActive ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				IList<File> files = synologenRepository.File.GetFilesByNodeId (
					PropertyValues.FileMoveNodeId, 
					PropertyValues.FileCategoryId,
					true,
					true);

				Assert.IsNotNull (files, "Fetched files is null.");
				Assert.AreEqual (PropertyValues.NoOfFilesCategoryNode, files.Count, "Wrong number of files (active)!");
			}
		}

		[Test, Description ("Fetches the files for a specified node and category (all)."), Category ("CruiseControl")]
		public void FileSearchFilesNodeCategoryAll ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				IList<File> files = synologenRepository.File.GetFilesByNodeId (
					PropertyValues.FileMoveNodeId,
					PropertyValues.FileCategoryId,
					false,
					false);

				Assert.IsNotNull (files, "Fetched files is null.");
				Assert.AreEqual (PropertyValues.NoOfFilesCategoryNodeAll, files.Count, "Wrong number of files (all)!");
			}
		}

		[Test, Description ("Fetches a specified file."), Category ("CruiseControl")]
		public void FileSearchFile ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				File file = synologenRepository.File.GetFileById (PropertyValues.FetchedFileId);

				Assert.IsNotNull (file, "Fetched file is null.");
				Assert.AreEqual (PropertyValues.BaseFileId, file.FleId, "Wrong base file!");
			}
		}

		#endregion

		#region File Category Tests

		[Test, Description ("Creates, fetches, updates and deletes a file-category."), Category ("Internal")]
		public void FileCategoryAddUpdateDeleteTest ()
		{
			FileCategory fileCategory;
			FileCategory fetchFileCategory;
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {

				// Create a new file-category
				synologenRepository.File.Insert (
					new FileCategory
					{
						Name = PropertyValues.FileCategoryName,
						IsActive = true
					});

				synologenRepository.SubmitChanges ();

				fileCategory = synologenRepository.File.GetInsertedFileCategory ();

				Assert.IsNotNull (fileCategory, "File-category is null.");

				// Fetch the file-category
				fetchFileCategory = synologenRepository.File.GetFileCategoryById (fileCategory.Id);

				Assert.IsNotNull (fetchFileCategory, "Fetched file-catgory is null.");
				Assert.AreEqual (PropertyValues.FileCategoryName, fetchFileCategory.Name, "Content are not equal");
			}
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				// Update file-category
				fetchFileCategory.Name = PropertyValues.FileCategoryNameUpdated;
				synologenRepository.File.Update (fetchFileCategory);

				synologenRepository.SubmitChanges ();

				fetchFileCategory = synologenRepository.File.GetFileCategoryById (fetchFileCategory.Id);

				Assert.IsNotNull (fetchFileCategory, "Fetched file-catgory is null.");
				Assert.AreEqual (PropertyValues.FileCategoryNameUpdated, fetchFileCategory.Name, "Updated content are not equal");
				
				// Delete the file-category
				synologenRepository.File.Delete (fetchFileCategory);

				synologenRepository.SubmitChanges ();

				bool found = true;
				try {
					// ReFetch the file-category
					fetchFileCategory = synologenRepository.File.GetFileCategoryById (fileCategory.Id);

					Assert.IsNull (fetchFileCategory, "Deleted file-category is not null.");
				}
				catch (ObjectNotFoundException e) {
					if (ObjectNotFoundErrors.FileCategoryNotFound == (ObjectNotFoundErrors) e.ErrorCode) {
						found = false;
					}
				}

				Assert.AreEqual (false, found, "Object still exist.");
			}
		}

		[Test, Description ("Fetches all active categories."), Category ("CruiseControl")]
		public void FileCategoriesSearchActive ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				List<FileCategory> fileCategories = (List<FileCategory>) synologenRepository.File.GetAllFileCategories (true);

				Assert.IsNotEmpty (fileCategories, "File-categories is empty (active).");
				Assert.GreaterOrEqual (fileCategories.Count, PropertyValues.NoOfCategoriesActive, "Wrong numer of categories.");
			}
		}

		[Test, Description ("Fetches all categories."), Category ("CruiseControl")]
		public void FileCategoriesSearchAll ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				List<FileCategory> fileCategories = (List<FileCategory>) synologenRepository.File.GetAllFileCategories (false);

				Assert.IsNotEmpty (fileCategories, "File-categories is empty (all).");
				Assert.GreaterOrEqual (fileCategories.Count, PropertyValues.NoOfCategoriesAll, "Wrong numer of categories.");
			}
		}

		[Test, Description ("Fetches a specified category."), Category ("CruiseControl")]
		public void FileCategoriesSearchSpecific ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				FileCategory fileCategory =  synologenRepository.File.GetFileCategoryById (PropertyValues.FileCategoryId);

				Assert.IsNotNull (fileCategory, "File-category is null.");
				Assert.AreEqual (fileCategory.Name, PropertyValues.FileCategoryContent, "Wrong file-category content.");
			}
		}

		#endregion
	}
}
