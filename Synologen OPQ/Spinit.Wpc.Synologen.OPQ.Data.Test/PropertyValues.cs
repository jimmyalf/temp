using Spinit.Wpc.Synologen.OPQ.Core;

namespace Spinit.Wpc.Synologen.OPQ.Data.Test
{
	internal static class PropertyValues
	{
		// General
		public const int NodeId = 1;				// Test node-id from Test-data
		public const int UserId = 1;				// Test user-id (existing).
		public const string UserName = "Admin";		// Test user-name (existing).

		// Nodes
		public const string NodeName = @"TestNode";
		public const string NodeNameUpdated = @"TestNode Updated";
		public const int ActiveNodesRoot = 1;
		public const int AllNodesRoot = 2;
		public const int ParentNodeId = 1;
		public const int ActiveNodesChild = 1;
		public const int AllNodesChild = 2;
		public const int ListUpId = 7;
		public const int ListCount = 4;
		public const string ListRootFirstConent = @"Test-Root1";
		public const string ListChildFirstConenent = @"Test-Root1-Child1-Child1-Child1";
		public const int MoveNodeId = 1;
		public const int MovedNodeId = 2;
		public const int NodeOrder1 = 1;
		public const int NodeOrder2 = 2;
		public const int NodeSupplierNodeId = 2;
		public const int NodeSupplierSearchNodeId = 1;
		public const int NodeSupplierCount = 1;

		// Documents
		public const DocumentTypes DocDocumentType = DocumentTypes.Routine;
		public const string DocCreateDocumentContent = @"The test document content.";
		public const string DocUpdateDocumentContent = @"Updated document test";
		public const int DocNodeIdActive = 5;
		public const int DocCountOnlyActive = 1;
		public const int DocCountAll = 2;
		public const int DocDocumentId = 1;
		public const string DocDocumentContent = @"Test-Content-1";
		public const int DocCountHistory = 3;
		public const string DocDocumentContentHistory = @"Test-Content-History-1-1";
		public const DocumentTypes DocDocumentTypeView = DocumentTypes.Routine;
		public const int DocDocumentIdView1 = 1;
		public const string DocDocumentContentView1 = @"Test-Content-1";
		public const int DocDocumentIdView2 = 2;
		public const string DocDocumentContentView2 = @"Test-Content-2";
		public const int DocDocumentIdView3 = 3;
		public const string DocDocumentContentView3 = @"Test-Content-History-3-2";
		public const int DocumentTypesNumber = 2;
		public const string DocumentTypeConent = @"Rutin";

		// File
		public const int BaseFileId = 1;
		public const int FileCategoryId = 1;
		public const int FileCategoryIdUpdated = 2;
		public const string FileCategoryName = @"Generated Test Category";
		public const string FileCategoryNameUpdated = @"Generated Test Category Updated";
		public const int FileMoveNodeId = 1;
		public const int MoveFileId = 1;
		public const int MovedFileId = 2;
		public const int FileOrder1 = 1;
		public const int FileOrder2 = 2;
		public const int NoOfCategoriesActive = 2;
		public const int NoOfCategoriesAll = 3;
		public const int NoOfFilesCategoryNode = 3;
		public const int NoOfFilesCategoryNodeAll = 4;
		public const string FileCategoryContent = @"Test Category 1";
		public const int FetchedFileId = 1;
	}
}
