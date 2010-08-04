using System;

namespace Spinit.Wpc.Synologen.Presentation.App
{
	public class ModelDomainMappingAttribute : Attribute
	{
		public string DomainPropertyName { get; set; }
	}
}