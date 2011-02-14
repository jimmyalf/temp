using System;

namespace Synologen.LensSubscription.Autogiro.Writers
{
	public class BaseWriter
	{
		protected static string Pad(int totalWidth)
		{
			return String.Empty.PadLeft(totalWidth,' ');
		}
	}
}