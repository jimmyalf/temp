using Spinit.Wpc.Base.Business;
using Spinit.Wpc.Base.Data;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Utility.Core;

namespace Spinit.Wpc.Synologen.OPQ.Business
{
	/// <summary>
	/// The base-business class.
	/// Encapsulates business-classes for base component.
	/// </summary>

	public class BBase
	{
		private readonly Core.Context _context;
		private readonly Configuration _configuration;

		private const string AdminShopMembers = "AdminShopMembers";

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="context">The context.</param>
	
		public BBase (Core.Context context)
		{
			_context = context;
			_configuration = Configuration.GetConfiguration (_context);
		}

		/// <summary>
		/// Checks to see if a user is a shop-admin.
		/// </summary>
		/// <param name="userId">The user-id.</param>
		/// <returns>If shop-admin=>true, otherwise false.</returns>

		public bool IsShopAdmin (int userId)
		{
			return BUtilities.HasRole (AdminShopMembers);

		}
	}
}
