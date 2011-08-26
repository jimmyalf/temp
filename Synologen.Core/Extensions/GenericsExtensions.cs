namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class GenericsExtensions
	{
		public static string SafeToString(this object value)
		{
			return value == null ? null : value.ToString();
		}
	}
}