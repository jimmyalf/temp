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
		private const int NodeId = 1;

		[SetUp, Description ("Initialize.")]
		public void NodeManagerInit ()
		{
			_configuration = new Configuration (
				Settings.Default.ConnectionString, 
				Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);

			_context = new Context (
				Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
				string.Empty,
				1,
				"Admin");
		}

		[TearDown, Description ("Close.")]
		public void NodeManagerCleanUp ()
		{
			_configuration = null;
			_context = null;
		}

		[Test, Explicit, Description ("Creates, fetches, updates and deletes a document."), Category ("CruiseControl")]
		public void DocumentTest ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {

				const string  content = @"The test document content.";
				// Create a new document
				synologenRepository.Document.Insert (
					new Document
					{
						NdeId = NodeId,
						DocTpeId = DocumentTypes.Routine,
						DocumentContent = content,
						IsActive = true
					});

				synologenRepository.SubmitChanges ();

				Document document = synologenRepository.Document.GetInsertedDocument ();

				Assert.IsNotNull (document, "Document is null.");

				// Fetch the docuemnt
				Document fetchDocument = synologenRepository.Document.GetDocumentById (document.Id);

				Assert.IsNotNull (fetchDocument, "Fetched document is null.");
				Assert.AreEqual (content, fetchDocument.DocumentContent, "Content are not equal");
		
				// Update node
				const string  updateContent = @"Updated document test";
				fetchDocument.DocumentContent = updateContent;
				synologenRepository.Document.Update (fetchDocument);

				synologenRepository.SubmitChanges ();

				// ReFetch the document
				fetchDocument = synologenRepository.Document.GetDocumentById (document.Id);

				Assert.IsNotNull (fetchDocument, "ReFetched document is null.");
				Assert.AreEqual (updateContent, fetchDocument.DocumentContent, "Updated content are not equal");

				// Fetch Histories
				List<DocumentHistory> documentHistories =
					(List<DocumentHistory>) synologenRepository.Document.GetAllDocumentHistoriesByDocumentId (fetchDocument.Id);
				
				Assert.IsNotNull (documentHistories, "Document-histories is null.");
				Assert.IsNotEmpty (documentHistories, "Document-histories is empty.");

				DocumentHistory documentHistory = synologenRepository.Document.GetDocumentHistoryById (
					documentHistories [0].Id,
					documentHistories [0].HistoryDate);

				Assert.IsNotNull (documentHistory, "Document-history document is null.");
				Assert.AreEqual (content, documentHistory.DocumentContent, "History content are not equal");

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
	
		[Test, Description ("Searches for nodes."), Category ("CruiseControl")]
		public void DocmentSearchTest ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (
					_configuration, null, _context)
				) {

				List<Document> documents = (List<Document>) synologenRepository.Document.GetDocumentsByNodeId (1, true);

				Assert.IsNotEmpty (documents, "Documents is empty (only-active).");
				Assert.AreEqual (1, documents.Count, "Wrong number of documents (only-active).");

				documents = (List<Document>) synologenRepository.Document.GetDocumentsByNodeId (5, false);

				Assert.IsNotEmpty (documents, "Documents is empty.");
				Assert.AreEqual (1, documents.Count, "Wrong number of documents.");

				Document document = synologenRepository.Document.GetDocumentById (1);

				Assert.IsNotNull (document, "Document is null.");
				Assert.AreEqual ("Test-Content-1", document.DocumentContent, "Wrong content fetched (document).");

				List<DocumentHistory> documentHistories =
					(List<DocumentHistory>) synologenRepository.Document.GetAllDocumentHistoriesByDocumentId (1);

				Assert.IsNotEmpty (documentHistories, "Document-history is empty.");
				Assert.AreEqual (3, documentHistories.Count, "Wrong number of document-history.");

				DocumentHistory documentHistory = synologenRepository.Document.GetDocumentHistoryById (
					documentHistories [0].Id,
					documentHistories [0].HistoryDate);

				Assert.IsNotNull (documentHistory, "Document-hsitory is null.");
				Assert.AreEqual ("Test-Content-History-1-1", documentHistory.DocumentContent, "Wrong content fetched (document-history).");

				DocumentView documentView = synologenRepository.Document.GetActiveDocument (1, null, null, DocumentTypes.Routine);

				Assert.IsNotNull (documentView, "Document-view 1 is null.");
				Assert.AreEqual ("Test-Content-1", documentView.DocumentContent, "Wrong content fetched (view 1).");
				
				documentView = synologenRepository.Document.GetActiveDocument (2, null, null, DocumentTypes.Routine);

				Assert.IsNotNull (documentView, "Document-view 1 is null.");
				Assert.AreEqual ("Test-Content-2", documentView.DocumentContent, "Wrong content fetched (view 2).");
				
				documentView = synologenRepository.Document.GetActiveDocument (3, null, null, DocumentTypes.Routine);

				Assert.IsNotNull (documentView, "Document-view 1 is null.");
				Assert.AreEqual ("Test-Content-History-3-2", documentView.DocumentContent, "Wrong content fetched (view 3).");
			}
		}
	}
}
