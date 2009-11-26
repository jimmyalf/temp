using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;
using Spinit.Wpc.Synologen.OPQ.Core;
using Spinit.Wpc.Utility.Core;

namespace Spinit.Wpc.Synologen.OPQ.Business
{
	public class BUtilities
	{
		private readonly Core.Context _context;
		private readonly Configuration _configuration;

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="context">The context.</param>

		public BUtilities(Core.Context context)
		{
			_context = context;
			_configuration = Configuration.GetConfiguration (_context);
		}


		#region Resources

		/// <summary>
		/// Gets a string resource from the ErrorText resource file.
		/// </summary>
		/// <param name="key">The key name of the resource to return</param>
		/// <param name="context">The context.</param>
		/// <returns>Return a string resource if the specified key was found</returns>

		public static string GetResourceString(string key, Core.Context context)
		{
			if (context == null)
			{
				return null;
			}

			var rm = new ResourceManager(
				"Spinit.Wpc.Synologen.OPQ.Business.ErrorText",
				Assembly.GetExecutingAssembly());
			return rm.GetString(key, context.CultureInfo);
		}

		/// <summary>
		/// Fetches the error-text for a 
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns>The error-string.</returns>

		public static string GetErrorTextString (string key)
		{
			try {
				return PropertyExtension.CreateWith<ErrorText, string> (key).Invoke (null);
			}
			catch(Exception) {
				return string.Empty;
			}
		}

		#endregion

		#region User-handling

		/// <summary>
		/// Gets the current-user.
		/// </summary>
		/// <returns>If authenticated, the current user.</returns>

		public static IBaseUserRow GetCurrentUser ()
		{
			if (CxUser.Current != null) {
				return CxUser.Current.User;
			}

			if (PublicUser.Current != null) {
				return PublicUser.Current.User;
			}

			return null;
		}

		/// <summary>
		/// Checks to see if an user has an specified role.
		/// </summary>
		/// <param name="component">The component.</param>
		/// <param name="role">The role to check for.</param>
		/// <returns>If user has role=>true, otherwise false.</returns>

		public static bool HasRole (string component, string role)
		{
			if (CxUser.Current != null) {
				return CxUser.Current.IsInRole (component, role, CxUser.Current.Location, CxUser.Current.Language);
			}

			if (PublicUser.Current != null) {
				return PublicUser.Current.IsInRole (component, role, PublicUser.Current.Location, PublicUser.Current.Language);
			}
			
			return false;
		}

		#endregion

		#region Member-handling

		public List<int> GetAllShopIdsPerMember (int memberId)
		{
			var provider = new Synologen.Data.SqlProvider(_configuration.ConnectionString);
			return provider.GetAllShopIdsPerMember(memberId);
		}

		public int GetMemberId(int userId)
		{
			var provider = new Synologen.Data.SqlProvider(_configuration.ConnectionString);
			return provider.GetMemberId(userId);
		}

		#endregion
	}
}
