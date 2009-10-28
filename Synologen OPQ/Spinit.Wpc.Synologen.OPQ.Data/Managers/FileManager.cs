using System;
using System.Collections.Generic;
using System.Linq;

using Spinit.Data.Linq;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.Opq.Core.Exceptions;
using Spinit.Wpc.Synologen.OPQ.Data;
using Spinit.Wpc.Synologen.OPQ.Data.Entities;

namespace Spinit.Wpc.Synologen.Opq.Data.Managers
{
	public class FileManager : EntityManager<WpcSynologenRepository>
	{
		private readonly WpcSynologenDataContext _dataContext;

		private EFile _insertedFile;
		private EFileCategory _insertedFileCategory;
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="manager">The repository-manager.</param>

		public FileManager (WpcSynologenRepository manager) : base (manager)
		{
			_dataContext = (WpcSynologenDataContext) Manager.Context;
		}

		#region Create

		#region Create File

		/// <summary>
		/// Inserts a file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <exception cref="UserException">If no current-user.</exception>

		private void Insert (EFile file)
		{
			file.Order = 0;
			file.CreatedById = Manager.WebContext.UserId ?? 0;
			file.CreatedByName = Manager.WebContext.UserName;
			file.CreatedDate = DateTime.Now;

			if ((file.CreatedById == 0) || (file.CreatedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			_insertedFile = file;

			_dataContext.Files.InsertOnSubmit (file);
		}

		/// <summary>
		/// Inserts a file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <exception cref="UserException">If no current-user.</exception>

		public void Insert (File file)
		{
			Insert (EFile.Convert (file));
		}

		/// <summary>
		/// Returns the inserted file.
		/// </summary>
		/// <returns>The inserted file.</returns>

		public File GetInsertedFile ()
		{
			return EFile.Convert (_insertedFile);
		}

		#endregion

		#region Create File Category

		/// <summary>
		/// Inserts a file-category.
		/// </summary>
		/// <param name="fileCategory">The file-category.</param>
		/// <exception cref="UserException">If no current-user.</exception>

		private void Insert (EFileCategory fileCategory)
		{
			fileCategory.CreatedById = Manager.WebContext.UserId ?? 0;
			fileCategory.CreatedByName = Manager.WebContext.UserName;
			fileCategory.CreatedDate = DateTime.Now;

			if ((fileCategory.CreatedById == 0) || (fileCategory.CreatedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			_insertedFileCategory = fileCategory;

			_dataContext.FileCategories.InsertOnSubmit (fileCategory);
		}

		/// <summary>
		/// Inserts a file-category.
		/// </summary>
		/// <param name="fileCategory">The file-category.</param>
		/// <exception cref="UserException">If no current-user.</exception>

		public void Insert (FileCategory fileCategory)
		{
			Insert (EFileCategory.Convert (fileCategory));
		}

		/// <summary>
		/// Returns the inserted file-category.
		/// </summary>
		/// <returns>The inserted file-category.</returns>

		public FileCategory GetInsertedFileCategory ()
		{
			return EFileCategory.Convert (_insertedFileCategory);
		}

		#endregion

		#endregion

		#region Remove

		#region Remove File

		/// <summary>
		/// Deletes all documents for a node.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <exception cref="ObjectNotFoundException">If the file is not found.</exception>

		public void DeleteAllForNode (int nodeId)
		{
			var query = from file in _dataContext.Files
						where file.NdeId == nodeId
						select file;

			IList<EFile> files = query.ToList ();

			if (files.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"File not found.",
					ObjectNotFoundErrors.FileNotFound);
			}

			_dataContext.Files.DeleteAllOnSubmit (files);
		}

		#endregion

		#endregion

		#region Fetch

		#region Fetch File

		/// <summary>
		/// Fetches the file by file-id.
		/// </summary>
		/// <param name="fileId">The file-id.</param>
		/// <returns>A file.</returns>
		/// <exception cref="ObjectNotFoundException">If the file is not found.</exception>

		public File GetFileById (int fileId)
		{
			var query = from file in _dataContext.Files
						where file.Id == fileId
						select file;

			IList<EFile> files = query.ToList ();

			if (files.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"File not found.",
					ObjectNotFoundErrors.FileNotFound);
			}

			return EFile.Convert (files.First ());
		}

		#endregion

		#region Fetch File Categories

		/// <summary>
		/// Fetches the file-category by file-category-id.
		/// </summary>
		/// <param name="fileCategoryId">The file-category-id.</param>
		/// <returns>A file-category.</returns>
		/// <exception cref="ObjectNotFoundException">If the file-category is not found.</exception>

		public FileCategory GetFileCategoryById (int fileCategoryId)
		{
			var query = from fileCategory in _dataContext.FileCategories
						where fileCategory.Id == fileCategoryId
						select fileCategory;

			IList<EFileCategory> fileCategories = query.ToList ();

			if (fileCategories.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"File-category not found.",
					ObjectNotFoundErrors.FileCategoryNotFound);
			}

			return EFileCategory.Convert (fileCategories.First ());

		}

		/// <summary>
		/// Fetches all file-categories.
		/// </summary>
		/// <returns>A list of file-categories.</returns>
		/// <exception cref="ObjectNotFoundException">If no file-categories are found.</exception>

		public IList<FileCategory> GetAllFileCategories ()
		{
			var query = from fileCategory in _dataContext.FileCategories
						orderby fileCategory.Name ascending
						select fileCategory;

			Converter<EFileCategory, FileCategory> converter = Converter;
			IList<FileCategory> fileCategories = query.ToList ().ConvertAll (converter);

			if (fileCategories == null) {
				throw new ObjectNotFoundException (
					"File-category not found.",
					ObjectNotFoundErrors.FileCategoryNotFound);
			}

			return fileCategories;
		}

		#endregion

		#endregion

		#region Converters

		/// <summary>
		/// Converts a EFile to a File with associations.
		/// </summary>
		/// <param name="eFile">The eFile.</param>
		/// <returns>A file.</returns>

		public File Converter (EFile eFile)
		{
			File file = EFile.Convert (eFile);

			if (!SkipFileCategory && (eFile.FileCategory != null)) {
				SkipFiles = true;
				file.FileCategory = Converter (eFile.FileCategory);
				SkipFiles = false;
			}

			if (!SkipNode && (eFile.Node != null)) {
				Manager.Node.SkipFiles = true;
				file.Node = Manager.Node.Converter (eFile.Node);
				Manager.Node.SkipFiles = false;
			}

			// Only fetch users flat (external object).
			if (!SkipUsers && (eFile.CreatedBy != null)) {
				file.CreatedBy = EBaseUser.Convert (eFile.CreatedBy);
			}

			// Only fetch users flat (external object).
			if (!SkipUsers && (eFile.ChangedBy != null)) {
				file.ChangedBy = EBaseUser.Convert (eFile.ChangedBy);
			}

			// Only fetch users flat (external object).
			if (!SkipUsers && (eFile.ApprovedBy != null)) {
				file.ApprovedBy = EBaseUser.Convert (eFile.ApprovedBy);
			}

			// Only fetch users flat (external object).
			if (!SkipUsers && (eFile.LockedBy != null)) {
				file.LockedBy = EBaseUser.Convert (eFile.LockedBy);
			}

			// Only fetch users flat (external object).
			if (!SkipConcern && (eFile.Concern != null)) {
				file.Concern = EConcern.Convert (eFile.Concern);
			}

			// Only fetch users flat (external object).
			if (!SkipShop && (eFile.Shop != null)) {
				file.Shop = EShop.Convert (eFile.Shop);
			}

			return file;
		}

		/// <summary>
		/// Converts a EFileCategory to a FileCategory with associations.
		/// </summary>
		/// <param name="eFileCategory">The eFileCategory.</param>
		/// <returns>A file-category.</returns>

		public FileCategory Converter (EFileCategory eFileCategory)
		{
			FileCategory fileCategory = EFileCategory.Convert (eFileCategory);

			if (!SkipFiles && (eFileCategory.Files != null)) {
				SkipFileCategory = true;
				Converter<EFile, File> converter = Converter;
				fileCategory.Files = eFileCategory.Files.ToList ().ConvertAll (converter);
				SkipFileCategory = false;
			}

			// Only fetch users flat (external object).
			if (!SkipUsers && (eFileCategory.CreatedBy != null)) {
				fileCategory.CreatedBy = EBaseUser.Convert (eFileCategory.CreatedBy);
			}

			// Only fetch users flat (external object).
			if (!SkipUsers && (eFileCategory.ChangedBy != null)) {
				fileCategory.ChangedBy = EBaseUser.Convert (eFileCategory.ChangedBy);
			}

			return fileCategory;
		}


		#endregion

		#region Properties

		#region Skips for File

		/// <summary>
		/// If true=>skip filling file-category.
		/// </summary>

		public bool SkipFileCategory { get; set; }

		/// <summary>
		/// If true=>skip filling node.
		/// </summary>

		public bool SkipNode { get; set; }

		/// <summary>
		/// If true=>skip filling users.
		/// </summary>

		public bool SkipUsers { get; set; }

		/// <summary>
		/// If true=>skip filling concern.
		/// </summary>

		public bool SkipConcern { get; set; }

		/// <summary>
		/// If true=>skip filling shop.
		/// </summary>

		public bool SkipShop { get; set; }

		#endregion

		#region Skips for File Category

		/// <summary>
		/// If ture=>skip filling file.
		/// </summary>

		public bool SkipFiles { get; set; }

		#endregion

		#endregion
	}
}
