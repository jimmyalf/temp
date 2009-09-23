using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("PartyTaxScheme", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class PartyTaxSchemeType {
    
		private RegistrationNameType registrationNameField;
    
		private IdentifierType companyIDField;
    
		private CodeType taxLevelCodeField;
    
		private ReasonType exemptionReasonField;
    
		private AddressType registrationAddressField;
    
		private TaxSchemeType taxSchemeField;
    
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
		public CodeType TaxLevelCode {
			get {
				return this.taxLevelCodeField;
			}
			set {
				this.taxLevelCodeField = value;
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
		public AddressType RegistrationAddress {
			get {
				return this.registrationAddressField;
			}
			set {
				this.registrationAddressField = value;
			}
		}
    
		/// <remarks/>
		public TaxSchemeType TaxScheme {
			get {
				return this.taxSchemeField;
			}
			set {
				this.taxSchemeField = value;
			}
		}
	}
}