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

		#region Base-User

		/// <summary>
		/// Fetches the base-user by user-id.
		/// </summary>
		/// <param name="userId">The user-id.</param>
		/// <returns>A user.</returns>
		/// <exception cref="ObjectNotFoundException">If the base-user is not found.</exception>

		public BaseUser GetUserById (int userId)
		{
			var query = from user in _dataContext.BaseUsers
			            where user.Id == userId
			            select user;

			IList<EBaseUser> users = query.ToList ();

			if (users.IsEmpty ()) {
				throw new ObjectNotFoundException (
					"Base-User not found.",
					ObjectNotFoundErrors.UserNotFound);
			}

			return EBaseUser.Convert (users.First ());
		}
		
		#endregion
	}
}
