using System.Collections.Generic;

using Spinit.Wpc.Synologen.OPQ.Core.Entities;

namespace Spinit.Wpc.Synologen.OPQ.Business
{
	/// <summary>
	/// The file-category business class.
	/// Implements the class tblSynologenOPQFileCategories.
	/// </summary>
	public class BFileCategory
	{
		/// <summary>
		/// Creates a new file category.
		/// </summary>
		/// <param name="name">The name of the file-category.</param>
		/// <param name="fileCategory">The created file category.</param>
		
		public SynologenOpqFileCategory CreateFileCategory (string name)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Changes the file category.
		/// </summary>
		/// <param name="fileCategoryId">The id of the file category.</param>
		/// <param name="name">The name of the file-category.</param>
		/// <param name="fileCategory">The changed file category.</param>

		public SynologenOpqFileCategory ChangeFileCategory (int fileCategoryId, string name)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Deletes a file category.
		/// </summary>
		
		public void DeleteFileCategory (int fileCategoryId)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Gets a specific file-category.
		/// </summary>
		/// <param name="fileCategoryId">The id of the file-category.</param>
		/// <param name="fileCategory">The file-category</param>

		public SynologenOpqFileCategory GetFileCategory (int fileCategoryId)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Gets a list of file categories.
		/// </summary>
		/// <param name="onlyActive">If true=&gt;fetches only active categories.</param>
		/// <param name="fileCategories">A list of file-categories.</param>

		public List<SynologenOpqFileCategory> GetFileCategories (bool onlyActive)
		{
			throw new System.NotImplementedException ();
		}
	}
}
