using System.Collections.Generic;
using System.Linq;

using Spinit.Data.Linq;
using Spinit.Wpc.Synologen.OPQ.Core.Entities;
using Spinit.Wpc.Synologen.Opq.Core.Exceptions;
using Spinit.Wpc.Synologen.OPQ.Data;
using Spinit.Wpc.Synologen.OPQ.Data.Entities;

namespace Spinit.Wpc.Synologen.Opq.Data.Managers
{
	public class ExternalObjectsManager : EntityManager<WpcSynologenRepository>
	{
		private readonly WpcSynologenDataContext _dataContext;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="manager">The repository-manager.</param>
		 
		public ExternalObjectsManager (WpcSynologenRepository manager) : base (manager)
		{
			_dataContext = (WpcSynologenDataContext) Manager.Context;
		}

		#region Base User

		/// <summary>
		/// Fetches the base-user by user-id.
		/// </summary>
		/// <param name="userId">The user-id.</param>
		/// <returns>A user.</returns>
		/// <exception cref="ObjectNotFoundException">If the base-user is not found.</exception>

		public BaseUser GetUserById (int userId)
		{
			IQueryable<EBaseUser> query = from user in _dataContext.BaseUsers
			                              where user.Id == userId
			                              select user;

			IList<EBaseUser> users = query.ToList ();

			if (users.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Base-User not found.",
					ObjectNotFoundErrors.BaseUserNotFound);
			}

			return EBaseUser.Convert (users.First ());
		}

		/// <summary>
		/// Checks if a user exist.
		/// </summary>
		/// <param name="userId">The user-id.</param>
		/// <exception cref="ObjectNotFoundException">If the base-user is not found.</exception>

		public void CheckUserExist (int userId)
		{
			GetUserById (userId);
		}
		
		#endregion

		#region Base File

		/// <summary>
		/// Fetches the base-file by file-id.
		/// </summary>
		/// <param name="fileId">The file-id.</param>
		/// <returns>A file.</returns>
		/// <exception cref="ObjectNotFoundException">If the base-file is not found.</exception>

		public BaseFile GetFileById (int fileId)
		{
			IQueryable<EBaseFile> query = from file in _dataContext.BaseFiles
			                              where file.Id == fileId
			                              select file;

			IList<EBaseFile> files = query.ToList ();

			if (files.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Base-file not found.",
					ObjectNotFoundErrors.BaseFileNotFound);
			}

			return EBaseFile.Convert (files.First ());
		}

		/// <summary>
		/// Checks if a file exist.
		/// </summary>
		/// <param name="fileId">The file-id.</param>
		/// <exception cref="ObjectNotFoundException">If the base-file is not found.</exception>

		public void CheckFileExist (int fileId)
		{
			GetFileById (fileId);
		}

		#endregion

		#region Synologen Shop

		/// <summary>
		/// Fetches the shop by shop-id.
		/// </summary>
		/// <param name="shopId">The shop-id.</param>
		/// <returns>A shop.</returns>
		/// <exception cref="ObjectNotFoundException">If the shop is not found.</exception>

		public Shop GetShopById (int shopId)
		{
			IQueryable<EShop> query = from shop in _dataContext.Shops
			                          where shop.Id == shopId
			                          select shop;

			IList<EShop> shops = query.ToList ();

			if (shops.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Shop not found.",
					ObjectNotFoundErrors.ShopNotFound);
			}

			return EShop.Convert (shops.First ());
		}

		/// <summary>
		/// Checks if a shop exist.
		/// </summary>
		/// <param name="shopId">The shop-id.</param>
		/// <exception cref="ObjectNotFoundException">If the shop is not found.</exception>

		public void CheckShopExist (int shopId)
		{
			GetShopById (shopId);
		}

		#endregion
		
		#region Synologen Concern

		/// <summary>
		/// Fetches the concern by concern-id.
		/// </summary>
		/// <param name="concernId">The concern-id.</param>
		/// <returns>A concern.</returns>
		/// <exception cref="ObjectNotFoundException">If the concern is not found.</exception>

		public Concern GetConcernById (int concernId)
		{
			IQueryable<EConcern> query = from concern in _dataContext.Concerns
			                             where concern.Id == concernId
			                             select concern;

			IList<EConcern> concerns = query.ToList ();

			if (concerns.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Concern not found.",
					ObjectNotFoundErrors.ConcernNotFound);
			}

			return EConcern.Convert (concerns.First ());
		}

		/// <summary>
		/// Checks if a concern exist.
		/// </summary>
		/// <param name="concernId">The concern-id.</param>
		/// <exception cref="ObjectNotFoundException">If the concern is not found.</exception>

		public void CheckConcernExist (int concernId)
		{
			GetConcernById (concernId);
		}

		#endregion
	}
}
