using System;

namespace Spinit.Wp.Synologen.Autogiro.Writers
{
	public class BaseWriter
	{
		protected static string Pad(int totalWidth)
		{
			return String.Empty.PadLeft(totalWidth,' ');
		}
	}
}