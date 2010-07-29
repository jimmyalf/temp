using System.Collections.Specialized;

namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class ListExtensions
	{
		public static bool HasKey(this NameObjectCollectionBase collection, string key)
		{
			foreach (var keyItem in collection.Keys)
			{
				if(keyItem.Equals(key)) return true;
			}
			return false;
		}
	}
}