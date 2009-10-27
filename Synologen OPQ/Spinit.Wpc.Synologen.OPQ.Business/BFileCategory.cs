using System.Collections.Generic;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;

namespace Spinit.Wpc.Synologen.OPQ.Business
{
	/// <summary>
	/// The file-category business class.
	/// Implements the class SynologenOPQFileCategories.
	/// </summary>
	
	public class BFileCategory
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="context">The context,</param>
	
		public BFileCategory (Context context)
		{
			Context = context;
		}

		/// <summary>
		/// Creates a new file category.
		/// </summary>
		/// <param name="name">The name of the file-category.</param>
		
		public FileCategory CreateFileCategory (string name)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Changes the file category.
		/// </summary>
		/// <param name="fileCategoryId">The id of the file category.</param>
		/// <param name="name">The name of the file-category.</param>

		public FileCategory ChangeFileCategory (int fileCategoryId, string name)
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

		public FileCategory GetFileCategory (int fileCategoryId)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Gets a list of file categories.
		/// </summary>
		/// <param name="onlyActive">If true=&gt;fetches only active categories.</param>

		public List<FileCategory> GetFileCategories (bool onlyActive)
		{
			throw new System.NotImplementedException ();
		}
		
		/// <summary>
		/// Gets or sets the context.
		/// </summary>

		public Context Context { get; set; }
	}
}
