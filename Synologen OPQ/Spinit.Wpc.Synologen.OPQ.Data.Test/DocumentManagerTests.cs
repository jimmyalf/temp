using System.Threading;

using NUnit.Framework;

using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
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

				string content = @"The test document content.";
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
				content = @"Updated document test";
				fetchDocument.DocumentContent = content;
				synologenRepository.Document.Update (fetchDocument);

				synologenRepository.SubmitChanges ();

				// ReFetch the document
				fetchDocument = synologenRepository.Document.GetDocumentById (document.Id);

				Assert.IsNotNull (fetchDocument, "ReFetched document is null.");
				Assert.AreEqual (content, fetchDocument.DocumentContent, "Content are not equal");
			
				// Delete the document
				synologenRepository.Document.Delete (fetchDocument);

				//synologenRepository.SubmitChanges ();
			}
		}
	}
}
