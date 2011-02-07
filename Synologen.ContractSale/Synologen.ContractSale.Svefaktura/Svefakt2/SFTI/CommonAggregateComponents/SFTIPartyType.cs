using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.CustomEnumerations;
using Spinit.Wpc.Synologen.Svefaktura.CustomTypes;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("Party", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public class SFTIPartyType {

		private List<SFTIPartyIdentificationType> partyIdentificationField;

		private List<NameType> partyNameField;
    
		private SFTIAddressType addressField;

		private List<SFTIPartyTaxSchemeType> partyTaxSchemeField;
    
		private SFTIContactType contactField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("PartyIdentification")]
		public List<SFTIPartyIdentificationType> PartyIdentification {
			get {
				return partyIdentificationField;
			}
			set {
				partyIdentificationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlArrayItem("Name", Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0", IsNullable=false)]
		[PropertyValidationRule("SFTIPartyType.PartyName is missing.", ValidationType.RequiredNotNull)]
		[PropertyValidationRule("SFTIPartyType.PartyName is must contain one or more elements.", ValidationType.CollectionHasMinimumCountRequirement, 1)]
		public List<NameType> PartyName {
			get {
				return partyNameField;
			}
			set {
				partyNameField = value;
			}
		}
    
		/// <remarks/>
		[PropertyValidationRule("SFTIPartyType.Address is missing.", ValidationType.RequiredNotNull)]
		public SFTIAddressType Address {
			get {
				return addressField;
			}
			set {
				addressField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("PartyTaxScheme")]
		public List<SFTIPartyTaxSchemeType> PartyTaxScheme {
			get {
				return partyTaxSchemeField;
			}
			set {
				partyTaxSchemeField = value;
			}
		}
    
		/// <remarks/>
		public SFTIContactType Contact {
			get {
				return contactField;
			}
			set {
				contactField = value;
			}
		}
	}
}