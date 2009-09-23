using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("HazardousItem", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class HazardousItemType {
    
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
				return this.idField;
			}
			set {
				this.idField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public PlacardNotationType PlacardNotation {
			get {
				return this.placardNotationField;
			}
			set {
				this.placardNotationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public PlacardEndorsementType PlacardEndorsement {
			get {
				return this.placardEndorsementField;
			}
			set {
				this.placardEndorsementField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public InformationType AdditionalInformation {
			get {
				return this.additionalInformationField;
			}
			set {
				this.additionalInformationField = value;
			}
		}
    
		/// <remarks/>
		public CodeType UNDGCode {
			get {
				return this.uNDGCodeField;
			}
			set {
				this.uNDGCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType EmergencyProceduresCode {
			get {
				return this.emergencyProceduresCodeField;
			}
			set {
				this.emergencyProceduresCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType MedicalFirstAidGuideCode {
			get {
				return this.medicalFirstAidGuideCodeField;
			}
			set {
				this.medicalFirstAidGuideCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public NameType1 TechnicalName {
			get {
				return this.technicalNameField;
			}
			set {
				this.technicalNameField = value;
			}
		}
    
		/// <remarks/>
		public PartyType ContactParty {
			get {
				return this.contactPartyField;
			}
			set {
				this.contactPartyField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("SecondaryHazard")]
		public List<SecondaryHazardType> SecondaryHazard {
			get {
				return this.secondaryHazardField;
			}
			set {
				this.secondaryHazardField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("HazardousGoodsTransit")]
		public List<HazardousGoodsTransitType> HazardousGoodsTransit {
			get {
				return this.hazardousGoodsTransitField;
			}
			set {
				this.hazardousGoodsTransitField = value;
			}
		}
    
		/// <remarks/>
		public TemperatureType EmergencyTemperature {
			get {
				return this.emergencyTemperatureField;
			}
			set {
				this.emergencyTemperatureField = value;
			}
		}
    
		/// <remarks/>
		public TemperatureType FlashpointTemperature {
			get {
				return this.flashpointTemperatureField;
			}
			set {
				this.flashpointTemperatureField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("AdditionalTemperature")]
		public List<TemperatureType> AdditionalTemperature {
			get {
				return this.additionalTemperatureField;
			}
			set {
				this.additionalTemperatureField = value;
			}
		}
	}
}