using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using Spinit.Wpc.Synologen.Opq.Business;

namespace Spinit.Wpc.Synologen.OPQ.Business
{
	public class BUtilities
	{
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
	}
}
