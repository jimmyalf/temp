using System;

namespace Synologen.LensSubscription.Autogiro.FileIO.Test.Helpers
{
	public static class StringExtensions
	{
		public static string AppendPaths(params string[] pathArray)
		{
			return String.Empty.AppendPaths(pathArray);
		}

		public static string AppendPaths(this string root, params string[] pathArray)
		{
			if(root == null) return null;
			var returnString = root;
			foreach (var pathItem in pathArray)
			{
				if(String.IsNullOrEmpty(pathItem)) continue;
				returnString = String.Concat(returnString.TrimEnd('\\'), "\\", pathItem.TrimStart('\\'));
			}
			return returnString;
		}
	}
}