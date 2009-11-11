using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

		[Test, Description("Creates a document."), Category("Document")]
		public void CreateDocument()
		{
			const string rootName = "opq";
			var bNode = new BNode(_context);
			var node = bNode.CreateNode(null, rootName);

			string documentContent = "Content";
			var bDocument = new BDocument(_context);
			var document = bDocument.CreateDocument(node.Id, null, DocumentTypes.Routine, documentContent);
			Assert.AreEqual(documentContent, document.DocumentContent, "Document content not as expected");
			Assert.AreEqual(node.Id, document.NdeId, "Node id not as expected" );
			Assert.AreEqual(DocumentTypes.Routine, (DocumentTypes) document.DocTpeId, "Documenttype not as excpected");

		}

		[Test, Description("Creates a document and fetches. Compare results"), Category("Document")]
		public void CreateAndFetchDocument()
		{
			const string rootName = "opq";
			var bNode = new BNode(_context);
			var node = bNode.CreateNode(null, rootName);

			string documentContent = "Content";
			var bDocument = new BDocument(_context);
			var document = bDocument.CreateDocument(node.Id, null, DocumentTypes.Routine, documentContent);
			document = bDocument.GetDocument(document.Id, true);
			Assert.AreEqual(documentContent, document.DocumentContent, "Document content not as expected");
			Assert.AreEqual(node.Id, document.NdeId, "Node id not as expected");
			Assert.AreEqual(node, document.Node, "Node not as expected");
			Assert.AreEqual(DocumentTypes.Routine, (DocumentTypes)document.DocTpeId, "Documenttype not as excpected");
		}

		[Test, Description("Creates and updates a document. Compare results"), Category("Document")]
		public void CreateAndUpdatesDocument()
		{
			const string rootName = "opq";
			var bNode = new BNode(_context);
			var node = bNode.CreateNode(null, rootName);

			const string documentContent = "Content";
			const string newContent = "New Content";
			var bDocument = new BDocument(_context);
			var document = bDocument.CreateDocument(node.Id, null, DocumentTypes.Routine, documentContent);
			document = bDocument.ChangeDocument(document.Id, newContent);

			Assert.AreEqual(newContent, document.DocumentContent, "Document content not as expected");
			Assert.AreEqual(node.Id, document.NdeId, "Node id not as expected");
			Assert.AreEqual(node, document.Node, "Node not as expected");
			Assert.AreEqual(DocumentTypes.Routine, (DocumentTypes)document.DocTpeId, "Documenttype not as excpected");
		}

		[Test, Description("Creates and fetches document from node. Compare results"), Category("Document")]
		public void CreateAndFetchDocumentFromNode()
		{
			const string rootName = "opq";
			var bNode = new BNode(_context);
			var node = bNode.CreateNode(null, rootName);

			const string documentContent = "Content";
			const string newContent = "New Content";
			var bDocument = new BDocument(_context);
			var document = bDocument.CreateDocument(node.Id, null, DocumentTypes.Routine, documentContent);
			IList<Document> documents =
				bDocument.GetDocuments(node.Id, null, DocumentTypes.Routine, null, true, true, true);
			if (documents == null || documents.Count == 0)
			{
				Assert.Fail("Documents returned empty. Should be 1");
			}
			document = (Document) documents[0];
			Assert.AreEqual(newContent, document.DocumentContent, "Document content not as expected");
			Assert.AreEqual(node.Id, document.NdeId, "Node id not as expected");
			Assert.AreEqual(node, document.Node, "Node not as expected");
		}

		[Test, Description("Deletes a document")]
		public void DeleteDocument()
		{
			const string rootName = "opq";
			var bNode = new BNode(_context);
			var node = bNode.CreateNode(null, rootName);

			const string documentContent = "Content";
			const string newContent = "New Content";
			var bDocument = new BDocument(_context);
			var document = bDocument.CreateDocument(node.Id, null, DocumentTypes.Routine, documentContent);
			bDocument.DeleteDocument(document.Id, false);
			IList<Document> documents =
			bDocument.GetDocuments(node.Id, null, DocumentTypes.Routine, null, true, true, true);
			if (documents.Count != 0)
			{
				Assert.Fail("No documents should be returned");
			}

		}
	}
}
