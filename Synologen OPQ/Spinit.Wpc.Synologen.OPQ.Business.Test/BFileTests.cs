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

		//[Test, Description ("Creates a file-category."), Category ("File")]
		//public void CreateFileCategory ()
		//{
		//    const string fileCategoryName = "File Category";
		//    BFile bFile = new BFile (_context);
		//    FileCategory fileCategory = bFile.CreateFileCategory (fileCategoryName);
		//    Assert.AreEqual (fileCategoryName, fileCategory.Name, "File-category name not as excpected");
		//}

		//[Test, Description ("Creates a file-category and fetches. Compare results"), Category ("File")]
		//public void CreateAndFetchFileCategory ()
		//{
		//    const string fileCategoryName = "File Category";
		//    BFile bFile = new BFile (_context);
		//    FileCategory fileCategory = bFile.CreateFileCategory (fileCategoryName);
		//    fileCategory = bFile.GetFileCategory (fileCategory.Id);
		//    Assert.AreEqual (fileCategoryName, fileCategory.Name, "File-category name not as excpected");
		//}

		//[Test, Description ("Creates and updates a file-category. Compare results"), Category ("File")]
		//public void CreateAndUpdatesFileCategory ()
		//{
		//    const string fileCategoryName = "File Category";
		//    const string newFileCategoryName = "Update File Category";
		//    BFile bFile = new BFile (_context);
		//    FileCategory fileCategory = bFile.CreateFileCategory (fileCategoryName);
		//    fileCategory = bFile.GetFileCategory (fileCategory.Id);
		//    fileCategory = bFile.ChangeFileCategory (fileCategory.Id, newFileCategoryName);
		//    Assert.AreEqual (newFileCategoryName, fileCategory.Name, "File-category name not as excpected");
		//}

		//[Test, Description ("Deletes a file-category")]
		//public void DeleteFileCategory ()
		//{
		//    const string fileCategoryName = "File Category";
		//    BFile bFile = new BFile (_context);
		//    FileCategory fileCategory = bFile.CreateFileCategory (fileCategoryName);
		//    bFile.DeleteFileCategory (fileCategory.Id, false);
		//    List<FileCategory> fileCategories = (List<FileCategory>) bFile.GetFileCategories (true);
		//    Assert.IsEmpty (fileCategories, "No file-categories should be returned");
		//}

		[Test, Description ("Creates a file."), Category ("File")]
		public void CreateFile ()
		{
			const string rootName = "opq";
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, rootName, false);

			const int fleId = 1;
			BFile bFile = new BFile (_context);
			File file = bFile.CreateFile (node.Id, null,null, fleId, FileCategories.SystemRoutineDocuments);
			Assert.AreEqual (fleId, file.FleId, "Base file id not as expected");
			Assert.AreEqual (node.Id, file.NdeId, "Node id not as expected");
			Assert.AreEqual (FileCategories.SystemRoutineDocuments, file.FleCatId, "File-category not as excpected");
		}

		[Test, Description ("Creates a file and fetches. Compare results"), Category ("File")]
		public void CreateAndFetchFile ()
		{
			const string rootName = "opq";
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, rootName, false);

			const int fleId = 1;
			BFile bFile = new BFile (_context);
			File file = bFile.CreateFile (node.Id, null, null, fleId, FileCategories.SystemRoutineDocuments );
			file = bFile.GetFile (file.Id, true);
			Assert.AreEqual (fleId, file.FleId, "Base file id not as expected");
			Assert.AreEqual (node.Id, file.NdeId, "Node id not as expected");
			Assert.AreEqual (node, file.Node, "Node not as expected");
			Assert.AreEqual (FileCategories.SystemRoutineDocuments, file.FleCatId, "File-category not as excpected");
		}

		[Test, Description ("Creates and updates a file. Compare results"), Category ("File")]
		public void CreateAndUpdatesFile ()
		{
			const string rootName = "opq";
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, rootName, false);

			const int fleId = 1;
			const int newFleId = 2;
			BFile bFile = new BFile (_context);
			File file = bFile.CreateFile(node.Id, null, null, fleId, FileCategories.SystemRoutineDocuments);
			file = bFile.ChangeFile (file.Id, newFleId);

			Assert.AreEqual (newFleId, file.FleId, "Base file id not as expected");
			Assert.AreEqual (node.Id, file.NdeId, "Node id not as expected");
			Assert.AreEqual (node, file.Node, "Node not as expected");
			Assert.AreEqual (FileCategories.SystemRoutineDocuments, file.FleCatId, "File-category not as excpected");
		}

		[Test, Description ("Creates and fetches file from node without category. Compare results"), Category ("File")]
		public void CreateAndFetchFileFromNode ()
		{
			const string rootName = "opq";
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, rootName, true);

			const int fleId = 1;
			BFile bFile = new BFile (_context);
			File file = bFile.CreateFile(node.Id, null, null, fleId, FileCategories.SystemRoutineDocuments);
			bFile.Publish (file.Id);
			bFile.Unlock (file.Id);
			List<File> files = (List<File>) bFile.GetFiles (node.Id, null, null, null, true, true, true);
			Assert.IsNotNull (files, "Files returned null");
			Assert.IsNotEmpty (files, "Files returned empty. Should be 1.");
			file = files [0];
			Assert.IsNotNull (file.BaseFile, "Base-file returned null");
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
			BFile bFile = new BFile (_context);
			File file = bFile.CreateFile(node.Id, null, null, fleId, FileCategories.SystemRoutineDocuments);
			bFile.Publish (file.Id);
			bFile.Unlock (file.Id);
			List<File> files = (List<File>) bFile.GetFiles (node.Id, null, null, FileCategories.SystemRoutineDocuments, true, true, true);
			Assert.IsNotNull (files, "Files returned null");
			Assert.IsNotEmpty (files, "Files returned empty. Should be 1.");
			file = files [0];
			Assert.AreEqual (fleId, file.FleId, "Base file id not as expected");
			Assert.AreEqual (node.Id, file.NdeId, "Node id not as expected");
			Assert.AreEqual (node, file.Node, "Node not as expected");
			Assert.AreEqual (FileCategories.SystemRoutineDocuments, file.FleCatId, "File-category not as excpected");
		}

		[Test, Description ("Deletes a file")]
		public void DeleteFile ()
		{
			const string rootName = "opq";
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, rootName, false);

			const int fleId = 1;
			BFile bFile = new BFile (_context);
			File file = bFile.CreateFile(node.Id, null, null, fleId, FileCategories.SystemRoutineDocuments);
			bFile.DeleteFile (file.Id, false);
			List<File> files = (List<File>) bFile.GetFiles (node.Id, null, null, null, true, true, true);
			Assert.IsEmpty (files, "No files should be returned");
		}

		[Test, Description("Creates a file and moves up. Compare results"), Category("File")]
		public void CreateAndMoveFileUp()
		{
			const string rootName = "opq";
			var bNode = new BNode(_context);
			Node node = bNode.CreateNode(null, rootName, false);

			const int fleId = 1;
			var bFile = new BFile(_context);
			File file1 = bFile.CreateFile(node.Id, null, null, fleId, FileCategories.SystemRoutineDocuments);
			File file2 = bFile.CreateFile(node.Id, null, null, fleId, FileCategories.SystemRoutineDocuments);
			File file3 = bFile.CreateFile(node.Id, null, null, fleId, FileCategories.SystemRoutineDocuments);
			file1 = bFile.GetFile(file1.Id, false);
			file2 = bFile.GetFile(file2.Id, false);
			file3 = bFile.GetFile(file3.Id, false);
			Assert.AreEqual(1, file1.Order, "Order on file1 not as expected.");
			Assert.AreEqual(2, file2.Order, "Order on file2 not as expected.");
			Assert.AreEqual(3, file3.Order, "Order on file3 not as expected.");
			bFile.MoveFile(FileMoveActions.MoveUp, file2.Id);
			file1 = bFile.GetFile (file1.Id, false);
			file2 = bFile.GetFile (file2.Id, false);
			file3 = bFile.GetFile (file3.Id, false);
			Assert.AreEqual (2, file1.Order, "Order on file1 not as expected (moved).");
			Assert.AreEqual(1, file2.Order, "Order on file2 not as expected (moved).");
			Assert.AreEqual(3, file3.Order, "Order on file3 not as expected (moved).");
		}

		[Test, Description("Creates a file and moves down. Compare results"), Category("File")]
		public void CreateAndMoveFileDown()
		{
			const string rootName = "opq";
			var bNode = new BNode(_context);
			Node node = bNode.CreateNode(null, rootName, false);

			const int fleId = 1;
			var bFile = new BFile(_context);
			File file1 = bFile.CreateFile(node.Id, null, null, fleId, FileCategories.SystemRoutineDocuments);
			File file2 = bFile.CreateFile(node.Id, null, null, fleId, FileCategories.SystemRoutineDocuments);
			File file3 = bFile.CreateFile(node.Id, null, null, fleId, FileCategories.SystemRoutineDocuments);
			file1 = bFile.GetFile(file1.Id, false);
			file2 = bFile.GetFile(file2.Id, false);
			file3 = bFile.GetFile(file3.Id, false);
			Assert.AreEqual(1, file1.Order, "Order on file1 not as expected");
			Assert.AreEqual(2, file2.Order, "Order on file2 not as expected");
			Assert.AreEqual(3, file3.Order, "Order on file3 not as expected");
			bFile.MoveFile(FileMoveActions.MoveDown, file2.Id);
			file1 = bFile.GetFile (file1.Id, false);
			file2 = bFile.GetFile (file2.Id, false);
			file3 = bFile.GetFile (file3.Id, false);
			Assert.AreEqual (1, file1.Order, "Order on file1 not as expected");
			Assert.AreEqual(3, file2.Order, "Order on file2 not as expected");
			Assert.AreEqual(2, file3.Order, "Order on file3 not as expected");
		}

		[Test, Description("Create files on shop and moves up. Compare results"), Category("File")]
		public void CreateAndMoveFileUpShop()
		{
			const string rootName = "opq";
			var bNode = new BNode(_context);
			Node node = bNode.CreateNode(null, rootName, false);

			const int fleId = 1;
			const int shopId = 1;
			var bFile = new BFile(_context);
			File file1 = bFile.CreateFile(node.Id, null, null, fleId, FileCategories.SystemRoutineDocuments);
			File file2 = bFile.CreateFile(node.Id, shopId, null, fleId, FileCategories.ShopRoutineDocuments);
			File file3 = bFile.CreateFile(node.Id, shopId, null, fleId, FileCategories.ShopRoutineDocuments);
			file1 = bFile.GetFile(file1.Id, false);
			file2 = bFile.GetFile(file2.Id, false);
			file3 = bFile.GetFile(file3.Id, false);
			Assert.AreEqual(1, file1.Order, "Order on file1 not as expected");
			Assert.AreEqual(1, file2.Order, "Order on file2 not as expected");
			Assert.AreEqual(2, file3.Order, "Order on file3 not as expected");
			bFile.MoveFile(FileMoveActions.MoveUp, file3.Id);
			file1 = bFile.GetFile (file1.Id, false);
			file2 = bFile.GetFile (file2.Id, false);
			file3 = bFile.GetFile (file3.Id, false);
			Assert.AreEqual (1, file1.Order, "Order on file1 not as expected");
			Assert.AreEqual(2, file2.Order, "Order on file2 not as expected");
			Assert.AreEqual(1, file3.Order, "Order on file3 not as expected");
		}

		[Test, Description("Create files on shop and moves down. Compare results"), Category("File")]
		public void CreateAndMoveFileDownShop()
		{
			const string rootName = "opq";
			var bNode = new BNode(_context);
			Node node = bNode.CreateNode(null, rootName, false);

			const int fleId = 1;
			const int shopId = 1;
			var bFile = new BFile(_context);
			File file1 = bFile.CreateFile(node.Id, null, null, fleId, FileCategories.SystemRoutineDocuments);
			File file2 = bFile.CreateFile(node.Id, shopId, null, fleId, FileCategories.ShopRoutineDocuments);
			File file3 = bFile.CreateFile(node.Id, shopId, null, fleId, FileCategories.ShopRoutineDocuments);
			file1 = bFile.GetFile(file1.Id, false);
			file2 = bFile.GetFile(file2.Id, false);
			file3 = bFile.GetFile(file3.Id, false);
			Assert.AreEqual(1, file1.Order, "Order on file1 not as expected");
			Assert.AreEqual(1, file2.Order, "Order on file2 not as expected");
			Assert.AreEqual(2, file3.Order, "Order on file3 not as expected");
			bFile.MoveFile(FileMoveActions.MoveDown, file2.Id);
			file1 = bFile.GetFile (file1.Id, false);
			file2 = bFile.GetFile (file2.Id, false);
			file3 = bFile.GetFile (file3.Id, false);
			Assert.AreEqual (1, file1.Order, "Order on file1 not as expected");
			Assert.AreEqual(2, file2.Order, "Order on file2 not as expected");
			Assert.AreEqual(1, file3.Order, "Order on file3 not as expected");
		}
		
		[Test, Description ("Creates files and moves them up and down.")]
		public void CreateAndMoveFiles ()
		{
			BNode bNode = new BNode (_context);
			Node node = bNode.CreateNode (null, PropertyValues.FileNodeName, false);

			BFile bFile = new BFile (_context);

			File firstFile = bFile.CreateFile (node.Id, null, null, PropertyValues.BaseFileId, FileCategories.SystemRoutineDocuments);
			firstFile = bFile.GetFile (firstFile.Id, true);
			Assert.AreEqual (PropertyValues.FirstFileOrderOne, firstFile.Order, "First file wrong order (1).");

			File secondFile = bFile.CreateFile(node.Id, null, null, PropertyValues.BaseFileId, FileCategories.SystemRoutineDocuments);
			secondFile = bFile.GetFile (secondFile.Id, true);
			Assert.AreEqual (PropertyValues.SecondFileOrderOne, secondFile.Order, "Second file wrong order (1).");

			File thirdFile = bFile.CreateFile(node.Id, null, null, PropertyValues.BaseFileId, FileCategories.SystemRoutineDocuments);
			thirdFile = bFile.GetFile (thirdFile.Id, true);
			Assert.AreEqual (PropertyValues.ThirdFileOrderOne, thirdFile.Order, "Third file wrong order (1).");

			bFile.MoveFile (FileMoveActions.MoveDown, firstFile.Id);
		
			firstFile = bFile.GetFile (firstFile.Id, true);
			Assert.AreEqual (PropertyValues.FirstFileOrderTwo, firstFile.Order, "First file wrong order (2).");

			secondFile = bFile.GetFile (secondFile.Id, true);
			Assert.AreEqual (PropertyValues.SecondFileOrderTwo, secondFile.Order, "Second file wrong order (2).");

			thirdFile = bFile.GetFile (thirdFile.Id, true);
			Assert.AreEqual (PropertyValues.ThirdFileOrderOne, thirdFile.Order, "Third file wrong order (2).");

			bFile.MoveFile (FileMoveActions.MoveUp, firstFile.Id);
		
			firstFile = bFile.GetFile (firstFile.Id, true);
			Assert.AreEqual (PropertyValues.FirstFileOrderOne, firstFile.Order, "First file wrong order (3).");

			secondFile = bFile.GetFile (secondFile.Id, true);
			Assert.AreEqual (PropertyValues.SecondFileOrderOne, secondFile.Order, "Second file wrong order (3).");
			
			thirdFile = bFile.GetFile (thirdFile.Id, true);
			Assert.AreEqual (PropertyValues.ThirdFileOrderOne, thirdFile.Order, "Third file wrong order (3).");
		}
	}
}
