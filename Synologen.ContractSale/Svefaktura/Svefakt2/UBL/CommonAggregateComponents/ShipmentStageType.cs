using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("ShipmentStage", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class ShipmentStageType {
    
		private IdentifierType idField;
    
		private CodeType transportModeCodeField;
    
		private CodeType transportMeansTypeCodeField;
    
		private CodeType transitDirectionCodeField;
    
		private PeriodType transitPeriodField;

		private List<PartyType> carrierPartyField;
    
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
		public CodeType TransportModeCode {
			get {
				return transportModeCodeField;
			}
			set {
				transportModeCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType TransportMeansTypeCode {
			get {
				return transportMeansTypeCodeField;
			}
			set {
				transportMeansTypeCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType TransitDirectionCode {
			get {
				return transitDirectionCodeField;
			}
			set {
				transitDirectionCodeField = value;
			}
		}
    
		/// <remarks/>
		public PeriodType TransitPeriod {
			get {
				return transitPeriodField;
			}
			set {
				transitPeriodField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("CarrierParty")]
		public List<PartyType> CarrierParty {
			get {
				return carrierPartyField;
			}
			set {
				carrierPartyField = value;
			}
		}
	}
}