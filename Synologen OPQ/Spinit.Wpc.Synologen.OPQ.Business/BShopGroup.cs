using System.Collections.Generic;
using System.IO;
using System.Linq;

using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.OPQ.Core.Exceptions;
using Spinit.Wpc.Synologen.OPQ.Data;
using File = Spinit.Wpc.Synologen.OPQ.Core.Entities.File;

namespace Spinit.Wpc.Synologen.OPQ.Business
{
	public class BShopGroup
	{
		private readonly Context _context;
		private readonly Configuration _configuration;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="context">The context.</param>

		public BShopGroup (Context context)
		{
			_context = context;
			_configuration = Configuration.GetConfiguration (_context);
		}

		#region Move To Group

		/// <summary>
		/// Checks to see if the shop is the first to be moved to shop-group-id.
		/// </summary>
		/// <param name="shopGroupId">The id of the shop-group.</param>
		/// <returns>If not exists=>true.</returns>

		public bool IsFirst (int shopGroupId)
		{
			using (
				WpcSynologenRepository synologenRepository
					= WpcSynologenRepository.GetWpcSynologenRepositoryNoTracking (_configuration, null, _context)
				) {
				
				try {
					IList<Shop> shops = synologenRepository.ExternalObjectsManager.GetShopsForGroup (shopGroupId);

					if ((shops != null) && shops.Any () && shops.Count == 1) {
						return true;
					}
				}
				catch (ObjectNotFoundException ex) {
					if (((ObjectNotFoundErrors) ex.ErrorCode) == ObjectNotFoundErrors.ShopNotFound) {
						return true;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Moves all documents from a shop to a shop-group.
		/// </summary>
		/// <param name="shopId">The shop-id.</param>
		/// <param name="shopGroupId">The shop-group-id.</param>

		public void MoveDocumentsToGroup (int? shopId, int? shopGroupId)
		{
			if ((shopId != null) && (shopGroupId != null)) {
				BDocument document = new BDocument (_context);

				List<Document> documents = (List<Document>) document.GetDocuments (null, shopId, null, null, null, null, false, false, false);

				if ((documents != null) && documents.Any ()) {
					documents.ForEach (
						d => {
						    Document newDocument = document.CreateDocument (d.NdeId, null, shopGroupId, d.DocTpeId, d.DocumentContent);
							if (d.ApprovedById != null) {
								document.Publish (newDocument.Id);
							}
							if (d.LockedById != null) {
								document.Lock (newDocument.Id);
							}
							else {
								document.UnLock (newDocument.Id);
							}
						});
				}
			}
		}

		/// <summary>
		/// Moves all files from a shop to a shop-group.
		/// </summary>
		/// <param name="shopId">The shop-id.</param>
		/// <param name="shopGroupId">The shop-group-id.</param>
		/// <param name="shopGroupPathRoot">The root-path of the shop-group.</param>
		/// <param name="shopPath">The shop-path.</param>
		/// <param name="shopGroupPath">The shop-group-path.</param>

		public void MoveFilesToGroup (int? shopId, int? shopGroupId, string shopGroupPathRoot, string shopPath, string shopGroupPath)
		{
			if ((shopId != null) && (shopGroupId != null)) {
				BFile file = new BFile (_context);
				Base.Data.File dFile = new Base.Data.File (Configuration.GetConfiguration (_context).ConnectionString);

				Directory.CreateDirectory (shopGroupPath);

				List<File> files = (List<File>) file.GetFiles (null, shopId, null, null, null, false, false, false);

				if ((files != null) && files.Any ()) {
					files.ForEach (
						f => {
							Utility.Core.IBaseFileRow fle = dFile.GetFile (f.FleId);

						    string orgFileName = Path.GetFileName (fle.Name);
							string fileName = orgFileName.Replace (string.Concat ("-", fle.Id), string.Empty);

						    int fileId = dFile.AddFile (
						     	fle.Name,
						     	false,
						     	fle.ContentInfo,
						     	null,
						     	fileName,
						     	_context.UserName);

							// Add id to filename
							string newFileName = string.Concat (
								Path.GetFileNameWithoutExtension (fileName),
								"-",
								fileId,
								Path.GetExtension (fileName));
							
							System.IO.File.Copy (Path.Combine (shopPath, orgFileName), Path.Combine (shopGroupPath, newFileName));
						    
							newFileName = Path.Combine (shopGroupPathRoot, newFileName).Replace (Utility.Business.Globals.FilesUrl, string.Empty);

							//Update with new id
							dFile.UpdateFile (fileId, newFileName.Replace ("//", "/"), _context.UserName);

							File newFile = file.CreateFile (f.NdeId, null, f.CncId, shopGroupId, fileId, f.FleCatId);
							if (f.ApprovedById != null) {
								file.Publish (newFile.Id);
							}
							if (f.LockedById != null) {
								file.Lock (newFile.Id);
							}
							else {
								file.Unlock (newFile.Id);
							}
					    });
				}
			}
		}

		#endregion
	}
}
