using System;
using AutoMapper;

namespace Spinit.Wpc.Synologen.Presentation.Application.AutomapperMappings
{
	public class NullableDateFormatter : ValueFormatter<DateTime?>
	{
		protected override string FormatValueCore(DateTime? value)
		{
			return value.HasValue ? value.Value.ToString("yyyy-MM-dd") : String.Empty;
		}
	}
}