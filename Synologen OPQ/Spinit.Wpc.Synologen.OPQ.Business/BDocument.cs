using System;
using System.Collections.Generic;

using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.OPQ.Core.Exceptions;
using Spinit.Wpc.Synologen.OPQ.Data;

namespace Spinit.Wpc.Synologen.OPQ.Business
{
	/// <summary>
	/// The documentContent business class.
	/// Implements the classes SynologenOPQDocuments, SynologenOPQDocumentsHistory and SynologenOPQDocumentTypes.
	/// </summary>
	
	public class BDocument
	{
		private readonly Context _context;
		private readonly Configuration _configuration;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="context">The context.</param>
	
		public BDocument (Context context)
		{
			_context = context;
			_configuration = Configuration.GetConfiguration (_context);
		}

		#region Document Type

		/// <summary>
		/// Fetches a list with all document-types.
		/// </summary>
		/// <returns>A list with document-types.</returns>

		public IList<DocumentType> GetDocumentTypes ()
		{
			using (
				WpcSynologenRepository synologenRepository 
					= WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {

				return synologenRepository.Document.GetAllDocumentTypes ();
			}
		}

		#endregion

		#region Document

		/// <summary>
		/// Creates a new document.
		/// </summary>
		/// <param name="nodeId">The node-id to connect the document to.</param>
		/// <param name="shopId">The shop-id to connect the document to.</param>
		/// <param name="documentTypeId">The document-type.</param>
		/// <param name="documentContent">The document content.</param>

		public Document CreateDocument (int nodeId, int? shopId, DocumentTypes documentTypeId, string documentContent)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.Document.Insert (
					new Document
					{
						NdeId = nodeId, 
						ShpId = shopId, 
						DocTpeId = documentTypeId, 
						DocumentContent = documentContent
					});
				synologenRepository.SubmitChanges ();

				return synologenRepository.Document.GetInsertedDocument ();
			}
		}

		/// <summary>
		/// Changes a document.
		/// </summary>
		/// <param name="documentId">The id of the document.</param>
		/// <param name="documentContent">The document content.</param>

		public Document ChangeDocument (int documentId, string documentContent)
		{
			Document document = GetDocument (documentId, false);

			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				document.DocumentContent = documentContent;
				synologenRepository.Document.Update (document);
				synologenRepository.SubmitChanges ();

				return synologenRepository.Document.GetDocumentById (documentId);
			}
		}

		/// <summary>
		/// Deletes a document.
		/// </summary>
		/// <param name="documentId">The id of the document.</param>
		/// <param name="removeCompletely">If true=>removes a document completely</param>
		
		public void DeleteDocument (int documentId, bool removeCompletely)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				if (removeCompletely) {
					synologenRepository.Document.Delete (
						new Document
						{
							Id = documentId
						});
				}
				else {
					synologenRepository.Document.DeactivateDocument (documentId);
				}
				synologenRepository.SubmitChanges ();
			}
		}

		/// <summary>
		/// Undeletes a document.
		/// </summary>
		/// <param name="documentId">The document-id.</param>

		public void UnDeleteDocument (int documentId)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.Document.ReactivateDocument (documentId);
				synologenRepository.SubmitChanges ();
			}
		}

		/// <summary>
		/// UnPublish the document.
		/// </summary>
		/// <param name="documentId">The id of the document.</param>

		public void UnPublish(int documentId)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository(_configuration, null, _context)
				)
			{
				synologenRepository.Document.UnApproveDocument(documentId);
				synologenRepository.SubmitChanges();
			}
		}


		/// <summary>
		/// Publish the document.
		/// </summary>
		/// <param name="documentId">The id of the document.</param>
		
		public void Publish (int documentId)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.Document.ApproveDocument (documentId);
				synologenRepository.SubmitChanges ();
			}
		}

		/// <summary>
		/// Locks the document.
		/// </summary>
		/// <param name="documentId">The id of the document.</param>
		
		public void Lock (int documentId)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.Document.CheckOutDocument (documentId);
				synologenRepository.SubmitChanges ();
			}
		}

		/// <summary>
		/// Unlocks the document.
		/// </summary>
		/// <param name="documentId">The id of the document.</param>
		
		public void UnLock (int documentId)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				synologenRepository.Document.CheckInDocument (documentId);
				synologenRepository.SubmitChanges ();
			}
		}

		/// <summary>
		/// Gets a specified document
		/// </summary>
		/// <param name="documentId">The id of the document.</param>
		/// <param name="fillObjects">If true=>fill-objects</param>

		public Document GetDocument (int documentId, bool fillObjects)
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {
				if (fillObjects) {
					synologenRepository.AddDataLoadOptions<Document> (d => d.DocumentHistories);
					synologenRepository.AddDataLoadOptions<Document> (d => d.DocumentType);
					synologenRepository.AddDataLoadOptions<Document> (d => d.Node);
					synologenRepository.AddDataLoadOptions<Document> (d => d.CreatedBy);
					synologenRepository.AddDataLoadOptions<Document> (d => d.ChangedBy);
					synologenRepository.AddDataLoadOptions<Document> (d => d.ApprovedBy);
					synologenRepository.AddDataLoadOptions<Document> (d => d.LockedBy);

					synologenRepository.SetDataLoadOptions ();
				}

				return synologenRepository.Document.GetDocumentById (documentId);
			}
		}

		/// <summary>
		/// Gets list of documents.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		/// <param name="shopId">The id of the shop.</param>
		/// <param name="cncId">The concern-id.</param>
		/// <param name="documentType">The type of the document.</param>
		/// <param name="searchText">Text to search for.</param>
		/// <param name="onlyActive">If true=>fetch only active documents.</param>
		/// <param name="onlyApproved">If true=>fetch only approved documents.</param>
		/// <param name="fillObjects">If true=>fill-objects.</param>

		public IList<Document> GetDocuments (
			int? nodeId, 
			int? shopId,
			int? cncId,
			DocumentTypes? documentType, 
			string searchText, 
			bool onlyActive,
			bool onlyApproved,
			bool fillObjects)
		{
			using (
				WpcSynologenRepository synologenRepository 
					= WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {
				if (fillObjects) {
					synologenRepository.AddDataLoadOptions<Document> (d => d.DocumentHistories);
					synologenRepository.AddDataLoadOptions<Document> (d => d.DocumentType);
					synologenRepository.AddDataLoadOptions<Document> (d => d.Node);
					synologenRepository.AddDataLoadOptions<Document> (d => d.CreatedBy);
					synologenRepository.AddDataLoadOptions<Document> (d => d.ChangedBy);
					synologenRepository.AddDataLoadOptions<Document> (d => d.ApprovedBy);
					synologenRepository.AddDataLoadOptions<Document> (d => d.LockedBy);

					synologenRepository.SetDataLoadOptions ();
				}
				
				if ((nodeId != null) && ((shopId != null) || (cncId != null)) && (documentType != null)) {
					return synologenRepository.Document.GetDocumentsByNodeId (
						(int) nodeId, 
						shopId, 
						cncId,
						(DocumentTypes) documentType, 
						onlyActive,
						onlyApproved);
				}
				
				if ((nodeId != null) && (documentType != null)) {
					return synologenRepository.Document.GetDocumentsByNodeId (
						(int) nodeId,
						(DocumentTypes) documentType,
						onlyActive,
						onlyApproved);
				}

				if (nodeId != null) {
					return synologenRepository.Document.GetDocumentsByNodeId ((int) nodeId, onlyActive, onlyApproved);
				}

				if (searchText != null) {
					return synologenRepository.Document.GetDocumentsByText (searchText, onlyActive, onlyApproved);
				}

				return synologenRepository.Document.GetAllDocuments (onlyActive, onlyApproved);
			}
		}

		/// <summary>
		/// Gets list of documents.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		/// <param name="shopId">The id of the shop.</param>
		/// <param name="cncId">The id of the concern.</param>
		/// <param name="documentType">The type of the document.</param>
		/// <param name="searchText">Text to search for.</param>
		/// <param name="fillObjects">If true=>fill-objects.</param>

		public IList<Document> GetActiveDocuments (
			int? nodeId,
			int? shopId,
			int? cncId,
			DocumentTypes? documentType,
			string searchText,
			bool fillObjects)
		{
			using (
				WpcSynologenRepository synologenRepository
					= WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {
				IList<Document> documents;

				if (nodeId != null) {
					documents = synologenRepository.Document.GetDocumentsByNodeId (
						(int) nodeId,
						shopId,
						cncId,
						(DocumentTypes) documentType,
						true,
						false);
				}
				else if (searchText != null) {
					documents = synologenRepository.Document.GetDocumentsByText (searchText, true, false);
				}
				else {
					documents = synologenRepository.Document.GetAllDocuments (true, false);
				}

				IList<Document> retDocuments = new List<Document> ();
				if ((documents != null) && (documents.Count > 0)) {
					foreach (Document document in documents) {
						Document retDocument = GetActiveDocument (document.Id, fillObjects);

						if (retDocument != null) {
							retDocuments.Add (retDocument);
						}
					}
				}

				return retDocuments;
			}
		}

		/// <summary>
		/// Gets the active document.
		/// </summary>
		/// <param name="id">The id of the document.</param>
		/// <param name="fillObjects">If true=>fill-objects.</param>

		public Document GetActiveDocument (int id, bool fillObjects)
		{
			using (
				WpcSynologenRepository synologenRepository
					= WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {
				if (fillObjects) {
					synologenRepository.AddDataLoadOptions<Document> (d => d.DocumentHistories);
					synologenRepository.AddDataLoadOptions<Document> (d => d.DocumentType);
					synologenRepository.AddDataLoadOptions<Document> (d => d.Node);
					synologenRepository.AddDataLoadOptions<Document> (d => d.CreatedBy);
					synologenRepository.AddDataLoadOptions<Document> (d => d.ChangedBy);
					synologenRepository.AddDataLoadOptions<Document> (d => d.ApprovedBy);
					synologenRepository.AddDataLoadOptions<Document> (d => d.LockedBy);

					synologenRepository.SetDataLoadOptions ();
				}
				Document document = null;
				try {
					DocumentView docView = synologenRepository.Document.GetActiveDocument (id);

					document = synologenRepository.Document.GetDocumentById (docView.Id);

					if (docView.HistoryDate != null) {
						DocumentHistory documentHistory
							= synologenRepository.Document.GetDocumentHistoryById (docView.Id, (DateTime) docView.HistoryDate);

						document.DocumentContent = documentHistory.DocumentContent;

						if (documentHistory.ChangedById != null) {
							document.ChangedById = documentHistory.ChangedById;
							document.ChangedByName = documentHistory.ChangedByName;
							document.ChangedDate = documentHistory.ChangedDate;
							document.ChangedBy = synologenRepository.ExternalObjectsManager.GetUserById ((int) documentHistory.ChangedById);
						}
						else {
							document.ChangedById = null;
							document.ChangedByName = null;
							document.ChangedDate = null;
							document.ChangedBy = null;
						}

						document.ApprovedById = documentHistory.ApprovedById;
						document.ApprovedByName = documentHistory.ApprovedByName;
						document.ApprovedDate = documentHistory.ApprovedDate;
						document.ApprovedBy = synologenRepository.ExternalObjectsManager.GetUserById ((int) documentHistory.ApprovedById);
					}
				}
				catch (ObjectNotFoundException e) {
					if ((((ObjectNotFoundErrors) e.ErrorCode) == ObjectNotFoundErrors.DocumentNotFound)
					    || (((ObjectNotFoundErrors) e.ErrorCode) == ObjectNotFoundErrors.DocumentHistoryNotFound)
					    || (((ObjectNotFoundErrors) e.ErrorCode) == ObjectNotFoundErrors.DocumentViewNotFound)) {
						return null;
					}

				}

				return document;
			}
		}

		#endregion

		#region Document History

		/// <summary>
		/// Gets a specific document-history.
		/// </summary>
		/// <param name="documentId">The document-id.</param>
		/// <param name="historyDate">The history-date.</param>
		/// <param name="fillObjects">If true=>fill objects.</param>
		/// <returns>A document-history.</returns>

		public DocumentHistory GetDocumentHistory (int documentId, DateTime historyDate, bool fillObjects)
		{
			using (
				WpcSynologenRepository synologenRepository
					= WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {
				if (fillObjects) {
					synologenRepository.AddDataLoadOptions<DocumentHistory> (d => d.Document);
					synologenRepository.AddDataLoadOptions<DocumentHistory> (d => d.HistoryIdUser);

					synologenRepository.SetDataLoadOptions ();
				}

				return synologenRepository.Document.GetDocumentHistoryById (documentId, historyDate);
			}
		}

		/// <summary>
		/// Gets a list of document-histories for a document.
		/// </summary>
		/// <param name="documentId">The document-id.</param>
		/// <param name="fillObjects">If true=>fill objects.</param>
		/// <returns>A list of document-histories.</returns>

		public IList<DocumentHistory> GetDocumentHistories (int documentId, bool fillObjects)
		{
			using (
				WpcSynologenRepository synologenRepository
					= WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {
				if (fillObjects) {
					synologenRepository.AddDataLoadOptions<DocumentHistory> (d => d.Document);
					synologenRepository.AddDataLoadOptions<DocumentHistory> (d => d.HistoryIdUser);

					synologenRepository.SetDataLoadOptions ();
				}

				return synologenRepository.Document.GetAllDocumentHistoriesByDocumentId (documentId);
			}
		}

		#endregion
	}
}
