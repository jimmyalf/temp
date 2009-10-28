using System.Threading;

using NUnit.Framework;

using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.OPQ.Data.Test.Properties;

namespace Spinit.Wpc.Synologen.OPQ.Data.Test
{
	[TestFixture, Description ("The unit tests for the data file-manager.")]
	public class FileManagerTests
	{
		private Configuration _configuration;
		private Context _context;
		private const int NodeId = 1;
		private const int FileCategoryId = 1;
		private const int FileId = 1;

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

		[Test, Explicit, Description ("Creates, fetches, updates and deletes a file."), Category ("CruiseControl")]
		public void FileTest ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {

				// Create a new node
				synologenRepository.File.Insert (
					new File
					{
						NdeId = NodeId,
						FleId = FileId,
						FleCatId = FileCategoryId,
						IsActive = true
					});

				synologenRepository.SubmitChanges ();

				File file = synologenRepository.File.GetInsertedFile ();

				Assert.IsNotNull (file, "File is null.");

				// Fetch the node
				File fetchFile = synologenRepository.File.GetFileById (file.Id);

				Assert.IsNotNull (fetchFile, "Fetched file is null.");

				// Update node
			}
		}
		
		[Test, Explicit, Description ("Creates, fetches, updates and deletes a file-category."), Category ("CruiseControl")]
		public void FileCategoryTest ()
		{
			using (
				WpcSynologenRepository synologenRepository = WpcSynologenRepository.GetWpcSynologenRepository (_configuration, null, _context)
				) {

				// Create a new node
				synologenRepository.File.Insert (
					new FileCategory
					{
						Name = "Generated Test Category",
						IsActive = true
					});

				synologenRepository.SubmitChanges ();

				FileCategory fileCategory = synologenRepository.File.GetInsertedFileCategory ();

				Assert.IsNotNull (fileCategory, "File-category is null.");

				// Fetch the node
				FileCategory fetchFileCategory = synologenRepository.File.GetFileCategoryById (fileCategory.Id);

				Assert.IsNotNull (fetchFileCategory, "Fetched file-catgory is null.");

				// Update node
			}
		}
	}
}
