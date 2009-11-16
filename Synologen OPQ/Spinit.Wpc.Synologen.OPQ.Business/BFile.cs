using System.Collections.Generic;

using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.OPQ.Core.Exceptions;
using Spinit.Wpc.Synologen.OPQ.Data;

namespace Spinit.Wpc.Synologen.OPQ.Business
{
	/// <summary>
	/// The file business class.
	/// Implements the class tblSynologenOPQFiles.
	/// </summary>
	public class BFile
	{
		private readonly Context _context;
		private readonly Configuration _configuration;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="context">The context.</param>
		
		public BFile (Context context)
		{
			_context = context;
			_configuration = Configuration.GetConfiguration (_context);
		}

		#region File Category

		/// <summary>
		/// Creates a new file category.
		/// </summary>
		/// <param name="name">The name of the file-category.</param>

		public FileCategory CreateFileCategory (string name)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.File.Insert (
					new FileCategory 
					{
						Name = name
					});
				synologenRepository.SubmitChanges ();

				return synologenRepository.File.GetInsertedFileCategory ();
			}
		}

		/// <summary>
		/// Changes the file category.
		/// </summary>
		/// <param name="fileCategoryId">The id of the file category.</param>
		/// <param name="name">The name of the file-category.</param>

		public FileCategory ChangeFileCategory (int fileCategoryId, string name)
		{
			FileCategory fileCategory = GetFileCategory (fileCategoryId);
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {

				
				fileCategory.Name = name;
				synologenRepository.File.Update (fileCategory);
				synologenRepository.SubmitChanges ();

				return synologenRepository.File.GetFileCategoryById (fileCategoryId);
			}
		}

		/// <summary>
		/// Deletes a file category.
		/// </summary>
		/// <param name="fileCategoryId">The file-category-id.</param>
		/// <param name="removeCompletely">If true=>removes a file-category completely.</param>

		public void DeleteFileCategory (int fileCategoryId, bool removeCompletely)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				if (removeCompletely) {
					synologenRepository.File.Delete (
						new FileCategory
						{
							Id = fileCategoryId,
						});
				}
				else {
					synologenRepository.File.DeactivateFileCategory (fileCategoryId);
				}
				synologenRepository.SubmitChanges ();
			}
		}

		/// <summary>
		/// Undeletes a file-category.
		/// </summary>
		/// <param name="fileCategoryId">The file-category-id.</param>

		public void UnDeleteFileCategory (int fileCategoryId)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.File.ReactivateFileCategory (fileCategoryId);
				synologenRepository.SubmitChanges ();
			}
		}

		/// <summary>
		/// Gets a specific file-category.
		/// </summary>
		/// <param name="fileCategoryId">The id of the file-category.</param>

		public FileCategory GetFileCategory (int fileCategoryId)
		{
			using (
				WpcSynologenRepository synologenRepository 
					= WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {
				return synologenRepository.File.GetFileCategoryById (fileCategoryId);
			}
		}

		/// <summary>
		/// Gets a list of all file categories.
		/// </summary>
		/// <param name="onlyActive">If true=&gt;fetches only active categories.</param>

		public IList<FileCategory> GetFileCategories (bool onlyActive)
		{
			using (
				WpcSynologenRepository synologenRepository 
					= WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {
				return synologenRepository.File.GetAllFileCategories (onlyActive);
			}
		}

		#endregion

		#region File

		/// <summary>
		/// Creates a new file.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		/// <param name="shopId">The id of the shop.</param>
		/// <param name="cncId">The id of the concern.</param>
		/// <param name="baseFileId">The base-file-id.</param>
		/// <param name="fileCategory">The file-category.</param>
		
		public File CreateFile (int nodeId, int? shopId, int? cncId, int baseFileId, FileCategory fileCategory)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.File.Insert (
					new File
					{
						FleCatId = (fileCategory == null) ? null : (int?) fileCategory.Id,
						FleId = baseFileId,
						NdeId = nodeId,
						ShpId = shopId,
						CncId = cncId
					});
				synologenRepository.SubmitChanges ();

				return synologenRepository.File.GetInsertedFile ();
			}
		}

		/// <summary>
		/// Creates a new file.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		/// <param name="shopId">The id of the shop.</param>
		/// <param name="cncId">The id of the concern.</param>
		/// <param name="baseFileId">The base-file-id.</param>
		/// <param name="fileCategoryId">The file-category-id.</param>

		public File CreateFile (int nodeId, int? shopId, int? cncId, int baseFileId, int fileCategoryId)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.File.Insert (
					new File
					{
						FleCatId = fileCategoryId,
						FleId = baseFileId,
						NdeId = nodeId,
						ShpId = shopId,
						CncId = cncId
					});
				synologenRepository.SubmitChanges ();

				return synologenRepository.File.GetInsertedFile ();
			}
		}

		/// <summary>
		/// Changes a file.
		/// </summary>
		/// <param name="fileId">The id of the file.</param>
		/// <param name="baseFileId">The base-file-id.</param>
		
		public File ChangeFile (int fileId, int baseFileId)
		{
			File file = GetFile (fileId, false);
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				
				file.FleId = baseFileId;
				synologenRepository.File.Update (file);
				synologenRepository.SubmitChanges ();

				return synologenRepository.File.GetFileById (fileId);
			}
		}

		/// <summary>
		/// Deletes a file.
		/// </summary>
		/// <param name="fileId">The id of the file.</param>
		/// <param name="removeCompletely">If true=>removes a document completely</param>

		public void DeleteFile (int fileId, bool removeCompletely)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				if (removeCompletely) {
					synologenRepository.File.Delete (
						new File
						{
							Id = fileId
						});
				}
				else {
					synologenRepository.File.DeactivateFile (fileId);
				}
				synologenRepository.SubmitChanges ();
			}
		}

		/// <summary>
		/// Undeletes a file.
		/// </summary>
		/// <param name="fileId">The file-id.</param>

		public void UnDeleteFile (int fileId)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.File.ReactivateFile (fileId);
				synologenRepository.SubmitChanges ();
			}
		}

		/// <summary>
		/// Publish a file.
		/// </summary>
		/// <param name="fileId">The id of the file.</param>
		
		public void Publish (int fileId)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.File.ApproveFile (fileId);
				synologenRepository.SubmitChanges ();
			}
		}

		/// <summary>
		/// Locks a file.
		/// </summary>
		/// <param name="fileId">The id of the file.</param>
		
		public void Lock (int fileId)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.File.CheckOutFile (fileId);
				synologenRepository.SubmitChanges ();
			}
		}

		/// <summary>
		/// Unlocks the file.
		/// </summary>
		/// <param name="fileId">The id of the file.</param>
		
		public void Unlock (int fileId)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.File.CheckInFile (fileId);
				synologenRepository.SubmitChanges ();
			}
		}

		/// <summary>
		/// Moves a file up or down in the list.
		/// </summary>
		/// <param name="moveAction">The move-action.</param>
		/// <param name="source">The id of the file.</param>
		
		public void MoveFile (NodeMoveActions moveAction, int source)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				File sSource = synologenRepository.File.GetFileById (source);
				switch (moveAction) {
					case NodeMoveActions.MoveUp:
						synologenRepository.File.MoveFile (new File {Id = source, Order = sSource.Order + 1});
						break;

					case NodeMoveActions.MoveDown:
						synologenRepository.Node.MoveNode (new Node {Id = source, Order = sSource.Order - 1});
						break;

					default:
						throw new FileException ("Not valid move operation.", FileErrors.MoveToForbidden);
				}

				synologenRepository.SubmitChanges ();
			}
		}
		
		/// <summary>
		/// Gets a file.
		/// </summary>
		/// <param name="fileId">The id of the file.</param>
		/// <param name="fillObjects">Fill all objects.</param>

		public File GetFile (int fileId, bool fillObjects)
		{
			using (
				WpcSynologenRepository synologenRepository 
					= WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {
				synologenRepository.AddDataLoadOptions<File> (f => f.BaseFile);

				if (fillObjects) {
					synologenRepository.AddDataLoadOptions<File> (f => f.FileCategory);
					synologenRepository.AddDataLoadOptions<File> (f => f.Node);
					synologenRepository.AddDataLoadOptions<File> (f => f.CreatedBy);
					synologenRepository.AddDataLoadOptions<File> (f => f.ChangedBy);
					synologenRepository.AddDataLoadOptions<File> (f => f.ApprovedBy);
					synologenRepository.AddDataLoadOptions<File> (f => f.LockedBy);
				}

				synologenRepository.SetDataLoadOptions ();

				return synologenRepository.File.GetFileById (fileId);
			}
		}

		/// <summary>
		/// Gets a list of files.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		/// <param name="shopId">The id of the shop.</param>
		/// <param name="cncId">The concern-id.</param>
		/// <param name="fileCategoryId">The category-id.</param>
		/// <param name="onlyActive">If true=&gt;fetch only active files.</param>
		/// <param name="onlyApproved">If true=>fetch only approved documents.</param>
		/// <param name="fillObjects">Fill all objects.</param>

		public IList<File> GetFiles (int? nodeId, int? shopId, int? cncId, int? fileCategoryId, bool onlyActive, bool onlyApproved, bool fillObjects)
		{
			using (
				WpcSynologenRepository synologenRepository
					= WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {

				synologenRepository.AddDataLoadOptions<File> (f => f.BaseFile);

				if (fillObjects) {
					synologenRepository.AddDataLoadOptions<File> (f => f.FileCategory);
					synologenRepository.AddDataLoadOptions<File> (f => f.Node);
					synologenRepository.AddDataLoadOptions<File> (f => f.CreatedBy);
					synologenRepository.AddDataLoadOptions<File> (f => f.ChangedBy);
					synologenRepository.AddDataLoadOptions<File> (f => f.ApprovedBy);
					synologenRepository.AddDataLoadOptions<File> (f => f.LockedBy);
				}

				synologenRepository.SetDataLoadOptions ();

				if (nodeId != null) {
					if ((shopId != null) || (cncId != null)) {
						return synologenRepository.File.GetFilesByNodeId (
							(int) nodeId,
							shopId,
							cncId,
							fileCategoryId,
							onlyActive,
							onlyApproved);
					}
					return synologenRepository.File.GetFilesByNodeId ((int) nodeId, fileCategoryId, onlyActive, onlyApproved);
				}

				if ((shopId != null) || (cncId != null)) {
					if (fileCategoryId != null) {
						return synologenRepository.File.GetFilesByShopId (shopId, cncId, fileCategoryId, onlyActive, onlyApproved);
					}
					return synologenRepository.File.GetFilesByShopId (shopId, cncId, onlyActive, onlyApproved);
				}

				if (fileCategoryId != null) {
					return synologenRepository.File.GetFilesByCategoryId (fileCategoryId, onlyActive, onlyApproved);
				}

				return synologenRepository.File.GetAllFiles (onlyActive, onlyApproved);
			}
		}

		#endregion
	}
}
