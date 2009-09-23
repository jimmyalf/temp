using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("TaxCategory", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTITaxCategoryType {
    
		private IdentifierType idField;
    
		private PercentType1 percentField;
    
		private ReasonType exemptionReasonField;
    
		private SFTITaxSchemeType taxSchemeField;
    
		/// <remarks/>
		public IdentifierType ID {
			get {
				return this.idField;
			}
			set {
				this.idField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public PercentType1 Percent {
			get {
				return this.percentField;
			}
			set {
				this.percentField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ReasonType ExemptionReason {
			get {
				return this.exemptionReasonField;
			}
			set {
				this.exemptionReasonField = value;
			}
		}
    
		/// <remarks/>
		public SFTITaxSchemeType TaxScheme {
			get {
				return this.taxSchemeField;
			}
			set {
				this.taxSchemeField = value;
			}
		}
	}
}