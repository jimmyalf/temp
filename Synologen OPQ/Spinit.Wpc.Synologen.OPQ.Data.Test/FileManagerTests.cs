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

		#region File tests

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
						NdeId = PropertyValues.NodeId,
						FleId = PropertyValues.FileId,
						FleCatId = PropertyValues.FileCategoryId,
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

		#endregion

		#region File Category Tests

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

		#endregion
	}
}
