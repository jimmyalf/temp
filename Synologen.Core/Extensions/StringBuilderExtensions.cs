using System;
using System.Text;

namespace Spinit.Wpc.Synologen.Core.Extensions
{
	public static class StringBuilderExtensions
	{
		public static StringBuilder AppendLine(this StringBuilder builder, string format, params object[] args)
		{
			return builder.AppendFormat(format, args).Append(Environment.NewLine);
		}
	}
}