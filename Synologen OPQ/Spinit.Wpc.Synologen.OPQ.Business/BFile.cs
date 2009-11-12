using System.Collections.Generic;

using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
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
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.File.Update (
					new FileCategory
					{
						Id = fileCategoryId,
						Name = name
					});
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
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
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
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
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
						FleCatId = fileCategory.Id,
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
		/// <param name="fileCategory">The file-categories.</param>
		
		public File ChangeFile (int fileId, int baseFileId, FileCategory fileCategory)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Deletes a file.
		/// </summary>
		/// <param name="fileId">The id of the file.</param>
		
		public void DeleteFile (int fileId)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Publish a file.
		/// </summary>
		/// <param name="fileId">The id of the file.</param>
		
		public void Publish (int fileId)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Locks a file.
		/// </summary>
		/// <param name="fileId">The id of the file.</param>
		
		public void Lock (int fileId)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Unlocks the file.
		/// </summary>
		/// <param name="fileId">The id of the file.</param>
		
		public void Unlock (int fileId)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Gets a file.
		/// </summary>
		/// <param name="fileId">The id of the file.</param>

		public File GetFile (int fileId)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Gets a list of files.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		/// <param name="shopId">The id of the shop.</param>
		/// <param name="fileCategoryId">The category-id.</param>
		/// <param name="onlyActive">If true=&gt;fetch only active files.</param>

		public List<File> GetFiles (int? nodeId, int? shopId, int? fileCategoryId, bool onlyActive)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Moves a file up or down in the list.
		/// </summary>
		/// <param name="fileId">The id of the file.</param>
		/// <param name="moveAction">The action.</param>
		
		public void MoveFile (int fileId, NodeMoveActions moveAction)
		{
			throw new System.NotImplementedException ();
		}

		#endregion
	}
}
