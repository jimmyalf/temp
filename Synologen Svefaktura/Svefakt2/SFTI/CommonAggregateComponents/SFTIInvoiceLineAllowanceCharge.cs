namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable()]
	[System.Diagnostics.DebuggerStepThrough()]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("AllowanceCharge", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTIInvoiceLineAllowanceCharge {
    
		private ChargeIndicatorType chargeIndicatorField;
    
		private AmountType2 amountField;
    
		private AmountType2 allowanceChargeBaseAmountField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ChargeIndicatorType ChargeIndicator {
			get {
				return this.chargeIndicatorField;
			}
			set {
				this.chargeIndicatorField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public AmountType2 Amount {
			get {
				return this.amountField;
			}
			set {
				this.amountField = value;
			}
		}
    
		/// <remarks/>
		public AmountType2 AllowanceChargeBaseAmount {
			get {
				return this.allowanceChargeBaseAmountField;
			}
			set {
				this.allowanceChargeBaseAmountField = value;
			}
		}
	}
}