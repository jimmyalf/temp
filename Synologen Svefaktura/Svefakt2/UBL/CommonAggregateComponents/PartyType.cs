using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("CarrierParty", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class PartyType {
    
		private MarkCareIndicatorType markCareIndicatorField;
    
		private MarkAttentionIndicatorType markAttentionIndicatorField;
    
		private List<PartyIdentificationType> partyIdentificationField = new List<PartyIdentificationType>();
    
		private List<NameType1> partyNameField = new List<NameType1>();
    
		private AddressType addressField;
    
		private List<PartyTaxSchemeType> partyTaxSchemeField = new List<PartyTaxSchemeType>();
    
		private ContactType contactField;
    
		private LanguageType languageField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public MarkCareIndicatorType MarkCareIndicator {
			get {
				return this.markCareIndicatorField;
			}
			set {
				this.markCareIndicatorField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public MarkAttentionIndicatorType MarkAttentionIndicator {
			get {
				return this.markAttentionIndicatorField;
			}
			set {
				this.markAttentionIndicatorField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("PartyIdentification")]
		public List<PartyIdentificationType> PartyIdentification {
			get {
				return this.partyIdentificationField;
			}
			set {
				this.partyIdentificationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItemAttribute("Name", Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0", IsNullable=false)]
		public List<NameType1> PartyName {
			get {
				return this.partyNameField;
			}
			set {
				this.partyNameField = value;
			}
		}
    
		/// <remarks/>
		public AddressType Address {
			get {
				return this.addressField;
			}
			set {
				this.addressField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("PartyTaxScheme")]
		public List<PartyTaxSchemeType> PartyTaxScheme {
			get {
				return this.partyTaxSchemeField;
			}
			set {
				this.partyTaxSchemeField = value;
			}
		}
    
		/// <remarks/>
		public ContactType Contact {
			get {
				return this.contactField;
			}
			set {
				this.contactField = value;
			}
		}
    
		/// <remarks/>
		public LanguageType Language {
			get {
				return this.languageField;
			}
			set {
				this.languageField = value;
			}
		}
	}
}