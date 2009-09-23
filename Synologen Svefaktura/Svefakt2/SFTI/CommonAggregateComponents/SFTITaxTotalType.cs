using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable()]
	[System.Diagnostics.DebuggerStepThrough()]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("TaxTotal", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTITaxTotalType {
    
		private TaxAmountType totalTaxAmountField;
    
		private List<SFTITaxSubTotalType> taxSubTotalField = new List<SFTITaxSubTotalType>();
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public TaxAmountType TotalTaxAmount {
			get {
				return this.totalTaxAmountField;
			}
			set {
				this.totalTaxAmountField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("TaxSubTotal")]
		public List<SFTITaxSubTotalType> TaxSubTotal {
			get {
				return this.taxSubTotalField;
			}
			set {
				this.taxSubTotalField = value;
			}
		}
	}
}