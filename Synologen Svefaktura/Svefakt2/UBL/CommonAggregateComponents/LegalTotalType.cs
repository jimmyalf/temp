namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("LegalTotal", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class LegalTotalType {
    
		private ExtensionTotalAmountType lineExtensionTotalAmountField;
    
		private TotalAmountType taxExclusiveTotalAmountField;
    
		private TotalAmountType taxInclusiveTotalAmountField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ExtensionTotalAmountType LineExtensionTotalAmount {
			get {
				return this.lineExtensionTotalAmountField;
			}
			set {
				this.lineExtensionTotalAmountField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public TotalAmountType TaxExclusiveTotalAmount {
			get {
				return this.taxExclusiveTotalAmountField;
			}
			set {
				this.taxExclusiveTotalAmountField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public TotalAmountType TaxInclusiveTotalAmount {
			get {
				return this.taxInclusiveTotalAmountField;
			}
			set {
				this.taxInclusiveTotalAmountField = value;
			}
		}
	}
}