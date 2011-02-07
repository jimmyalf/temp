using System.Text;

namespace Spinit.Wpc.Synologen.Presentation.Site.Logic.Helpers
{
	public static class StringBuilderExtensions
	{
		public static StringBuilder AppendFormatLine(this StringBuilder builder, string format, params object[] parameters)
		{
			return builder.AppendFormat(format + "\r\n", parameters);
		}

	}
}