using System;
using AutoMapper;

namespace Spinit.Wpc.Synologen.Presentation.Application.AutomapperMappings
{
	public class StandardDateFormatter : ValueFormatter<DateTime>
	{
		protected override string FormatValueCore(DateTime value) { return value.ToString("yyyy-MM-dd"); }
	}
}