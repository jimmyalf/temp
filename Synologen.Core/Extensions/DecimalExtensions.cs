namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class DecimalExtensions
	{
		public static decimal Invert(this decimal value)
		{
			return value * -1;
		}
	}
}