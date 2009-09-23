namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable()]
	[System.Diagnostics.DebuggerStepThrough()]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("TaxSubTotal", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTITaxSubTotalType {
    
		private AmountType2 taxableAmountField;
    
		private TaxAmountType taxAmountField;
    
		private SFTITaxCategoryType taxCategoryField;
    
		private TaxAmountType taxCurrencyTaxAmountField;
    
		private TaxAmountType initialInvoiceTaxAmountField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public AmountType2 TaxableAmount {
			get {
				return this.taxableAmountField;
			}
			set {
				this.taxableAmountField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public TaxAmountType TaxAmount {
			get {
				return this.taxAmountField;
			}
			set {
				this.taxAmountField = value;
			}
		}
    
		/// <remarks/>
		public SFTITaxCategoryType TaxCategory {
			get {
				return this.taxCategoryField;
			}
			set {
				this.taxCategoryField = value;
			}
		}
    
		/// <remarks/>
		public TaxAmountType TaxCurrencyTaxAmount {
			get {
				return this.taxCurrencyTaxAmountField;
			}
			set {
				this.taxCurrencyTaxAmountField = value;
			}
		}
    
		/// <remarks/>
		public TaxAmountType InitialInvoiceTaxAmount {
			get {
				return this.initialInvoiceTaxAmountField;
			}
			set {
				this.initialInvoiceTaxAmountField = value;
			}
		}
	}
}