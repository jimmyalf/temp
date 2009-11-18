using System;
using System.Collections.Generic;
using System.Threading;

using NUnit.Framework;

using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.OPQ.Core.Exceptions;
using Spinit.Wpc.Synologen.OPQ.Data.Test.Properties;

namespace Spinit.Wpc.Synologen.OPQ.Data.Test
{
	[TestFixture, Description ("The unit tests for the data document-manager.")]
	public class DocumentManagerTests
	{
		private Configuration _configuration;
		private Context _context;

		[SetUp, Description ("Initialize.")]
		public void NodeManagerInit ()
		{
			_configuration = new Configuration (
				Settings.Default.ConnectionString, 
				Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);

			_context = new Context (
				Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
				string.Empty,
				PropertyValues.UserId,
				PropertyValues.UserName);
		}

		[TearDown, Description ("Close.")]
		public void NodeManagerCleanUp ()
		{
			_configuration = null;
			_context = null;
		}

		#region Document tests

		[Test, Description ("Creates, fetches, updates and deletes a document."), Category ("Internal")]
		public void DocumentAddUpdateDeleteTest ()
		{
			Document document;
			Document fetchDocument;
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {

				// Create a new document
				synologenRepository.Document.Insert (
					new Document
					{
						NdeId = PropertyValues.NodeId,
						DocTpeId = PropertyValues.DocDocumentType,
						DocumentContent = PropertyValues.DocCreateDocumentContent,
						IsActive = true
					});

				synologenRepository.SubmitChanges ();

				document = synologenRepository.Document.GetInsertedDocument ();

				Assert.IsNotNull (document, "Document is null.");

				// Fetch the docuemnt
				fetchDocument = synologenRepository.Document.GetDocumentById (document.Id);

				Assert.IsNotNull (fetchDocument, "Fetched document is null.");
				Assert.AreEqual (PropertyValues.DocCreateDocumentContent, fetchDocument.DocumentContent, "Content are not equal");
			}

			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				// Approve and check-in.
				synologenRepository.Document.ApproveDocument (document.Id);
				synologenRepository.Document.CheckInDocument (document.Id);

				synologenRepository.SubmitChanges ();
			}

			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				// Un-pprove and check-out.
				synologenRepository.Document.UnApproveDocument (document.Id);
				synologenRepository.Document.CheckOutDocument (document.Id);

				synologenRepository.SubmitChanges ();
			}

			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {
				// Update node
				fetchDocument.DocumentContent = PropertyValues.DocUpdateDocumentContent;
				synologenRepository.Document.Update (fetchDocument);

				synologenRepository.SubmitChanges ();

				// ReFetch the document
				fetchDocument = synologenRepository.Document.GetDocumentById (document.Id);

				Assert.IsNotNull (fetchDocument, "ReFetched document is null.");
				Assert.AreEqual (PropertyValues.DocUpdateDocumentContent, fetchDocument.DocumentContent, "Updated content are not equal");

				// Fetch Histories
				List<DocumentHistory> documentHistories =
					(List<DocumentHistory>) synologenRepository.Document.GetAllDocumentHistoriesByDocumentId (fetchDocument.Id);
				
				Assert.IsNotNull (documentHistories, "Document-histories is null.");
				Assert.IsNotEmpty (documentHistories, "Document-histories is empty.");

				DocumentHistory documentHistory = synologenRepository.Document.GetDocumentHistoryById (
					documentHistories [0].Id,
					documentHistories [0].HistoryDate);

				Assert.IsNotNull (documentHistory, "Document-history document is null.");
				Assert.AreEqual (PropertyValues.DocCreateDocumentContent, documentHistory.DocumentContent, "History content are not equal");

				// Delete the document
				synologenRepository.Document.Delete (fetchDocument);

				synologenRepository.SubmitChanges ();

				bool found = true;
				try {
					// ReFetch the document
					fetchDocument = synologenRepository.Document.GetDocumentById (document.Id);

					Assert.IsNull (fetchDocument, "Deleted document is not null.");
				}
				catch (ObjectNotFoundException e) {
					if (ObjectNotFoundErrors.DocumentNotFound == (ObjectNotFoundErrors) e.ErrorCode) {
						found = false;
					}
				}

				Assert.AreEqual (false, found, "Object still exist.");
			}
		}

		[Test, Description ("Fetches all active documents for a specified node."), Category ("CruiseControl")]
		public void DocumentSearchTestNodeActive ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				List<Document> documents =
					(List<Document>) synologenRepository.Document.GetDocumentsByNodeId (PropertyValues.DocNodeIdActive, true, true);

				Assert.IsNotEmpty (documents, "Documents is empty (only-active).");
				Assert.AreEqual (PropertyValues.DocCountOnlyActive, documents.Count, "Wrong number of documents (only-active).");
			}
		}

		[Test, Description ("Fetches all documents for a specified node."), Category ("CruiseControl")]
		public void DocumentSearchTestNodeAll ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				List<Document> documents = 
					(List<Document>) synologenRepository.Document.GetDocumentsByNodeId (PropertyValues.DocNodeIdActive, false, false);

				Assert.IsNotEmpty (documents, "Documents is empty.");
				Assert.AreEqual (PropertyValues.DocCountAll, documents.Count, "Wrong number of documents.");
			}
		}

		[Test, Description ("Fetches the content for a specified document."), Category ("CruiseControl")]
		public void DocumentSearchTestDocumentContent ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				Document document = synologenRepository.Document.GetDocumentById (PropertyValues.DocDocumentId);

				Assert.IsNotNull (document, "Document is null.");
				Assert.AreEqual (PropertyValues.DocDocumentContent, document.DocumentContent, "Wrong content fetched (document).");
			}
		}

		[Test, 
		Description ("Fetches the document-histories for a specified document, and checks the content of the first."), 
		Category ("CruiseControl")]
		public void DocumentSearchTestDocumentHistories ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				List<DocumentHistory> documentHistories =
					(List<DocumentHistory>) synologenRepository.Document.GetAllDocumentHistoriesByDocumentId (PropertyValues.DocDocumentId);

				Assert.IsNotEmpty (documentHistories, "Document-history is empty.");
				Assert.AreEqual (PropertyValues.DocCountHistory, documentHistories.Count, "Wrong number of document-history.");
				
				DocumentHistory documentHistory = synologenRepository.Document.GetDocumentHistoryById (
					documentHistories [0].Id,
					documentHistories [0].HistoryDate);

				Assert.IsNotNull (documentHistory, "Document-history is null.");
				Assert.AreEqual (
					PropertyValues.DocDocumentContentHistory,
					documentHistory.DocumentContent,
					"Wrong content fetched (document-history).");
			}
		}

		[Test, Description ("Fetches the active content from the view, active without history."), Category ("CruiseControl")]
		public void DocumentSearchTest ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				DocumentView documentView = synologenRepository.Document.GetActiveDocument (
					PropertyValues.DocDocumentIdView2, 
					null, 
					null,
					PropertyValues.DocDocumentTypeView);

				Assert.IsNotNull (documentView, "Document-view 2 is null.");
				Assert.AreEqual (PropertyValues.DocDocumentContentView2, documentView.DocumentContent, "Wrong content fetched (view 2).");
			}
		}

		[Test, Description ("Fetches the active content from the view, active with history."), Category ("CruiseControl")]
		public void DocumentSearchTestFromView1 ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				DocumentView documentView = synologenRepository.Document.GetActiveDocument (
					PropertyValues.DocDocumentIdView1,
					null,
					null,
					PropertyValues.DocDocumentTypeView);

				Assert.IsNotNull (documentView, "Document-view 1 is null.");
				Assert.AreEqual (PropertyValues.DocDocumentContentView1, documentView.DocumentContent, "Wrong content fetched (view 1).");
			}
		}
	
		[Test, Description ("Fetches the active content from the view, not active with active history."), Category ("CruiseControl")]
		public void DocumentSearchTestView3 ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				DocumentView documentView = synologenRepository.Document.GetActiveDocument (
					PropertyValues.DocDocumentIdView3,
					null,
					null,
					PropertyValues.DocDocumentTypeView);

				Assert.IsNotNull (documentView, "Document-view 3 is null.");
				Assert.AreEqual (PropertyValues.DocDocumentContentView3, documentView.DocumentContent, "Wrong content fetched (view 3).");
			}
		}

		[Test, Description ("Fetches a active document from the view using internal methods (same as business)."), Category ("CruiseControl")]
		public void DocumentSearchActive ()
		{
			List<Document> documents = (List<Document>) GetActiveDocuments (1, null, null, DocumentTypes.Routine, null, false);

			Assert.IsNotNull (documents, "Documents is null.");
			Assert.AreEqual (1, documents.Count, "Wrong number of documents fetced.");
		}

		#endregion

		#region Document Type tests

		[Test, Description ("Fetches all document types."), Category ("CruiseControl")]
		public void DocumentTypeSearchAll ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				IList<DocumentType> documentTypes = synologenRepository.Document.GetAllDocumentTypes ();

				Assert.IsNotNull (documentTypes, "Document-types is null.");
				Assert.AreEqual (PropertyValues.DocumentTypesNumber, documentTypes.Count, "Wrong number of document-types.");
			}
		}

		[Test, Description ("Fetches a specified document type."), Category ("CruiseControl")]
		public void DocumentTypeSearchSpecific ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				DocumentType documentType = synologenRepository.Document.GetDocumentTypeById (PropertyValues.DocDocumentType);

				Assert.IsNotNull (documentType, "Document-type is null.");
				Assert.AreEqual (PropertyValues.DocumentTypeConent, documentType.Name, "Wrong content fetched for document-type.");
			}
		}

		#endregion

		#region Internal Methods

		/// <summary>
		/// Gets list of documents.
		/// </summary>
		/// <param name="nodeId">The id of the node.</param>
		/// <param name="shopId">The id of the shop.</param>
		/// <param name="cncId">The id of the concern.</param>
		/// <param name="documentType">The type of the document.</param>
		/// <param name="searchText">Text to search for.</param>
		/// <param name="fillObjects">If true=>fill-objects.</param>

		private IList<Document> GetActiveDocuments (
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

				if ((nodeId != null) && ((shopId != null) || (cncId != null)) && (documentType != null)) {
					documents = synologenRepository.Document.GetDocumentsByNodeId (
						(int) nodeId,
						shopId,
						cncId,
						(DocumentTypes) documentType,
						false,
						false);
				}
				else if ((nodeId != null) && (documentType != null)) {
					documents = synologenRepository.Document.GetDocumentsByNodeId (
						(int) nodeId,
						(DocumentTypes) documentType,
						false,
						false);
				}
				else if (nodeId != null) {
					documents = synologenRepository.Document.GetDocumentsByNodeId ((int) nodeId, false, false);
				}
				else if (searchText != null) {
					documents = synologenRepository.Document.GetDocumentsByText (searchText, false, false);
				}
				else {
					documents = synologenRepository.Document.GetAllDocuments (false, false);
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

		private Document GetActiveDocument (int id, bool fillObjects)
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
	}
}
