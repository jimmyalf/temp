using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("TaxTotal", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTITaxTotalType {
    
		private TaxAmountType totalTaxAmountField;
    
		private List<SFTITaxSubTotalType> taxSubTotalField = new List<SFTITaxSubTotalType>();
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public TaxAmountType TotalTaxAmount {
			get {
				return this.totalTaxAmountField;
			}
			set {
				this.totalTaxAmountField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("TaxSubTotal")]
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