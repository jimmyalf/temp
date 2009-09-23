using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("cac:PartyTaxScheme", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class PartyTaxSchemeType {
    
		private RegistrationNameType registrationNameField;
    
		private IdentifierType companyIDField;
    
		private CodeType taxLevelCodeField;
    
		private ReasonType exemptionReasonField;
    
		private AddressType registrationAddressField;
    
		private TaxSchemeType taxSchemeField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public RegistrationNameType RegistrationName {
			get {
				return registrationNameField;
			}
			set {
				registrationNameField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType CompanyID {
			get {
				return companyIDField;
			}
			set {
				companyIDField = value;
			}
		}
    
		/// <remarks/>
		public CodeType TaxLevelCode {
			get {
				return taxLevelCodeField;
			}
			set {
				taxLevelCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ReasonType ExemptionReason {
			get {
				return exemptionReasonField;
			}
			set {
				exemptionReasonField = value;
			}
		}
    
		/// <remarks/>
		public AddressType RegistrationAddress {
			get {
				return registrationAddressField;
			}
			set {
				registrationAddressField = value;
			}
		}
    
		/// <remarks/>
		public TaxSchemeType TaxScheme {
			get {
				return taxSchemeField;
			}
			set {
				taxSchemeField = value;
			}
		}
	}
}