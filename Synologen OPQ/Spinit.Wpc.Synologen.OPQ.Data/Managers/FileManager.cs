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
		/// <exception cref="ObjectNotFoundException">If user, file-category, base-file, shop or concern does not exist.</exception>

		private void Insert (EFile file)
		{
			file.Order = 0;
			file.CreatedById = Manager.WebContext.UserId ?? 0;
			file.CreatedByName = Manager.WebContext.UserName;
			file.CreatedDate = DateTime.Now;

			if ((file.CreatedById == 0) || (file.CreatedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist (file.CreatedById);
			Manager.ExternalObjectsManager.CheckFileExist (file.FleId);
			CheckFileCategoryExist (file.FleCatId);

			if (file.ShpId != null) {
				Manager.ExternalObjectsManager.CheckShopExist ((int) file.ShpId);
			}

			if (file.CncId != null) {
				Manager.ExternalObjectsManager.CheckConcernExist ((int) file.CncId);
			}

			file.IsActive = true;

			_insertedFile = file;

			_dataContext.Files.InsertOnSubmit (file);
		}

		/// <summary>
		/// Inserts a file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If user, file-category, base-file, shop or concern does not exist.</exception>

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

			if (CheckFileCategoryNameExist (fileCategory.Name, null)) {
				throw new FileException ("File category exist.", FileErrors.FileCategoryExist);
			}

			fileCategory.IsActive = true;

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

		#region Change

		#region Change File

		/// <summary>
		/// Updates a file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If file, user, file-category or base-file does not exist.</exception>
		/// <exception cref="FileException">
		/// 1. If file is locked by other user. 
		/// 2. If shop or concern is changed.</exception>

		private void Update (EFile file)
		{
			EFile oldFile = _dataContext.Files.Single (d => d.Id == file.Id);

			if (oldFile == null) {
				throw new ObjectNotFoundException (
					"File not found.",
					ObjectNotFoundErrors.FileNotFound);
			}

			if ((oldFile.LockedById != null) && (oldFile.LockedById != Manager.WebContext.UserId)) {
				throw new FileException ("File locked by another user.", FileErrors.FileLockedByOtherUser);
			}

			oldFile.ChangedById = Manager.WebContext.UserId ?? 0;
			oldFile.ChangedByName = Manager.WebContext.UserName;
			oldFile.ChangedDate = DateTime.Now;

			if ((oldFile.ChangedById == 0) || (oldFile.ChangedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldFile.ChangedById);

			if (oldFile.ShpId != file.ShpId) {
				throw new FileException ("Shop change not allowed", FileErrors.ChangeOfShopNotAllowed);
			}

			if (oldFile.CncId != file.CncId) {
				throw new FileException ("Concern change not allowed", FileErrors.ChangeOfConcernNotAllowed);
			}

			if (oldFile.FleCatId != file.FleCatId) {
				CheckFileCategoryExist (file.FleCatId);
				oldFile.FleCatId = file.FleCatId;
			}

			if (oldFile.FleId != file.FleId) {
				Manager.ExternalObjectsManager.CheckFileExist (file.FleId);
				oldFile.FleId = file.FleId;
			}
		}

		/// <summary>
		/// Updates a file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the file is not found.</exception>
		/// <exception cref="FileException">
		/// 1. If file is locked by other user. 
		/// 2. If shop or concern is changed.</exception>

		public void Update (File file)
		{
			Update (EFile.Convert (file));
		}

		#endregion

		#region Deactive & Reactivate File

		/// <summary>
		/// Deactivates a file.
		/// </summary>
		/// <param name="fileId">The file-id.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the file or user is not found.</exception>

		public void DeactivateFile (int fileId)
		{
			EFile oldFile = _dataContext.Files.Single (n => n.Id == fileId);

			if (oldFile == null) {
				throw new ObjectNotFoundException (
					"File not found.",
					ObjectNotFoundErrors.FileNotFound);
			}

			if ((oldFile.LockedById != null) && (oldFile.LockedById != Manager.WebContext.UserId)) {
				throw new FileException ("File locked by another user.", FileErrors.FileLockedByOtherUser);
			}

			oldFile.ChangedById = Manager.WebContext.UserId ?? 0;
			oldFile.ChangedByName = Manager.WebContext.UserName;
			oldFile.ChangedDate = DateTime.Now;

			if ((oldFile.ChangedById == 0) || (oldFile.ChangedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldFile.ChangedById);

			oldFile.IsActive = false;
		}

		/// <summary>
		/// Reactivates a file.
		/// </summary>
		/// <param name="fileId">The file-id.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the file or user is not found.</exception>

		public void ReactivateFile (int fileId)
		{
			EFile oldFile = _dataContext.Files.Single (d => d.Id == fileId);

			if (oldFile == null) {
				throw new ObjectNotFoundException (
					"File not found.",
					ObjectNotFoundErrors.FileNotFound);
			}

			if ((oldFile.LockedById != null) && (oldFile.LockedById != Manager.WebContext.UserId)) {
				throw new FileException ("File locked by another user.", FileErrors.FileLockedByOtherUser);
			}

			oldFile.ChangedById = Manager.WebContext.UserId ?? 0;
			oldFile.ChangedByName = Manager.WebContext.UserName;
			oldFile.ChangedDate = DateTime.Now;

			if ((oldFile.ChangedById == 0) || (oldFile.ChangedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldFile.ChangedById);

			oldFile.IsActive = true;
		}

		#endregion

		#region Move File

		/// <summary>
		/// Moves a file.
		/// </summary>
		/// <param name="file">The file to move.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the file or user is not found.</exception>
		/// <exception cref="FileException">The move is forbidden or the position is not changed.</exception>

		private void MoveFile (EFile file)
		{
			EFile oldFile = _dataContext.Files.Single (n => n.Id == file.Id);

			if (oldFile == null) {
				throw new ObjectNotFoundException (
					"File not found.",
					ObjectNotFoundErrors.FileNotFound);
			}

			if ((oldFile.LockedById != null) && (oldFile.LockedById != Manager.WebContext.UserId)) {
				throw new FileException ("File locked by another user.", FileErrors.FileLockedByOtherUser);
			}

			oldFile.ChangedById = Manager.WebContext.UserId ?? 0;
			oldFile.ChangedByName = Manager.WebContext.UserName;
			oldFile.ChangedDate = DateTime.Now;

			if ((oldFile.ChangedById == 0) || (oldFile.ChangedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldFile.ChangedById);

			if (oldFile.NdeId != file.NdeId) {
				throw new FileException ("Position not valid.", FileErrors.MoveToForbidden);
			}

			if (oldFile.Order == file.Order) {
				throw new FileException ("Position not changed.", FileErrors.PositionNotMoved);
			}

			if ((file.Order < 1) || (file.Order > (GetNumberOfFilesForNode (file.NdeId, false, false) + 1))) {
				throw new FileException ("Position not valid.", FileErrors.MoveToForbidden);
			}

			oldFile.Order = file.Order;
		}

		/// <summary>
		/// Moves a file.
		/// </summary>
		/// <param name="file">The file to move.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the file or user is not found.</exception>
		/// <exception cref="FileException">The move is forbidden or the position is not changed.</exception>

		public void MoveFile (File file)
		{
			MoveFile (EFile.Convert (file));
		}

		#endregion

		#region Approve, Check-Out and Check-In files

		/// <summary>
		/// Approves a file.
		/// </summary>
		/// <param name="fileId">The file-id.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the file or user is not found.</exception>

		public void ApproveFile (int fileId)
		{
			EFile oldFile = _dataContext.Files.Single (d => d.Id == fileId);

			if (oldFile == null) {
				throw new ObjectNotFoundException (
					"File not found.",
					ObjectNotFoundErrors.FileNotFound);
			}

			if ((oldFile.LockedById != null) && (oldFile.LockedById != Manager.WebContext.UserId)) {
				throw new FileException ("File locked by another user.", FileErrors.FileLockedByOtherUser);
			}

			oldFile.ApprovedById = Manager.WebContext.UserId ?? 0;
			oldFile.ApprovedByName = Manager.WebContext.UserName;
			oldFile.ApprovedDate = DateTime.Now;

			if ((oldFile.ApprovedById == 0) || (oldFile.ApprovedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldFile.ChangedById);
		}

		/// <summary>
		/// Checks out a file.
		/// </summary>
		/// <param name="fileId">The file-id.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the file or user is not found.</exception>

		public void CheckOutFile (int fileId)
		{
			EFile oldFile = _dataContext.Files.Single (d => d.Id == fileId);

			if (oldFile == null) {
				throw new ObjectNotFoundException (
					"File not found.",
					ObjectNotFoundErrors.FileNotFound);
			}

			if ((oldFile.LockedById != null) && (oldFile.LockedById != Manager.WebContext.UserId)) {
				throw new FileException ("File locked by another user.", FileErrors.FileLockedByOtherUser);
			}

			oldFile.LockedById = Manager.WebContext.UserId ?? 0;
			oldFile.LockedByName = Manager.WebContext.UserName;
			oldFile.LockedDate = DateTime.Now;

			if ((oldFile.LockedById == 0) || (oldFile.LockedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}
	
			Manager.ExternalObjectsManager.CheckUserExist ((int) oldFile.ChangedById);
		}

		/// <summary>
		/// Checks-in a file.
		/// </summary>
		/// <param name="fileId">The file-id.</param>
		/// <exception cref="ObjectNotFoundException">If the file is not found.</exception>

		public void CheckInFile (int fileId)
		{
			EFile oldFile = _dataContext.Files.Single (d => d.Id == fileId);

			if (oldFile == null) {
				throw new ObjectNotFoundException (
					"File not found.",
					ObjectNotFoundErrors.FileNotFound);
			}

			if ((oldFile.LockedById != null) && (oldFile.LockedById != Manager.WebContext.UserId)) {
				throw new FileException ("File locked by another user.", FileErrors.FileLockedByOtherUser);
			}

			oldFile.LockedById = null;
			oldFile.LockedByName = null;
			oldFile.LockedDate = null;
		}

		#endregion

		#region Change File Category

		/// <summary>
		/// Updates a file-category.
		/// </summary>
		/// <param name="fileCategory">The file-category.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If file-category or user does not exist.</exception>

		private void Update (EFileCategory fileCategory)
		{
			EFileCategory oldFileCategory = _dataContext.FileCategories.Single (d => d.Id == fileCategory.Id);

			if (oldFileCategory == null) {
				throw new ObjectNotFoundException (
					"File not found.",
					ObjectNotFoundErrors.FileNotFound);
			}

			oldFileCategory.ChangedById = Manager.WebContext.UserId ?? 0;
			oldFileCategory.ChangedByName = Manager.WebContext.UserName;
			oldFileCategory.ChangedDate = DateTime.Now;

			if ((oldFileCategory.ChangedById == 0) || (oldFileCategory.ChangedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldFileCategory.ChangedById);

			if (!oldFileCategory.Name.Equals (fileCategory.Name)) {
				if (CheckFileCategoryNameExist (fileCategory.Name, fileCategory.Id)) {
					throw new FileException ("File category exist.", FileErrors.FileCategoryExist);
				}
				oldFileCategory.Name = fileCategory.Name;
			}
		}

		/// <summary>
		/// Updates a file-category.
		/// </summary>
		/// <param name="fileCategory">The file-category.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the file is not found.</exception>

		public void Update (FileCategory fileCategory)
		{
			Update (EFileCategory.Convert (fileCategory));
		}

		#endregion

		#region Deactive & Reactivate File Category

		/// <summary>
		/// Deactivates a file.
		/// </summary>
		/// <param name="fileCategoryId">The file-category-id.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the file-category or user is not found.</exception>

		public void DeactivateFileCategory (int fileCategoryId)
		{
			EFileCategory oldFileCategory = _dataContext.FileCategories.Single (n => n.Id == fileCategoryId);

			if (oldFileCategory == null) {
				throw new ObjectNotFoundException (
					"File-category not found.",
					ObjectNotFoundErrors.FileCategoryNotFound);
			}

			oldFileCategory.ChangedById = Manager.WebContext.UserId ?? 0;
			oldFileCategory.ChangedByName = Manager.WebContext.UserName;
			oldFileCategory.ChangedDate = DateTime.Now;

			if ((oldFileCategory.ChangedById == 0) || (oldFileCategory.ChangedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldFileCategory.ChangedById);

			oldFileCategory.IsActive = false;
		}

		/// <summary>
		/// Reactivates a file-category.
		/// </summary>
		/// <param name="fileCategoryId">The file-cateogry-id.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the file or user is not found.</exception>

		public void ReactivateFileCategory (int fileCategoryId)
		{
			EFileCategory oldFileCategory = _dataContext.FileCategories.Single (d => d.Id == fileCategoryId);

			if (oldFileCategory == null) {
				throw new ObjectNotFoundException (
					"File-category not found.",
					ObjectNotFoundErrors.FileCategoryNotFound);
			}

			oldFileCategory.ChangedById = Manager.WebContext.UserId ?? 0;
			oldFileCategory.ChangedByName = Manager.WebContext.UserName;
			oldFileCategory.ChangedDate = DateTime.Now;

			if ((oldFileCategory.ChangedById == 0) || (oldFileCategory.ChangedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldFileCategory.ChangedById);

			oldFileCategory.IsActive = true;
		}

		#endregion

		#endregion

		#region Remove

		#region Remove File

		/// <summary>
		/// Deletes a specific file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <exception cref="ObjectNotFoundException">If the file is not found.</exception>

		private void Delete (EFile file)
		{
			EFile oldFile = _dataContext.Files.Single (n => n.Id == file.Id);

			if (oldFile == null) {
				throw new ObjectNotFoundException (
					"File not found.",
					ObjectNotFoundErrors.FileNotFound);
			}

			if ((oldFile.LockedById != null) && (oldFile.LockedById != Manager.WebContext.UserId)) {
				throw new FileException ("File locked by another user.", FileErrors.FileLockedByOtherUser);
			}

			_dataContext.Files.DeleteOnSubmit (oldFile);
		}

		/// <summary>
		/// Deletes a specific file.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <exception cref="ObjectNotFoundException">If the file is not found.</exception>

		public void Delete (File file)
		{
			Delete (EFile.Convert (file));
		}

		/// <summary>
		/// Deletes all files for a node.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <exception cref="ObjectNotFoundException">If the file is not found.</exception>

		public void DeleteAllForNode (int nodeId)
		{
			IQueryable<EFile> query = from file in _dataContext.Files
						where file.NdeId == nodeId
						select file;

			IList<EFile> files = query.ToList ();

			foreach (EFile oldFile in files) {
				if ((oldFile.LockedById != null) && (oldFile.LockedById != Manager.WebContext.UserId)) {
					throw new FileException ("File locked by another user.", FileErrors.FileLockedByOtherUser);
				}
			}

			if (files.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"File not found.",
					ObjectNotFoundErrors.FileNotFound);
			}

			_dataContext.Files.DeleteAllOnSubmit (files);
		}

		#endregion

		#region Remove File Category

		/// <summary>
		/// Deletes a specific file-category.
		/// </summary>
		/// <param name="fileCategory">The file-category.</param>
		/// <exception cref="ObjectNotFoundException">If the file is not found.</exception>
		/// <exception cref="FileException">If the file-category is in use.</exception>

		private void Delete (EFileCategory fileCategory)
		{
			EFileCategory oldFileCategory = _dataContext.FileCategories.Single (n => n.Id == fileCategory.Id);

			if (oldFileCategory == null) {
				throw new ObjectNotFoundException (
					"File-category not found.",
					ObjectNotFoundErrors.FileCategoryNotFound);
			}

			if (GetNumberOfFilesForFileCategory (fileCategory.Id, false, false) > 0) {
				throw new FileException ("File-category is in use.", FileErrors.FileCategoryInUse);
			}

			_dataContext.FileCategories.DeleteOnSubmit (oldFileCategory);
		}

		/// <summary>
		/// Deletes a specific file-category.
		/// </summary>
		/// <param name="fileCategory">The file-category.</param>
		/// <exception cref="ObjectNotFoundException">If the file-category is not found.</exception>

		public void Delete (FileCategory fileCategory)
		{
			Delete (EFileCategory.Convert (fileCategory));
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
			IQueryable<EFile> query = from file in _dataContext.Files
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

		/// <summary>
		/// Fetches a list of files for a node.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <param name="onlyActive">If true=>fetch only active.</param>
		/// <param name="onlyApproved">If true=>fetch only approved and un-locked.</param>
		/// <returns>A list of files.</returns>
		/// <exception cref="ObjectNotFoundException">If the file is not found.</exception>

		public IList<File> GetFilesByNodeId (int nodeId, bool onlyActive, bool onlyApproved)
		{
			IOrderedQueryable<EFile> query = from file in _dataContext.Files
												 where file.NdeId == nodeId
												 orderby file.Order ascending
												 select file;

			if (onlyActive) {
				query = query.AddEqualityCondition ("IsActive", true);
			}

			if (onlyApproved) {
				query = query.AddIsNotNullCondition ("ApprovedById");
				query = query.AddIsNullCondition ("LockedById");
			}

			Converter<EFile, File> converter = Converter;
			IList<File> files = query.ToList ().ConvertAll (converter);

			if (files == null) {
				throw new ObjectNotFoundException (
					"File not found.",
					ObjectNotFoundErrors.FileNotFound);
			}

			return files;
		}

		/// <summary>
		/// Fetches a list of files for a node.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <param name="fileCategoryId">The file-category.</param>
		/// <param name="onlyActive">If true=>fetch only active.</param>
		/// <param name="onlyApproved">If true=>fetch only approved and un-locked.</param>
		/// <returns>A list of files.</returns>
		/// <exception cref="ObjectNotFoundException">If the file is not found.</exception>

		public IList<File> GetFilesByNodeId (int nodeId, int fileCategoryId, bool onlyActive, bool onlyApproved)
		{
			IOrderedQueryable<EFile> query = from file in _dataContext.Files
			                                 where file.NdeId == nodeId && file.FleCatId == fileCategoryId
			                                 orderby file.Order ascending
			                                 select file;

			if (onlyActive) {
				query = query.AddEqualityCondition ("IsActive", true);
			}

			if (onlyApproved) {
				query = query.AddIsNotNullCondition ("ApprovedById");
				query = query.AddIsNullCondition ("LockedById");
			}

			Converter<EFile, File> converter = Converter;
			IList<File> files = query.ToList ().ConvertAll (converter);

			if (files == null) {
				throw new ObjectNotFoundException (
					"File not found.",
					ObjectNotFoundErrors.FileNotFound);
			}
			
			return files;
		}

		/// <summary>
		/// Fetches a list of files for a node.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <param name="shopId">The shop-id.</param>
		/// <param name="fileCategoryId">The file-category.</param>
		/// <param name="onlyActive">If true=>fetch only active.</param>
		/// <param name="onlyApproved">If true=>fetch only approved and un-locked.</param>
		/// <returns>A list of files.</returns>
		/// <exception cref="ObjectNotFoundException">If the file is not found.</exception>

		public IList<File> GetFilesByNodeId (int nodeId, int shopId, int fileCategoryId, bool onlyActive, bool onlyApproved)
		{
			IOrderedQueryable<EFile> query = from file in _dataContext.Files
			                                 where
			                                 	file.NdeId == nodeId && file.ShpId == shopId
			                                 	&& file.FleCatId == fileCategoryId
			                                 orderby file.Order ascending
			                                 select file;

			if (onlyActive) {
				query = query.AddEqualityCondition ("IsActive", true);
			}

			if (onlyApproved) {
				query = query.AddIsNotNullCondition ("ApprovedById");
				query = query.AddIsNullCondition ("LockedById");
			}

			Converter<EFile, File> converter = Converter;
			IList<File> files = query.ToList ().ConvertAll (converter);

			if (files == null) {
				throw new ObjectNotFoundException (
					"File not found.",
					ObjectNotFoundErrors.FileNotFound);
			}

			return files;
		}

		/// <summary>
		/// Fetches a list of all files.
		/// </summary>
		/// <param name="onlyActive">If true=>fetch only active.</param>
		/// <param name="onlyApproved">If true=>fetch only approved and un-locked.</param>
		/// <returns>A list of files.</returns>
		/// <exception cref="ObjectNotFoundException">If the file is not found.</exception>

		public IList<File> GetAllFiles (bool onlyActive, bool onlyApproved)
		{
			IOrderedQueryable<EFile> query = from file in _dataContext.Files
			                                 orderby file.NdeId ascending , file.Order ascending
			                                 select file;

			if (onlyActive) {
				query = query.AddEqualityCondition ("IsActive", true);
			}

			if (onlyApproved) {
				query = query.AddIsNotNullCondition ("ApprovedById");
				query = query.AddIsNullCondition ("LockedById");
			}

			Converter<EFile, File> converter = Converter;
			IList<File> files = query.ToList ().ConvertAll (converter);

			if (files == null) {
				throw new ObjectNotFoundException (
					"File not found.",
					ObjectNotFoundErrors.FileNotFound);
			}
		
			return files;
		}

		/// <summary>
		/// Counts the number of files for a node. 
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <param name="onlyActive">If true=>fetch only active.</param>
		/// <param name="onlyApproved">If true=>fetch only approved and un-locked.</param>
		/// <returns>The number of files.</returns>

		public int GetNumberOfFilesForNode (int nodeId, bool onlyActive, bool onlyApproved)
		{
			if (onlyApproved) {
				if (onlyActive) {
					return _dataContext.Files.Count (
						file => file.NdeId == nodeId 
								&& file.IsActive
								&& file.ApprovedById != null
								&& file.LockedById == null);
				}

				return _dataContext.Files.Count (
					file => file.NdeId == nodeId
					&& file.ApprovedById != null
					&& file.LockedById == null);
			}
			
			if (onlyActive) {
				return _dataContext.Files.Count (file => file.NdeId == nodeId && file.IsActive);
			}

			return _dataContext.Files.Count (file => file.NdeId == nodeId);
		}

		/// <summary>
		/// Counts the number of files for a file-category. 
		/// </summary>
		/// <param name="fileCategoryId">The file-category-id.</param>
		/// <param name="onlyActive">If true=>fetch only active.</param>
		/// <param name="onlyApproved">If true=>fetch only approved and un-locked.</param>
		/// <returns>The number of files.</returns>

		public int GetNumberOfFilesForFileCategory (int fileCategoryId, bool onlyActive, bool onlyApproved)
		{
			if (onlyApproved) {
				if (onlyActive) {
					return _dataContext.Files.Count (
						file => file.FleCatId == fileCategoryId
								&& file.IsActive
								&& file.ApprovedById != null
								&& file.LockedById == null);
				}

				return _dataContext.Files.Count (
					file => file.FleCatId == fileCategoryId
							&& file.ApprovedById != null
							&& file.LockedById == null);
			}
			
			if (onlyActive) {
				return _dataContext.Files.Count (file => file.FleCatId == fileCategoryId && file.IsActive);
			}

			return _dataContext.Files.Count (file => file.FleCatId == fileCategoryId);
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
			IQueryable<EFileCategory> query = from fileCategory in _dataContext.FileCategories
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
		/// <param name="onlyActive">If true=>fetch only active.</param>
		/// <returns>A list of file-categories.</returns>
		/// <exception cref="ObjectNotFoundException">If no file-categories are found.</exception>

		public IList<FileCategory> GetAllFileCategories (bool onlyActive)
		{
			IQueryable<EFileCategory> query = from fileCategory in _dataContext.FileCategories
						orderby fileCategory.Name ascending
						select fileCategory;

			if (onlyActive) {
				query = query.AddEqualityCondition ("IsActive", true);
			}

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

		#region Internal methods

		/// <summary>
		/// Checks if a file-category exist.
		/// </summary>
		/// <param name="fileCategoryId">The file-category-id.</param>
		/// <exception cref="ObjectNotFoundException">If the file-category is not found.</exception>

		private void CheckFileCategoryExist (int fileCategoryId)
		{
			GetFileCategoryById (fileCategoryId);
		}
		
		/// <summary>
		/// Checks to see if a file-category already exist.
		/// </summary>
		/// <param name="name">The name to check.</param>
		/// <param name="id">The id to update.</param>
		/// <returns>If name exists=>true otherwise false.</returns>

		private bool CheckFileCategoryNameExist (string name, int? id)
		{
			IQueryable<EFileCategory> query = from fileCategory in _dataContext.FileCategories
			                                  where fileCategory.Name == name
			                                  select fileCategory;

			if (id != null) {
				query.AddNotEqualityCondition ("Id", (int) id);
			}

			IList<EFileCategory> tmpFileCategories = query.ToList ();

			if (!tmpFileCategories.IsEmpty ()) {
				return true;
			}

			return false;
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
