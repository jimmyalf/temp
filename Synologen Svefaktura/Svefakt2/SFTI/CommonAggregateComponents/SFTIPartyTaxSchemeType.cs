using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("PartyTaxScheme", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTIPartyTaxSchemeType {
    
		private RegistrationNameType registrationNameField;
    
		private IdentifierType companyIDField;
    
		private ReasonType exemptionReasonField;
    
		private SFTIAddressType registrationAddressField;
    
		private SFTITaxSchemeType taxSchemeField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public RegistrationNameType RegistrationName {
			get {
				return this.registrationNameField;
			}
			set {
				this.registrationNameField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType CompanyID {
			get {
				return this.companyIDField;
			}
			set {
				this.companyIDField = value;
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
		public SFTIAddressType RegistrationAddress {
			get {
				return this.registrationAddressField;
			}
			set {
				this.registrationAddressField = value;
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