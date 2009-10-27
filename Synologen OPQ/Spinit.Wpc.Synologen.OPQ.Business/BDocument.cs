using System.Collections.Generic;

using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;

namespace Spinit.Wpc.Synologen.OPQ.Business
{
	/// <summary>
	/// The documentContent business class.
	/// Implements the classes SynologenOPQDocuments, SynologenOPQDocumentsHistory and SynologenOPQDocumentTypes.
	/// </summary>
	
	public class BDocument
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="context">The context,</param>
	
		public BDocument (Context context)
		{
			Context = context;
		}
		
		/// <summary>
		/// Creates a new document.
		/// </summary>
		/// <param name="nodeId">The node-id to connect the document to.</param>
		/// <param name="shopId">The shop-id to connect the document to.</param>
		/// <param name="documentTypeId">The document-type.</param>
		/// <param name="documentContent">The document content.</param>

	
		public Document CreateDocument (int nodeId, int? shopId, DocumentTypes documentTypeId, string documentContent)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Changes a document.
		/// </summary>
		/// <param name="documentId">The id of the document.</param>
		/// <param name="documentContent">The document content.</param>

		public Document ChangeDocument (int documentId, string documentContent)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Deletes a document
		/// </summary>
		/// <param name="documentId">The id of the document.</param>
		
		public void DeleteDocument (int documentId)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Publish the document.
		/// </summary>
		/// <param name="documentId">The id of the document.</param>
		
		public void Publish (int documentId)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Locks the document.
		/// </summary>
		/// <param name="documentId">The id of the document.</param>
		
		public void Lock (int documentId)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Unlocks the document.
		/// </summary>
		/// <param name="documentId">The id of the document.</param>
		
		public void UnLock (int documentId)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Gets a specified document
		/// </summary>
		/// <param name="documentId">The id of the document.</param>

		public Document GetDocument (int documentId)
		{
			throw new System.NotImplementedException ();
		}

		/// <summary>
		/// Gets list of ducuments.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		/// <param name="shopId">The id of the shop.</param>
		/// <param name="documentType">The type of the document.</param>
		/// <param name="searchText">Text to search for.</param>
		/// <param name="onlyActive">If true=&gt;fetch only active documents.</param>

		public List<Document> GetDocuments (int? nodeId, int? shopId, DocumentTypes? documentType, string searchText, bool onlyActive)
		{
			throw new System.NotImplementedException ();
		}
	
		/// <summary>
		/// Gets or sets the context.
		/// </summary>

		public Context Context { get; set; }
	}
}
