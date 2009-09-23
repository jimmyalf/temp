namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("TaxSubTotal", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class TaxSubTotalType {
    
		private AmountType2 taxableAmountField;
    
		private TaxAmountType taxAmountField;
    
		private TaxCategoryType taxCategoryField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public AmountType2 TaxableAmount {
			get {
				return this.taxableAmountField;
			}
			set {
				this.taxableAmountField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public TaxAmountType TaxAmount {
			get {
				return this.taxAmountField;
			}
			set {
				this.taxAmountField = value;
			}
		}
    
		/// <remarks/>
		public TaxCategoryType TaxCategory {
			get {
				return this.taxCategoryField;
			}
			set {
				this.taxCategoryField = value;
			}
		}
	}
}