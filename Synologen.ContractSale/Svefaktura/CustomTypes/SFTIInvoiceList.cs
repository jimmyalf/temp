using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;

namespace Spinit.Wpc.Synologen.Svefaktura.CustomTypes{
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.Xml.Serialization.XmlRoot("Invoices", Namespace="urn:sfti:documents:BasicInvoice:1:0", IsNullable=false)]
	public class SFTIInvoiceList{
		[System.Xml.Serialization.XmlElement("Invoice")]
		public List<SFTIInvoiceType> Invoices { get; set; }
		
	}
}