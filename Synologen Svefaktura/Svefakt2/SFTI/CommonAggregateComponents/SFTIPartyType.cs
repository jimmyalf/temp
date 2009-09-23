using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("Party", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTIPartyType {
    
		private List<SFTIPartyIdentificationType> partyIdentificationField = new List<SFTIPartyIdentificationType>();
    
		private List<NameType1> partyNameField = new List<NameType1>();
    
		private SFTIAddressType addressField;
    
		private List<SFTIPartyTaxSchemeType> partyTaxSchemeField = new List<SFTIPartyTaxSchemeType>();
    
		private SFTIContactType contactField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("PartyIdentification")]
		public List<SFTIPartyIdentificationType> PartyIdentification {
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
		public SFTIAddressType Address {
			get {
				return this.addressField;
			}
			set {
				this.addressField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("PartyTaxScheme")]
		public List<SFTIPartyTaxSchemeType> PartyTaxScheme {
			get {
				return this.partyTaxSchemeField;
			}
			set {
				this.partyTaxSchemeField = value;
			}
		}
    
		/// <remarks/>
		public SFTIContactType Contact {
			get {
				return this.contactField;
			}
			set {
				this.contactField = value;
			}
		}
	}
}