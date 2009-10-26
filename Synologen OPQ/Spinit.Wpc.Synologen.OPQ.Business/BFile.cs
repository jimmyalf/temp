using System.Collections.Generic;

using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;

namespace Spinit.Wpc.Synologen.OPQ.Business
{
	/// <summary>
	/// The file business class.
	/// Implements the class tblSynologenOPQFiles.
	/// </summary>
	public class BFile
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="context">The context.</param>
		public BFile (Context context)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Gets or sets the context.
		/// </summary>
		public Context Context
		{
			get
			{
				throw new System.NotImplementedException ();
			}
			set
			{
			}
		}

		/// <summary>
		/// Creates a new file.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		/// <param name="shopId">The id of the shop.</param>
		/// <param name="baseFileId">The base-file-id.</param>
		/// <param name="fileCategory">The file-categories.</param>
		/// <param name="file">The created file.</param>
		public SynologenOpqFile CreateFile (int nodeId, int? shopId, int baseFileId, SynologenOpqFileCategory fileCategory)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Changes a file.
		/// </summary>
		/// <param name="fileId">The id of the file.</param>
		/// <param name="baseFileId">The base-file-id.</param>
		/// <param name="fileCategory">The file-categories.</param>
		/// <param name="file">The changed file.</param>
		public SynologenOpqFile ChangeFile (int fileId, int baseFileId, SynologenOpqFileCategory fileCategory)
		{
			string tmp = "dbApa";

			tmp = tmp.StartsWith ("db") ? tmp.Remove (0, 2) : tmp;

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
		/// <param name="fileFill">The file-fill object.</param>
		/// <param name="File">The fetched file.</param>
		public SynologenOpqFile GetFile (int fileId)
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
		/// <param name="fileFill">The file-fill object.</param>
		/// <param name="files">A list of files.</param>
		public List<SynologenOpqFile> GetFiles (int? nodeId, int? shopId, int? fileCategoryId, bool onlyActive)
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
	}
}
