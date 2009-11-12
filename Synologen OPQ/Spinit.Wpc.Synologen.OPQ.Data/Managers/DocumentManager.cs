using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;

using Spinit.Data.Linq;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.Opq.Core.Exceptions;
using Spinit.Wpc.Synologen.OPQ.Data;
using Spinit.Wpc.Synologen.OPQ.Data.Entities;

namespace Spinit.Wpc.Synologen.Opq.Data.Managers
{
	public class DocumentManager : EntityManager<WpcSynologenRepository>
	{
		private readonly WpcSynologenDataContext _dataContext;

		private EDocument _insertedDocument;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="manager">The repository-manager.</param>

		public DocumentManager (WpcSynologenRepository manager) : base (manager)
		{
			_dataContext = (WpcSynologenDataContext) Manager.Context;
		}

		#region Create

		#region Create Document

		/// <summary>
		/// Inserts a document.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If user, shop or concern does not exist.</exception>

		private void Insert (EDocument document)
		{
			document.CreatedById = Manager.WebContext.UserId ?? 0;
			document.CreatedByName = Manager.WebContext.UserName;
			document.CreatedDate = DateTime.Now;

			if ((document.CreatedById == 0) || (document.CreatedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}
			
			Manager.ExternalObjectsManager.CheckUserExist (document.CreatedById);

			if (document.ShpId != null) {
				Manager.ExternalObjectsManager.CheckShopExist ((int) document.ShpId);
			}

			if (document.CncId != null) {
				Manager.ExternalObjectsManager.CheckConcernExist ((int) document.CncId);
			}

			document.IsActive = true;

			_insertedDocument = document;

			_dataContext.Documents.InsertOnSubmit (document);
		}

		/// <summary>
		/// Inserts a document.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If user, shop or concern does not exist.</exception>

		public void Insert (Document document)
		{
			Insert (EDocument.Convert (document));
		}

		/// <summary>
		/// Returns the inserted document.
		/// </summary>
		/// <returns>The inserted document.</returns>

		public Document GetInsertedDocument ()
		{
			return EDocument.Convert (_insertedDocument);
		}

		#endregion

		#endregion

		#region Change

		#region Change Document

		/// <summary>
		/// Updates a document.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the document is not found.</exception>
		/// <exception cref="DocumentException">
		/// 1. If document is locked by other user. 
		/// 2. If node, shop, concern or document-type is changed.</exception>

		private void Update (EDocument document)
		{
			EDocument oldDocument = _dataContext.Documents.Single (d => d.Id == document.Id);

			if (oldDocument == null) {
				throw new ObjectNotFoundException (
					"Document not found.",
					ObjectNotFoundErrors.DocumentNotFound);
			}
		
			if ((oldDocument.LockedById != null) && (oldDocument.LockedById != Manager.WebContext.UserId)) {
				throw new DocumentException ("Document locked by another user.", DocumentErrors.DocumentLockedByOtherUser);
			}
		
			oldDocument.ChangedById = Manager.WebContext.UserId ?? 0;
			oldDocument.ChangedByName = Manager.WebContext.UserName;
			oldDocument.ChangedDate = DateTime.Now;

			if ((oldDocument.ChangedById == 0) || (oldDocument.ChangedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldDocument.ChangedById);

			if (oldDocument.NdeId != document.NdeId) {
				throw new DocumentException ("Node change not allowed", DocumentErrors.CangeOfNodeNotAllowed);
			}

			if (oldDocument.ShpId != document.ShpId) {
				throw new DocumentException ("Shop change not allowed", DocumentErrors.ChangeOfShopNotAllowed);
			}

			if (oldDocument.CncId != document.CncId) {
				throw new DocumentException ("Concern change not allowed", DocumentErrors.ChangeOfConcernNotAllowed);
			}

			if (oldDocument.DocTpeId != document.DocTpeId) {
				throw new DocumentException ("Document-type change not allowed", DocumentErrors.ChangeOfDocumentTypeNotAllowed);
			}

			if ((document.DocumentContent != null) && !oldDocument.DocumentContent.Equals (document.DocumentContent)) {
				oldDocument.DocumentContent = document.DocumentContent;
			}
		}

		/// <summary>
		/// Updates a document.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the document is not found.</exception>
		/// <exception cref="DocumentException">
		/// 1. If document is locked by other user. 
		/// 2. If node, shop, concern or document-type is changed.</exception>

		public void Update (Document document)
		{
			Update (EDocument.Convert (document));
		}

		#endregion

		#region Deactive & Reactivate Document

		/// <summary>
		/// Deactivates a document.
		/// </summary>
		/// <param name="documentId">The document-id.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the document is not found.</exception>
		/// <exception cref="DocumentException">If document is locked by other user.</exception>

		public void DeactivateDocument (int documentId)
		{
			EDocument oldDocument = _dataContext.Documents.Single (d => d.Id == documentId);

			if (oldDocument == null) {
				throw new ObjectNotFoundException (
					"Document not found.",
					ObjectNotFoundErrors.DocumentNotFound);
			}

			if ((oldDocument.LockedById != null) && (oldDocument.LockedById != Manager.WebContext.UserId)) {
				throw new DocumentException ("Document locked by another user.", DocumentErrors.DocumentLockedByOtherUser);
			}

			oldDocument.ChangedById = Manager.WebContext.UserId ?? 0;
			oldDocument.ChangedByName = Manager.WebContext.UserName;
			oldDocument.ChangedDate = DateTime.Now;

			if ((oldDocument.ChangedById == 0) || (oldDocument.ChangedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldDocument.ChangedById);

			oldDocument.IsActive = false;
		}

		/// <summary>
		/// Reactivates a document.
		/// </summary>
		/// <param name="documentId">The document-id.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the document is not found.</exception>
		/// <exception cref="DocumentException">If document is locked by other user.</exception>

		public void ReactivateDocument (int documentId)
		{
			EDocument oldDocument = _dataContext.Documents.Single (d => d.Id == documentId);

			if (oldDocument == null) {
				throw new ObjectNotFoundException (
					"Document not found.",
					ObjectNotFoundErrors.DocumentNotFound);
			}

			if ((oldDocument.LockedById != null) && (oldDocument.LockedById != Manager.WebContext.UserId)) {
				throw new DocumentException ("Document locked by another user.", DocumentErrors.DocumentLockedByOtherUser);
			}

			oldDocument.ChangedById = Manager.WebContext.UserId ?? 0;
			oldDocument.ChangedByName = Manager.WebContext.UserName;
			oldDocument.ChangedDate = DateTime.Now;

			if ((oldDocument.ChangedById == 0) || (oldDocument.ChangedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldDocument.ChangedById);

			oldDocument.IsActive = true;
		}

		#endregion

		#region Approve, Check-Out and Check-In documents

		/// <summary>
		/// Approves a document.
		/// </summary>
		/// <param name="documentId">The document-id.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the document or user is not found.</exception>
		/// <exception cref="DocumentException">If document is locked by other user.</exception>

		public void ApproveDocument (int documentId)
		{
			EDocument oldDocument = _dataContext.Documents.Single (d => d.Id == documentId);

			if (oldDocument == null) {
				throw new ObjectNotFoundException (
					"Document not found.",
					ObjectNotFoundErrors.DocumentNotFound);
			}

			if ((oldDocument.LockedById != null) && (oldDocument.LockedById != Manager.WebContext.UserId)) {
				throw new DocumentException ("Document locked by another user.", DocumentErrors.DocumentLockedByOtherUser);
			}

			oldDocument.ApprovedById = Manager.WebContext.UserId ?? 0;
			oldDocument.ApprovedByName = Manager.WebContext.UserName;
			oldDocument.ApprovedDate = DateTime.Now;
			oldDocument.ChangedById = Manager.WebContext.UserId ?? 0;
			oldDocument.ChangedByName = Manager.WebContext.UserName;
			oldDocument.ChangedDate = DateTime.Now;

			if ((oldDocument.ApprovedById == 0) || (oldDocument.ApprovedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldDocument.ApprovedById);
		}

		/// <summary>
		/// Checks out a document.
		/// </summary>
		/// <param name="documentId">The document-id.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the document or user is not found.</exception>
		/// <exception cref="DocumentException">If document is locked by other user.</exception>

		public void CheckOutDocument (int documentId)
		{
			EDocument oldDocument = _dataContext.Documents.Single (d => d.Id == documentId);

			if (oldDocument == null) {
				throw new ObjectNotFoundException (
					"Document not found.",
					ObjectNotFoundErrors.DocumentNotFound);
			}

			if ((oldDocument.LockedById != null) && (oldDocument.LockedById != Manager.WebContext.UserId)) {
				throw new DocumentException ("Document locked by another user.", DocumentErrors.DocumentLockedByOtherUser);
			}

			oldDocument.LockedById = Manager.WebContext.UserId ?? 0;
			oldDocument.LockedByName = Manager.WebContext.UserName;
			oldDocument.LockedDate = DateTime.Now;
			oldDocument.ChangedById = Manager.WebContext.UserId ?? 0;
			oldDocument.ChangedByName = Manager.WebContext.UserName;
			oldDocument.ChangedDate = DateTime.Now;

			if ((oldDocument.LockedById == 0) || (oldDocument.LockedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldDocument.LockedById);
		}

		/// <summary>
		/// Checks-in a document.
		/// </summary>
		/// <param name="documentId">The document-id.</param>
		/// <exception cref="UserException">If no current-user.</exception>
		/// <exception cref="ObjectNotFoundException">If the document or user is not found.</exception>
		/// <exception cref="DocumentException">If document is locked by other user.</exception>

		public void CheckInDocument (int documentId)
		{
			EDocument oldDocument = _dataContext.Documents.Single (d => d.Id == documentId);

			if ((oldDocument.LockedById != null) && (oldDocument.LockedById != Manager.WebContext.UserId)) {
				throw new DocumentException ("Document locked by another user.", DocumentErrors.DocumentLockedByOtherUser);
			}

			if (oldDocument == null) {
				throw new ObjectNotFoundException (
					"Document not found.",
					ObjectNotFoundErrors.DocumentNotFound);
			}

			oldDocument.LockedById = null;
			oldDocument.LockedByName = null;
			oldDocument.LockedDate = null;
			oldDocument.ChangedById = Manager.WebContext.UserId ?? 0;
			oldDocument.ChangedByName = Manager.WebContext.UserName;
			oldDocument.ChangedDate = DateTime.Now;

			if ((oldDocument.ChangedById == 0) || (oldDocument.ChangedByName == null)) {
				throw new UserException ("No user found.", UserErrors.NoCurrentExist);
			}

			Manager.ExternalObjectsManager.CheckUserExist ((int) oldDocument.ChangedById);
		}

		#endregion

		#endregion

		#region Remove 

		#region Remove Document

		/// <summary>
		/// Deletes a specific document including document-histories.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <exception cref="ObjectNotFoundException">If the document is not found.</exception>

		private void Delete (EDocument document)
		{
			EDocument oldDocument = _dataContext.Documents.Single (d => d.Id == document.Id);

			if (oldDocument == null) {
				throw new ObjectNotFoundException (
					"Document not found.",
					ObjectNotFoundErrors.DocumentNotFound);
			}

			if ((oldDocument.LockedById != null) && (oldDocument.LockedById != Manager.WebContext.UserId)) {
				throw new DocumentException ("Document locked by another user.", DocumentErrors.DocumentLockedByOtherUser);
			}

			try {
				Delete (oldDocument.Id);
			}
			catch (ObjectNotFoundException e) {
				if ((ObjectNotFoundErrors) e.ErrorCode != ObjectNotFoundErrors.DocumentHistoryNotFound) {
					throw;
				}
			}

			_dataContext.Documents.DeleteOnSubmit (oldDocument);
		}

		/// <summary>
		/// Deletes a specific document including document-histories.
		/// </summary>
		/// <param name="document">The document.</param>
		/// <exception cref="ObjectNotFoundException">If the document is not found.</exception>

		public void Delete (Document document)
		{
			Delete (EDocument.Convert (document));
		}

		/// <summary>
		/// Deletes all documents for a node.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <exception cref="ObjectNotFoundException">If the document is not found.</exception>

		public void DeleteAllForNode (int nodeId)
		{
			IQueryable<EDocument> query = from document in _dataContext.Documents
						where document.NdeId == nodeId
						select document;

			IList<EDocument> documents = query.ToList ();

			foreach (EDocument oldDocument in documents) {
				if ((oldDocument.LockedById != null) && (oldDocument.LockedById != Manager.WebContext.UserId)) {
					throw new DocumentException ("Document locked by another user.", DocumentErrors.DocumentLockedByOtherUser);
				}
			}

			if (documents.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Document not found.",
					ObjectNotFoundErrors.DocumentNotFound);
			}

			// Delete histories.
			foreach (EDocument document in documents) {
				Delete (document.Id);
			}

			_dataContext.Documents.DeleteAllOnSubmit (documents);
		}

		#endregion

		#region Remove Document History

		/// <summary>
		/// Deletes a specific document-history.
		/// </summary>
		/// <param name="documentHistory">The document-history.</param>
		/// <exception cref="ObjectNotFoundException">If the document-history is not found.</exception>

		private void Delete (EDocumentHistory documentHistory)
		{
			EDocumentHistory oldDocumentHistory
				= _dataContext.DocumentHistories.Single (
					ds => ds.Id == documentHistory.Id && ds.HistoryDate == documentHistory.HistoryDate);

			if (oldDocumentHistory == null) {
				throw new ObjectNotFoundException (
					"Document-history not found.",
					ObjectNotFoundErrors.DocumentHistoryNotFound);
			}

			_dataContext.DocumentHistories.DeleteOnSubmit (oldDocumentHistory);
		}

		/// <summary>
		/// Deletes a specific document-history.
		/// </summary>
		/// <param name="documentHistory">The document-history.</param>
		/// <exception cref="ObjectNotFoundException">If the document-history is not found.</exception>

		public void Delete (DocumentHistory documentHistory)
		{
			Delete (EDocumentHistory.Convert (documentHistory));
		}

		/// <summary>
		/// Deletes all document-histories for a document.
		/// </summary>
		/// <param name="documentId">The document-id.</param>
		/// <exception cref="ObjectNotFoundException">If the document-history is not found.</exception>

		public void Delete (int documentId)
		{
			IOrderedQueryable<EDocumentHistory> query = from documentHistory in _dataContext.DocumentHistories
						where documentHistory.Id == documentId
						orderby documentHistory.HistoryDate descending 
						select documentHistory;


			IList<EDocumentHistory> documentHistories = query.ToList ();

			if (documentHistories.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Document-history not found.",
					ObjectNotFoundErrors.DocumentHistoryNotFound);
			}

			_dataContext.DocumentHistories.DeleteAllOnSubmit (documentHistories);
		}

		#endregion

		#endregion

		#region Fetch

		#region Fetch Document

		/// <summary>
		/// Fetches the document by document-id.
		/// </summary>
		/// <param name="documentId">The document-id.</param>
		/// <returns>A document.</returns>
		/// <exception cref="ObjectNotFoundException">If the document is not found.</exception>

		public Document GetDocumentById (int documentId)
		{
			IQueryable<EDocument> query = from document in _dataContext.Documents
			                              where document.Id == documentId
			                              select document;

			IList<EDocument> documents = query.ToList ();

			if (documents.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Document not found.",
					ObjectNotFoundErrors.DocumentNotFound);
			}

			return Converter (documents.First ());
		}
	
		/// <summary>
		/// Fetches a list of documents for a node.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <param name="onlyActive">If true=>fetch only active.</param>
		/// <param name="onlyApproved">If true=>fetch only approved and un-locked.</param>
		/// <returns>A list of documents.</returns>
		/// <exception cref="ObjectNotFoundException">If the document is not found.</exception>

		public IList<Document> GetDocumentsByNodeId (int nodeId, bool onlyActive, bool onlyApproved)
		{
			IOrderedQueryable<EDocument> query = from document in _dataContext.Documents
			                                     where document.NdeId == nodeId
			                                     orderby document.DocTpeId ascending , document.CreatedDate descending
			                                     select document;

			if (onlyActive) {
				query = query.AddEqualityCondition ("IsActive", true);
			}

			if (onlyApproved) {
				query = query.AddIsNotNullCondition ("ApprovedById");
				query = query.AddIsNullCondition ("LockedById");
			}
			
			Converter<EDocument, Document> converter = Converter;
			IList<Document> documents = query.ToList ().ConvertAll (converter);

			if (documents == null) {
				throw new ObjectNotFoundException (
					"Document not found.",
					ObjectNotFoundErrors.DocumentNotFound);
			}

			return documents;
		}

		/// <summary>
		/// Fetches a list of documents for a node.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <param name="documentType">The type-of-documents.</param>
		/// <param name="onlyActive">If true=>fetch only active.</param>
		/// <param name="onlyApproved">If true=>fetch only approved and un-locked.</param>
		/// <returns>A list of documents.</returns>
		/// <exception cref="ObjectNotFoundException">If the document is not found.</exception>

		public IList<Document> GetDocumentsByNodeId (int nodeId, DocumentTypes documentType, bool onlyActive, bool onlyApproved)
		{
			IOrderedQueryable<EDocument> query = from document in _dataContext.Documents
			                                     where document.NdeId == nodeId && document.DocTpeId == (int) documentType
			                                     orderby document.DocTpeId ascending , document.CreatedDate descending
			                                     select document;

			if (onlyActive) {
				query = query.AddEqualityCondition ("IsActive", true);
			}

			if (onlyApproved) {
				query = query.AddIsNotNullCondition ("ApprovedById");
				query = query.AddIsNullCondition ("LockedById");
			}

			Converter<EDocument, Document> converter = Converter;
			IList<Document> documents = query.ToList ().ConvertAll (converter);

			if (documents == null) {
				throw new ObjectNotFoundException (
					"Document not found.",
					ObjectNotFoundErrors.DocumentNotFound);
			}

			return documents;
		}

		/// <summary>
		/// Fetches a list of documents for a node.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <param name="shopId">The shop-id.</param>
		/// <param name="documentType">The type-of-documents.</param>
		/// <param name="onlyActive">If true=>fetch only active.</param>
		/// <param name="onlyApproved">If true=>fetch only approved and un-locked.</param>
		/// <returns>A list of documents.</returns>
		/// <exception cref="ObjectNotFoundException">If the document is not found.</exception>

		public IList<Document> GetDocumentsByNodeId (int nodeId, int shopId, DocumentTypes documentType, bool onlyActive, bool onlyApproved)
		{
			IOrderedQueryable<EDocument> query = from document in _dataContext.Documents
			                                     where
			                                     	document.NdeId == nodeId && document.ShpId == shopId
			                                     	&& document.DocTpeId == (int) documentType
			                                     orderby document.DocTpeId ascending , document.CreatedDate descending
			                                     select document;

			if (onlyActive) {
				query = query.AddEqualityCondition ("IsActive", true);
			}

			if (onlyApproved) {
				query = query.AddIsNotNullCondition ("ApprovedById");
				query = query.AddIsNullCondition ("LockedById");
			}

			Converter<EDocument, Document> converter = Converter;
			IList<Document> documents = query.ToList ().ConvertAll (converter);

			if (documents == null) {
				throw new ObjectNotFoundException (
					"Document not found.",
					ObjectNotFoundErrors.DocumentNotFound);
			}

			return documents;
		}

		/// <summary>
		/// Fetches a list of documents by search-text.
		/// </summary>
		/// <param name="text">The search-text.</param>
		/// <param name="onlyActive">If true=>fetch only active.</param>
		/// <param name="onlyApproved">If true=>fetch only approved and un-locked.</param>
		/// <returns>A list of documents.</returns>
		/// <exception cref="ObjectNotFoundException">If the document is not found.</exception>

		public IList<Document> GetDocumentsByText (string text, bool onlyActive, bool onlyApproved)
		{
			IOrderedQueryable<EDocument> query = from document in _dataContext.Documents
			                                     where SqlMethods.Like (document.DocumentContent, string.Concat ("%", text, "%"))
			                                     orderby document.DocTpeId ascending , document.CreatedDate descending
			                                     select document;

			if (onlyActive) {
				query = query.AddEqualityCondition ("IsActive", true);
			}

			if (onlyApproved) {
				query = query.AddIsNotNullCondition ("ApprovedById");
				query = query.AddIsNullCondition ("LockedById");
			}

			Converter<EDocument, Document> converter = Converter;
			IList<Document> documents = query.ToList ().ConvertAll (converter);

			if (documents == null) {
				throw new ObjectNotFoundException (
					"Document not found.",
					ObjectNotFoundErrors.DocumentNotFound);
			}

			return documents;
		}

		/// <summary>
		/// Fetches a list of all documents.
		/// </summary>
		/// <param name="onlyActive">If true=>fetch only active.</param>
		/// <param name="onlyApproved">If true=>fetch only approved and un-locked.</param>
		/// <returns>A list of documents.</returns>
		/// <exception cref="ObjectNotFoundException">If the document is not found.</exception>

		public IList<Document> GetAllDocuments (bool onlyActive, bool onlyApproved)
		{
			IOrderedQueryable<EDocument> query = from document in _dataContext.Documents
			                                     orderby document.DocTpeId ascending , document.CreatedDate descending
			                                     select document;

			if (onlyActive) {
				query = query.AddEqualityCondition ("IsActive", true);
			}

			if (onlyApproved) {
				query = query.AddIsNotNullCondition ("ApprovedById");
				query = query.AddIsNullCondition ("LockedById");
			}

			Converter<EDocument, Document> converter = Converter;
			IList<Document> documents = query.ToList ().ConvertAll (converter);

			if (documents == null) {
				throw new ObjectNotFoundException (
					"Document not found.",
					ObjectNotFoundErrors.DocumentNotFound);
			}

			return documents;
		}

		/// <summary>
		/// Fetches the active document from the document-view.
		/// </summary>
		/// <param name="nodeId">The node-id.</param>
		/// <param name="shopId">The shop-id.</param>
		/// <param name="concernId">The concern-id.</param>
		/// <param name="documentType">The document-type.</param>
		/// <returns>A document-view-object.</returns>

		public DocumentView GetActiveDocument (int nodeId, int? shopId, int? concernId, DocumentTypes documentType)
		{
		    IOrderedQueryable<EDocumentView> query = from documentView in _dataContext.DocumentViews
		                                             where 
														documentView.NdeId == nodeId 
														&& documentView.ApprovedById != null
														&& documentView.LockedById == null
														&& documentView.IsActive
														&& documentView.DocTpeId == (int) documentType
		                                             orderby documentView.ApprovedDate descending
		                                             select documentView;

			if (shopId != null) {
				query.AddEqualityCondition ("ShpId", shopId);
			}

			if (concernId != null) {
				query.AddEqualityCondition ("CncId", concernId);
			}

			EDocumentView docView = query.ToList ().First ();

			if (docView == null) {
				throw new ObjectNotFoundException (
					"Document View not found.",
					ObjectNotFoundErrors.DocumentViewNotFound);
			}

			return EDocumentView.Convert (docView);
		}

		#endregion

		#region Fetch Document Histories

		/// <summary>
		/// Fetches the document-history by document-id and history-date.
		/// </summary>
		/// <param name="documentId">The document-id.</param>
		/// <param name="historyDate">The history-date.</param>
		/// <returns>A document-history.</returns>
		/// <exception cref="ObjectNotFoundException">If the document-history is not found.</exception>

		public DocumentHistory GetDocumentHistoryById (int documentId, DateTime historyDate)
		{
			IQueryable<EDocumentHistory> query = from documentHistory in _dataContext.DocumentHistories
			                                     where
			                                     	documentHistory.Id == documentId && documentHistory.HistoryDate == historyDate
			                                     select documentHistory;

			IList<EDocumentHistory> documentHistories = query.ToList ();

			if (documentHistories.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Document-history not found.",
					ObjectNotFoundErrors.DocumentHistoryNotFound);
			}

			return Converter (documentHistories.First ());

		}

		/// <summary>
		/// Fetches all document-histories for a docment-id.
		/// </summary>
		/// <param name="documentId">The document-id.</param>
		/// <returns>A list of document-histories.</returns>
		/// <exception cref="ObjectNotFoundException">If no document-histories are found.</exception>

		public IList<DocumentHistory> GetAllDocumentHistoriesByDocumentId (int documentId)
		{
			IOrderedQueryable<EDocumentHistory> query = from documentHistory in _dataContext.DocumentHistories
			                                            where documentHistory.Id == documentId
			                                            orderby documentHistory.HistoryDate descending
			                                            select documentHistory;

			Converter<EDocumentHistory, DocumentHistory> converter = Converter;
			IList<DocumentHistory> documentHistories = query.ToList ().ConvertAll (converter);

			if (documentHistories == null) {
				throw new ObjectNotFoundException (
					"Document-histroy not found.",
					ObjectNotFoundErrors.DocumentHistoryNotFound);
			}

			return documentHistories;
		}

		#endregion

		#region Fetch Document Type

		/// <summary>
		/// Fetches the document-type by document-type-id.
		/// </summary>
		/// <param name="documentTypeId">The document-type-id.</param>
		/// <returns>A document-type.</returns>
		/// <exception cref="ObjectNotFoundException">If the document-type is not found.</exception>

		public DocumentType GetDocumentTypeById (DocumentTypes documentTypeId)
		{
			IQueryable<EDocumentType> query = from documentType in _dataContext.DocumentTypes
			                                  where documentType.Id == (int) documentTypeId
			                                  select documentType;

			IList<EDocumentType> documentTypes = query.ToList ();

			if (documentTypes.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Document-type not found.",
					ObjectNotFoundErrors.DocumentTypeNotFound);
			}

			return Converter (documentTypes.First ());
			
		}

		/// <summary>
		/// Fetches all document-types.
		/// </summary>
		/// <returns>A list of document-types.</returns>
		/// <exception cref="ObjectNotFoundException">If no document-types are found.</exception>

		public IList<DocumentType> GetAllDocumentTypes ()
		{
			IOrderedQueryable<EDocumentType> query = from documentType in _dataContext.DocumentTypes
						orderby documentType.Name ascending
						select documentType;

			Converter<EDocumentType, DocumentType> converter = Converter;
			IList<DocumentType> documentTypes = query.ToList ().ConvertAll (converter);

			if (documentTypes == null) {
				throw new ObjectNotFoundException (
					"Document-type not found.",
					ObjectNotFoundErrors.DocumentTypeNotFound);
			}

			return documentTypes;
		}

		#endregion

		#endregion

		#region Converters

		/// <summary>
		/// Converts a EDocument to a Document with associations.
		/// </summary>
		/// <param name="eDocument">The eDocument.</param>
		/// <returns>A document.</returns>

		public Document Converter (EDocument eDocument)
		{
			Document document = EDocument.Convert (eDocument);

			if (!SkipDocumentHistories && (eDocument.DocumentHistories != null)) {
				SkipDocument = true;
				Converter<EDocumentHistory, DocumentHistory> converter = Converter;
				document.DocumentHistories = eDocument.DocumentHistories.ToList ().ConvertAll (converter);
				SkipDocument = false;
			}

			if (!SkipDocumentType && (eDocument.DocumentType != null)) {
				SkipDocument = true;
				document.DocumentType = Converter (eDocument.DocumentType);
				SkipDocument = false;
			}

			if (!SkipNode && (eDocument.Node != null)) {
				Manager.Node.SkipDocuments = true;
				document.Node = Manager.Node.Converter (eDocument.Node);
				Manager.Node.SkipDocuments = false;
			}

			// Only fetch users flat (external object).
			if (!SkipUsers && (eDocument.CreatedBy != null)) {
				document.CreatedBy = EBaseUser.Convert (eDocument.CreatedBy);
			}

			// Only fetch users flat (external object).
			if (!SkipUsers && (eDocument.ChangedBy != null)) {
				document.ChangedBy = EBaseUser.Convert (eDocument.ChangedBy);
			}

			// Only fetch users flat (external object).
			if (!SkipUsers && (eDocument.ApprovedBy != null)) {
				document.ApprovedBy = EBaseUser.Convert (eDocument.ApprovedBy);
			}

			// Only fetch users flat (external object).
			if (!SkipUsers && (eDocument.LockedBy != null)) {
				document.LockedBy = EBaseUser.Convert (eDocument.LockedBy);
			}

			// Only fetch users flat (external object).
			if (!SkipConcern && (eDocument.Concern != null)) {
				document.Concern = EConcern.Convert (eDocument.Concern);
			}

			// Only fetch users flat (external object).
			if (!SkipShop && (eDocument.Shop != null)) {
				document.Shop = EShop.Convert (eDocument.Shop);
			}

			return document;
		}

		/// <summary>
		/// Converts a EDocumentHistory to a DocumentHistory with associations.
		/// </summary>
		/// <param name="eDocumentHistory">The eDocumentHistory.</param>
		/// <returns>A document-history.</returns>

		public DocumentHistory Converter (EDocumentHistory eDocumentHistory)
		{
			DocumentHistory documentHistory = EDocumentHistory.Convert (eDocumentHistory);

			if (!SkipDocument && eDocumentHistory.Document != null) {
				SkipDocumentHistories = true;
				documentHistory.Document = Converter (eDocumentHistory.Document);
				SkipDocumentHistories = false;
			}

			// Only fetch users flat (external object).
			if (!SkipUser && (eDocumentHistory.HistoryIdUser != null)) {
				documentHistory.HistoryIdUser = EBaseUser.Convert (eDocumentHistory.HistoryIdUser);
			}

			return documentHistory;
		}

		/// <summary>
		/// Converts a EDocumentType to a DocumentType with associations.
		/// </summary>
		/// <param name="eDocumentType">The eDocumentType.</param>
		/// <returns>A document-type.</returns>

		public DocumentType Converter (EDocumentType eDocumentType)
		{
			DocumentType documentType = EDocumentType.Convert (eDocumentType);

			if (!SkipDocument && eDocumentType.Documents != null) {
				SkipDocumentHistories = true;
				Converter<EDocument, Document> converter = Converter;
				documentType.Documents = eDocumentType.Documents.ToList ().ConvertAll (converter);
				SkipDocumentHistories = false;
			}

			return documentType;
		}

		#endregion

		#region Properties

		#region Skips for Document

		/// <summary>
		/// If true=>skip document-histories.
		/// </summary>

		public bool SkipDocumentHistories { get; set; }

		/// <summary>
		/// If true=>skip document-type.
		/// </summary>

		public bool SkipDocumentType { get; set; }

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

		#region Sips for Document-History and Document-Type

		/// <summary>
		/// If true=>skip docuemnt.
		/// </summary>

		public bool SkipDocument { get; set; }

		/// <summary>
		/// If true=>skip user.
		/// </summary>

		public bool SkipUser { get; set; }

		#endregion

		#endregion
	}
}
