using System.Collections.Generic;
using System.Threading;

using NUnit.Framework;

using Spinit.Wpc.Synologen.OPQ.Business.Test.Properties;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;


namespace Spinit.Wpc.Synologen.OPQ.Business.Test
{
	[TestFixture, Description("The unit tests for the document business layer.")]
	public class BDocumentTests
	{
		private Configuration _configuration;
		private Context _context;
		private TestInit _init;

		[SetUp, Description("Initialize.")]
		public void NodeManagerInit()
		{
			_configuration = new Configuration(
				Settings.Default.ConnectionString,
				Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);

			_context = new Context(
				Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
				string.Empty,
				1,
				"Admin");
			_init = new TestInit(_configuration);
			_init.InitDatabase();
		}

		[TearDown, Description("Close.")]
		public void NodeManagerCleanUp()
		{
			_init.DeleteDatabase();
			_configuration = null;
			_context = null;
		}

		[Test, Description ("Creates a document."), Category ("Document")]
		public void CreateDocument ()
		{
			const string rootName = "opq";
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, rootName, true);

			const string documentContent = "Content";
			BDocument bDocument = new BDocument (_context);
			Document document = bDocument.CreateDocument (node.Id, null, null, DocumentTypes.Routine, documentContent);
			Assert.AreEqual (documentContent, document.DocumentContent, "Document content not as expected");
			Assert.AreEqual (node.Id, document.NdeId, "Node id not as expected");
			Assert.AreEqual (DocumentTypes.Routine, document.DocTpeId, "Documenttype not as excpected");
		}

		[Test, Description ("Creates a document and fetches. Compare results"), Category ("Document")]
		public void CreateAndFetchDocument ()
		{
			const string rootName = "opq";
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, rootName, true);

			const string documentContent = "Content";
			BDocument bDocument = new BDocument (_context);
			Document document = bDocument.CreateDocument (node.Id, null, null, DocumentTypes.Routine, documentContent);
			document = bDocument.GetDocument (document.Id, true);
			Assert.AreEqual (documentContent, document.DocumentContent, "Document content not as expected");
			Assert.AreEqual (node.Id, document.NdeId, "Node id not as expected");
			Assert.AreEqual (node, document.Node, "Node not as expected");
			Assert.AreEqual (DocumentTypes.Routine, document.DocTpeId, "Documenttype not as excpected");
		}

		[Test, Description ("Creates and updates a document. Compare results"), Category ("Document")]
		public void CreateAndUpdatesDocument ()
		{
			const string rootName = "opq";
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, rootName, false);

			const string documentContent = "Content";
			const string newContent = "New Content";
			BDocument bDocument = new BDocument (_context);
			Document document = bDocument.CreateDocument (node.Id, null, null, DocumentTypes.Routine, documentContent);
			document = bDocument.ChangeDocument (document.Id, newContent);

			Assert.AreEqual (newContent, document.DocumentContent, "Document content not as expected");
			Assert.AreEqual (node.Id, document.NdeId, "Node id not as expected");
			Assert.AreEqual (node, document.Node, "Node not as expected");
			Assert.AreEqual (DocumentTypes.Routine, document.DocTpeId, "Documenttype not as excpected");
		}

		[Test, Description ("Creates and fetches document from node. Compare results"), Category ("Document")]
		public void CreateAndFetchDocumentFromNode ()
		{
			const string rootName = "opq";
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, rootName, false);

			const string documentContent = "Content";
			BDocument bDocument = new BDocument (_context);
			Document document = bDocument.CreateDocument (node.Id, null, null, DocumentTypes.Routine, documentContent);
			bDocument.Publish (document.Id);
			bDocument.UnLock (document.Id);
			List<Document> documents =
				(List<Document>) bDocument.GetDocuments (node.Id, null, null, null, DocumentTypes.Routine, null, true, true, true);
			Assert.IsNotNull (documents, "Documents returned null");
			Assert.IsNotEmpty (documents, "Documents returned empty. Should be 1.");
			document = documents [0];
			Assert.AreEqual (documentContent, document.DocumentContent, "Document content not as expected");
			Assert.AreEqual (node.Id, document.NdeId, "Node id not as expected");
			Assert.AreEqual (node, document.Node, "Node not as expected");
		}

		[Test, Description ("Deletes a document")]
		public void DeleteDocument ()
		{
			const string rootName = "opq";
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, rootName, false);

			const string documentContent = "Content";
			BDocument bDocument = new BDocument (_context);
			Document document = bDocument.CreateDocument (node.Id, null, null, DocumentTypes.Routine, documentContent);
			bDocument.DeleteDocument (document.Id, false);
			List<Document> documents =
				(List<Document>) bDocument.GetDocuments (node.Id, null, null, null, DocumentTypes.Routine, null, true, true, true);
			Assert.IsEmpty (documents, "No documents should be returned");
		}
		
		[Test, Description ("Creates a document and updates without history.")]
		public void CreateAndUpdateWithoutHistory ()
		{
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, PropertyValues.DocumentNodeName, false);

			BDocument bDocument = new BDocument (_context);
			Document document = bDocument.CreateDocument (node.Id, null, null, DocumentTypes.Routine, PropertyValues.FirstUpdateHistoryContent);
			bDocument.ChangeDocument (document.Id, PropertyValues.SecondUpdateHistoryContent);
			List<DocumentHistory> documentHistories = (List<DocumentHistory>) bDocument.GetDocumentHistories (document.Id, true);
			Assert.IsEmpty (documentHistories, "No document-history should be returned.");
		}
	
		[Test, Description ("Creates a document and updates with history.")]
		public void CreateAndUpdateWithHistory ()
		{
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, PropertyValues.DocumentNodeName, false);

			BDocument bDocument = new BDocument (_context);
			Document document = bDocument.CreateDocument (node.Id, null, null, DocumentTypes.Routine, PropertyValues.FirstUpdateHistoryContent);
			bDocument.Publish (document.Id);
			bDocument.UnLock (document.Id);
			List<DocumentHistory> documentHistories = (List<DocumentHistory>) bDocument.GetDocumentHistories (document.Id, true);
			Assert.IsEmpty (documentHistories, "No document-history should be returned.");

			bDocument.UnPublish (document.Id);
			documentHistories = (List<DocumentHistory>) bDocument.GetDocumentHistories (document.Id, true);
			Assert.IsNotEmpty (documentHistories, "Document-history should be returned.");
		}
	}
}
