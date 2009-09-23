using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("cac:HazardousGoodsTransit", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class HazardousGoodsTransitType {
    
		private CodeType transportEmergencyCardCodeField;
    
		private CodeType packingCriteriaCodeField;
    
		private CodeType regulationCodeField;
    
		private CodeType inhalationToxicityZoneCodeField;
    
		private TemperatureType maximumTemperatureField;
    
		private TemperatureType minimumTemperatureField;
    
		/// <remarks/>
		public CodeType TransportEmergencyCardCode {
			get {
				return transportEmergencyCardCodeField;
			}
			set {
				transportEmergencyCardCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType PackingCriteriaCode {
			get {
				return packingCriteriaCodeField;
			}
			set {
				packingCriteriaCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType RegulationCode {
			get {
				return regulationCodeField;
			}
			set {
				regulationCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType InhalationToxicityZoneCode {
			get {
				return inhalationToxicityZoneCodeField;
			}
			set {
				inhalationToxicityZoneCodeField = value;
			}
		}
    
		/// <remarks/>
		public TemperatureType MaximumTemperature {
			get {
				return maximumTemperatureField;
			}
			set {
				maximumTemperatureField = value;
			}
		}
    
		/// <remarks/>
		public TemperatureType MinimumTemperature {
			get {
				return minimumTemperatureField;
			}
			set {
				minimumTemperatureField = value;
			}
		}
	}
}