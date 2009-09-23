namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable()]
	[System.Diagnostics.DebuggerStepThrough()]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("LegalTotal", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTILegalTotalType {
    
		private ExtensionTotalAmountType lineExtensionTotalAmountField;
    
		private TotalAmountType taxExclusiveTotalAmountField;
    
		private TotalAmountType taxInclusiveTotalAmountField;
    
		private AmountType2 roundOffAmountField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ExtensionTotalAmountType LineExtensionTotalAmount {
			get {
				return this.lineExtensionTotalAmountField;
			}
			set {
				this.lineExtensionTotalAmountField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public TotalAmountType TaxExclusiveTotalAmount {
			get {
				return this.taxExclusiveTotalAmountField;
			}
			set {
				this.taxExclusiveTotalAmountField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public TotalAmountType TaxInclusiveTotalAmount {
			get {
				return this.taxInclusiveTotalAmountField;
			}
			set {
				this.taxInclusiveTotalAmountField = value;
			}
		}
    
		/// <remarks/>
		public AmountType2 RoundOffAmount {
			get {
				return this.roundOffAmountField;
			}
			set {
				this.roundOffAmountField = value;
			}
		}
	}
}