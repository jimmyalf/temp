using System.Collections.Generic;
using System.Threading;

using NUnit.Framework;

using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.Opq.Core.Exceptions;
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

		[Test, Explicit, Description ("Creates, fetches, updates and deletes a document."), Category ("Internal")]
		public void DocumentAddUpdateDeleteTest ()
		{
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

				Document document = synologenRepository.Document.GetInsertedDocument ();

				Assert.IsNotNull (document, "Document is null.");

				// Fetch the docuemnt
				Document fetchDocument = synologenRepository.Document.GetDocumentById (document.Id);

				Assert.IsNotNull (fetchDocument, "Fetched document is null.");
				Assert.AreEqual (PropertyValues.DocCreateDocumentContent, fetchDocument.DocumentContent, "Content are not equal");
		
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
		public void DocmentSearchTestNodeActive ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				List<Document> documents =
					(List<Document>) synologenRepository.Document.GetDocumentsByNodeId (PropertyValues.DocNodeIdActive, true);

				Assert.IsNotEmpty (documents, "Documents is empty (only-active).");
				Assert.AreEqual (PropertyValues.DocCountOnlyActive, documents.Count, "Wrong number of documents (only-active).");
			}
		}

		[Test, Description ("Fetches all documents for a specified node."), Category ("CruiseControl")]
		public void DocmentSearchTestNodeAll ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				List<Document> documents = 
					(List<Document>) synologenRepository.Document.GetDocumentsByNodeId (PropertyValues.DocNodeIdActive, false);

				Assert.IsNotEmpty (documents, "Documents is empty.");
				Assert.AreEqual (PropertyValues.DocCountAll, documents.Count, "Wrong number of documents.");
			}
		}

		[Test, Description ("Fetches the content for a specified document."), Category ("CruiseControl")]
		public void DocmentSearchTestDocumentContent ()
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
		public void DocmentSearchTestDocumentHistories ()
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
		public void DocmentSearchTest ()
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
		public void DocmentSearchTestFromView1 ()
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
		public void DocmentSearchTestView3 ()
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
	}
}
