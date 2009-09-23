using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("TaxTotal", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class TaxTotalType {
    
		private TaxAmountType totalTaxAmountField;
    
		private List<TaxSubTotalType> taxSubTotalField = new List<TaxSubTotalType>();
    
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
		public List<TaxSubTotalType> TaxSubTotal {
			get {
				return this.taxSubTotalField;
			}
			set {
				this.taxSubTotalField = value;
			}
		}
	}
}