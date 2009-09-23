using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("TaxSubTotal", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public class SFTITaxSubTotalType {
    
		private AmountType taxableAmountField;
    
		private TaxAmountType taxAmountField;
    
		private SFTITaxCategoryType taxCategoryField;
    
		private TaxAmountType taxCurrencyTaxAmountField;
    
		private TaxAmountType initialInvoiceTaxAmountField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public AmountType TaxableAmount {
			get {
				return taxableAmountField;
			}
			set {
				taxableAmountField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public TaxAmountType TaxAmount {
			get {
				return taxAmountField;
			}
			set {
				taxAmountField = value;
			}
		}
    
		/// <remarks/>
		public SFTITaxCategoryType TaxCategory {
			get {
				return taxCategoryField;
			}
			set {
				taxCategoryField = value;
			}
		}
    
		/// <remarks/>
		public TaxAmountType TaxCurrencyTaxAmount {
			get {
				return taxCurrencyTaxAmountField;
			}
			set {
				taxCurrencyTaxAmountField = value;
			}
		}
    
		/// <remarks/>
		public TaxAmountType InitialInvoiceTaxAmount {
			get {
				return initialInvoiceTaxAmountField;
			}
			set {
				initialInvoiceTaxAmountField = value;
			}
		}
	}
}