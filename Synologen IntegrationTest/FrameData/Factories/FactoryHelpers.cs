using System.Linq;

namespace Spinit.Wpc.Synologen.Integration.Test.FrameData.Factories
{
	public static class FactoryHelpers
	{
		public static string Reverse(this string input)
		{
			return new string(input.ToCharArray().Reverse().ToArray());
		}
	}
}