using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("HazardousGoodsTransit", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class HazardousGoodsTransitType {
    
		private CodeType transportEmergencyCardCodeField;
    
		private CodeType packingCriteriaCodeField;
    
		private CodeType regulationCodeField;
    
		private CodeType inhalationToxicityZoneCodeField;
    
		private TemperatureType maximumTemperatureField;
    
		private TemperatureType minimumTemperatureField;
    
		/// <remarks/>
		public CodeType TransportEmergencyCardCode {
			get {
				return this.transportEmergencyCardCodeField;
			}
			set {
				this.transportEmergencyCardCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType PackingCriteriaCode {
			get {
				return this.packingCriteriaCodeField;
			}
			set {
				this.packingCriteriaCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType RegulationCode {
			get {
				return this.regulationCodeField;
			}
			set {
				this.regulationCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType InhalationToxicityZoneCode {
			get {
				return this.inhalationToxicityZoneCodeField;
			}
			set {
				this.inhalationToxicityZoneCodeField = value;
			}
		}
    
		/// <remarks/>
		public TemperatureType MaximumTemperature {
			get {
				return this.maximumTemperatureField;
			}
			set {
				this.maximumTemperatureField = value;
			}
		}
    
		/// <remarks/>
		public TemperatureType MinimumTemperature {
			get {
				return this.minimumTemperatureField;
			}
			set {
				this.minimumTemperatureField = value;
			}
		}
	}
}