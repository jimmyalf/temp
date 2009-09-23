namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("AllowanceCharge", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTIInvoiceLineAllowanceCharge {
    
		private ChargeIndicatorType chargeIndicatorField;
    
		private AmountType2 amountField;
    
		private AmountType2 allowanceChargeBaseAmountField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ChargeIndicatorType ChargeIndicator {
			get {
				return this.chargeIndicatorField;
			}
			set {
				this.chargeIndicatorField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
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