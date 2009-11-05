using Spinit.Wpc.Synologen.OPQ.Core;

namespace Spinit.Wpc.Synologen.OPQ.Data.Test
{
	internal static class PropertyValues
	{
		// General
		public const int NodeId = 1;				// Test node-id from Test-data
		public const int FileCategoryId = 1;		// Test file-category-id from Test-data
		public const int FileId = 1;				// Test file-id (existing).
		public const int UserId = 1;				// Test user-id (existing).
		public const string UserName = "Admin";		// Test user-name (existing).

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
	}
}
