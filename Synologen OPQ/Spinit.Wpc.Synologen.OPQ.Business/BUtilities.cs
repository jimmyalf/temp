using System;
using System.Reflection;
using System.Resources;

using Spinit.Wpc.Utility.Core;

namespace Spinit.Wpc.Synologen.OPQ.Business
{
	public class BUtilities
	{
		#region Resources

		/// <summary>
		/// Gets a string resource from the ErrorText resource file.
		/// </summary>
		/// <param name="key">The key name of the resource to return</param>
		/// <param name="context">The context.</param>
		/// <returns>Return a string resource if the specified key was found</returns>

		public static string GetResourceString(string key, Context context)
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

		public static bool HasRole (string role)
		{
			return true;
		}

		#endregion
	}
}
