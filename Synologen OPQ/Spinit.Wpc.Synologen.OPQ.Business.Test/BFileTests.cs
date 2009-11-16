using System.Collections.Generic;
using System.Threading;

using NUnit.Framework;

using Spinit.Wpc.Synologen.OPQ.Business.Test.Properties;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;

namespace Spinit.Wpc.Synologen.OPQ.Business.Test
{
	[TestFixture, Description("The nunit tests for file business layer")]
	public class BFileTests
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

		[Test, Description ("Creates a file-category."), Category ("File")]
		public void CreateFileCategory ()
		{
			const string fileCategoryName = "File Category";
			BFile bFile = new BFile (_context);
			FileCategory fileCategory = bFile.CreateFileCategory (fileCategoryName);
			Assert.AreEqual (fileCategoryName, fileCategory.Name, "File-category name not as excpected");
		}

		[Test, Description ("Creates a file-category and fetches. Compare results"), Category ("File")]
		public void CreateAndFetchFileCategory ()
		{
			const string fileCategoryName = "File Category";
			BFile bFile = new BFile (_context);
			FileCategory fileCategory = bFile.CreateFileCategory (fileCategoryName);
			fileCategory = bFile.GetFileCategory (fileCategory.Id);
			Assert.AreEqual (fileCategoryName, fileCategory.Name, "File-category name not as excpected");
		}

		[Test, Description ("Creates and updates a file-category. Compare results"), Category ("File")]
		public void CreateAndUpdatesFileCategory ()
		{
			const string fileCategoryName = "File Category";
			const string newFileCategoryName = "Update File Category";
			BFile bFile = new BFile (_context);
			FileCategory fileCategory = bFile.CreateFileCategory (fileCategoryName);
			fileCategory = bFile.GetFileCategory (fileCategory.Id);
			fileCategory = bFile.ChangeFileCategory (fileCategory.Id, newFileCategoryName);
			Assert.AreEqual (newFileCategoryName, fileCategory.Name, "File-category name not as excpected");
		}

		[Test, Description ("Deletes a file-category")]
		public void DeleteFileCategory ()
		{
			const string fileCategoryName = "File Category";
			BFile bFile = new BFile (_context);
			FileCategory fileCategory = bFile.CreateFileCategory (fileCategoryName);
			bFile.DeleteFileCategory (fileCategory.Id, false);
			List<FileCategory> fileCategories = (List<FileCategory>) bFile.GetFileCategories (true);
			Assert.IsEmpty (fileCategories, "No file-categories should be returned");
		}

		[Test, Description ("Creates a file."), Category ("File")]
		public void CreateFile ()
		{
			const string rootName = "opq";
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, rootName, false);

			const int fleId = 1;
			const string fileCategoryName = "File Category";
			BFile bFile = new BFile (_context);
			FileCategory fileCategory = bFile.CreateFileCategory (fileCategoryName);
			File file = bFile.CreateFile (node.Id, null,null, fleId, fileCategory);
			Assert.AreEqual (fleId, file.FleId, "Base file id not as expected");
			Assert.AreEqual (node.Id, file.NdeId, "Node id not as expected");
			Assert.AreEqual (fileCategory.Id, file.FleCatId, "File-category not as excpected");
		}

		[Test, Description ("Creates a file and fetches. Compare results"), Category ("File")]
		public void CreateAndFetchFile ()
		{
			const string rootName = "opq";
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, rootName, false);

			const int fleId = 1;
			const string fileCategoryName = "File Category";
			BFile bFile = new BFile (_context);
			FileCategory fileCategory = bFile.CreateFileCategory (fileCategoryName);
			File file = bFile.CreateFile (node.Id, null, null, fleId, fileCategory);
			file = bFile.GetFile (file.Id, true);
			Assert.AreEqual (fleId, file.FleId, "Base file id not as expected");
			Assert.AreEqual (node.Id, file.NdeId, "Node id not as expected");
			Assert.AreEqual (node, file.Node, "Node not as expected");
			Assert.AreEqual (fileCategory.Id, file.FleCatId, "File-category not as excpected");
		}

		[Test, Description ("Creates and updates a file. Compare results"), Category ("File")]
		public void CreateAndUpdatesFile ()
		{
			const string rootName = "opq";
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, rootName, false);

			const int fleId = 1;
			const int newFleId = 2;
			const string fileCategoryName = "File Category";
			BFile bFile = new BFile (_context);
			FileCategory fileCategory = bFile.CreateFileCategory (fileCategoryName);
			File file = bFile.CreateFile (node.Id, null, null, fleId, fileCategory);
			file = bFile.ChangeFile (file.Id, newFleId);

			Assert.AreEqual (newFleId, file.FleId, "Base file id not as expected");
			Assert.AreEqual (node.Id, file.NdeId, "Node id not as expected");
			Assert.AreEqual (node, file.Node, "Node not as expected");
			Assert.AreEqual (fileCategory.Id, file.FleCatId, "File-category not as excpected");
		}

		[Test, Description ("Creates and fetches file from node without category. Compare results"), Category ("File")]
		public void CreateAndFetchFileFromNode ()
		{
			const string rootName = "opq";
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, rootName, true);

			const int fleId = 1;
			BFile bFile = new BFile (_context);
			File file = bFile.CreateFile (node.Id, null, null, fleId, null);
			bFile.Publish (file.Id);
			List<File> files = (List<File>) bFile.GetFiles (node.Id, null, null, null, true, true, true);
			Assert.IsNotNull (files, "Files returned null");
			Assert.IsNotEmpty (files, "Files returned empty. Should be 1.");
			file = files [0];
			Assert.AreEqual (fleId, file.FleId, "Base file id not as expected");
			Assert.AreEqual (node.Id, file.NdeId, "Node id not as expected");
			Assert.AreEqual (node, file.Node, "Node not as expected");
		}

		[Test, Description ("Creates and fetches file from node with category. Compare results"), Category ("File")]
		public void CreateAndFetchFileFromNodeCategory ()
		{
			const string rootName = "opq";
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, rootName, true);

			const int fleId = 1;
			const string fileCategoryName = "File Category";
			BFile bFile = new BFile (_context);
			FileCategory fileCategory = bFile.CreateFileCategory (fileCategoryName);
			File file = bFile.CreateFile (node.Id, null, null, fleId, fileCategory);
			bFile.Publish (file.Id);
			List<File> files = (List<File>) bFile.GetFiles (node.Id, null, null, fileCategory.Id, true, true, true);
			Assert.IsNotNull (files, "Files returned null");
			Assert.IsNotEmpty (files, "Files returned empty. Should be 1.");
			file = files [0];
			Assert.AreEqual (fleId, file.FleId, "Base file id not as expected");
			Assert.AreEqual (node.Id, file.NdeId, "Node id not as expected");
			Assert.AreEqual (node, file.Node, "Node not as expected");
			Assert.AreEqual (fileCategory.Id, file.FleCatId, "File-category not as excpected");
		}

		[Test, Description ("Deletes a file")]
		public void DeleteFile ()
		{
			const string rootName = "opq";
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, rootName, false);

			const int fleId = 1;
			const string fileCategoryName = "File Category";
			BFile bFile = new BFile (_context);
			FileCategory fileCategory = bFile.CreateFileCategory (fileCategoryName);
			File file = bFile.CreateFile (node.Id, null, null, fleId, fileCategory);
			bFile.DeleteFile (file.Id, false);
			List<File> files = (List<File>) bFile.GetFiles (node.Id, null, null, null, true, true, true);
			Assert.IsEmpty (files, "No files should be returned");
		}
	}
}
