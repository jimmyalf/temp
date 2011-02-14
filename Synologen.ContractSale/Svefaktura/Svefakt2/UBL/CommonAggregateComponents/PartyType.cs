using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("CarrierParty", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class PartyType {
    
		private MarkCareIndicatorType markCareIndicatorField;
    
		private MarkAttentionIndicatorType markAttentionIndicatorField;

		private List<PartyIdentificationType> partyIdentificationField;

		private List<NameType> partyNameField;
    
		private AddressType addressField;

		private List<PartyTaxSchemeType> partyTaxSchemeField;
    
		private ContactType contactField;
    
		private LanguageType languageField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public MarkCareIndicatorType MarkCareIndicator {
			get {
				return markCareIndicatorField;
			}
			set {
				markCareIndicatorField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public MarkAttentionIndicatorType MarkAttentionIndicator {
			get {
				return markAttentionIndicatorField;
			}
			set {
				markAttentionIndicatorField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("PartyIdentification")]
		public List<PartyIdentificationType> PartyIdentification {
			get {
				return partyIdentificationField;
			}
			set {
				partyIdentificationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItem("Name", Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0", IsNullable=false)]
		public List<NameType> PartyName {
			get {
				return partyNameField;
			}
			set {
				partyNameField = value;
			}
		}
    
		/// <remarks/>
		public AddressType Address {
			get {
				return addressField;
			}
			set {
				addressField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("PartyTaxScheme")]
		public List<PartyTaxSchemeType> PartyTaxScheme {
			get {
				return partyTaxSchemeField;
			}
			set {
				partyTaxSchemeField = value;
			}
		}
    
		/// <remarks/>
		public ContactType Contact {
			get {
				return contactField;
			}
			set {
				contactField = value;
			}
		}
    
		/// <remarks/>
		public LanguageType Language {
			get {
				return languageField;
			}
			set {
				languageField = value;
			}
		}
	}
}