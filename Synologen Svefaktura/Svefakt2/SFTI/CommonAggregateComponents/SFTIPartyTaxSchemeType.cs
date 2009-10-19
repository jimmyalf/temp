using Spinit.Wpc.Synologen.Svefaktura.CustomEnumerations;
using Spinit.Wpc.Synologen.Svefaktura.CustomTypes;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("PartyTaxScheme", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public class SFTIPartyTaxSchemeType {
    
		private RegistrationNameType registrationNameField;
    
		private IdentifierType companyIDField;
    
		private ReasonType exemptionReasonField;
    
		private SFTIAddressType registrationAddressField;
    
		private SFTITaxSchemeType taxSchemeField;
    
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
		[PropertyValidationRule("SFTIPartyTaxSchemeType.CompanyID is missing.", ValidationType.RequiredNotNull)]
		public IdentifierType CompanyID {
			get {
				return companyIDField;
			}
			set {
				companyIDField = value;
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
		public SFTIAddressType RegistrationAddress {
			get {
				return registrationAddressField;
			}
			set {
				registrationAddressField = value;
			}
		}
    
		/// <remarks/>
		public SFTITaxSchemeType TaxScheme {
			get {
				return taxSchemeField;
			}
			set {
				taxSchemeField = value;
			}
		}
	}
}