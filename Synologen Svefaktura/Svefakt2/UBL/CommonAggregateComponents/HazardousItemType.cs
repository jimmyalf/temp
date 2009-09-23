using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("HazardousItem", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class HazardousItemType {
    
		private IdentifierType idField;
    
		private PlacardNotationType placardNotationField;
    
		private PlacardEndorsementType placardEndorsementField;
    
		private InformationType additionalInformationField;
    
		private CodeType uNDGCodeField;
    
		private CodeType emergencyProceduresCodeField;
    
		private CodeType medicalFirstAidGuideCodeField;
    
		private NameType1 technicalNameField;
    
		private PartyType contactPartyField;
    
		private List<SecondaryHazardType> secondaryHazardField = new List<SecondaryHazardType>();

		private List<HazardousGoodsTransitType> hazardousGoodsTransitField = new List<HazardousGoodsTransitType>();
    
		private TemperatureType emergencyTemperatureField;
    
		private TemperatureType flashpointTemperatureField;
    
		private List<TemperatureType> additionalTemperatureField = new List<TemperatureType>();
    
		/// <remarks/>
		public IdentifierType ID {
			get {
				return idField;
			}
			set {
				idField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public PlacardNotationType PlacardNotation {
			get {
				return placardNotationField;
			}
			set {
				placardNotationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public PlacardEndorsementType PlacardEndorsement {
			get {
				return placardEndorsementField;
			}
			set {
				placardEndorsementField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public InformationType AdditionalInformation {
			get {
				return additionalInformationField;
			}
			set {
				additionalInformationField = value;
			}
		}
    
		/// <remarks/>
		public CodeType UNDGCode {
			get {
				return uNDGCodeField;
			}
			set {
				uNDGCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType EmergencyProceduresCode {
			get {
				return emergencyProceduresCodeField;
			}
			set {
				emergencyProceduresCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType MedicalFirstAidGuideCode {
			get {
				return medicalFirstAidGuideCodeField;
			}
			set {
				medicalFirstAidGuideCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public NameType1 TechnicalName {
			get {
				return technicalNameField;
			}
			set {
				technicalNameField = value;
			}
		}
    
		/// <remarks/>
		public PartyType ContactParty {
			get {
				return contactPartyField;
			}
			set {
				contactPartyField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("SecondaryHazard")]
		public List<SecondaryHazardType> SecondaryHazard {
			get {
				return secondaryHazardField;
			}
			set {
				secondaryHazardField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("HazardousGoodsTransit")]
		public List<HazardousGoodsTransitType> HazardousGoodsTransit {
			get {
				return hazardousGoodsTransitField;
			}
			set {
				hazardousGoodsTransitField = value;
			}
		}
    
		/// <remarks/>
		public TemperatureType EmergencyTemperature {
			get {
				return emergencyTemperatureField;
			}
			set {
				emergencyTemperatureField = value;
			}
		}
    
		/// <remarks/>
		public TemperatureType FlashpointTemperature {
			get {
				return flashpointTemperatureField;
			}
			set {
				flashpointTemperatureField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("AdditionalTemperature")]
		public List<TemperatureType> AdditionalTemperature {
			get {
				return additionalTemperatureField;
			}
			set {
				additionalTemperatureField = value;
			}
		}
	}
}