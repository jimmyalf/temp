using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("ShipmentStage", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class ShipmentStageType {
    
		private IdentifierType idField;
    
		private CodeType transportModeCodeField;
    
		private CodeType transportMeansTypeCodeField;
    
		private CodeType transitDirectionCodeField;
    
		private PeriodType transitPeriodField;
    
		private List<PartyType> carrierPartyField = new List<PartyType>();
    
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
		public CodeType TransportModeCode {
			get {
				return this.transportModeCodeField;
			}
			set {
				this.transportModeCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType TransportMeansTypeCode {
			get {
				return this.transportMeansTypeCodeField;
			}
			set {
				this.transportMeansTypeCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType TransitDirectionCode {
			get {
				return this.transitDirectionCodeField;
			}
			set {
				this.transitDirectionCodeField = value;
			}
		}
    
		/// <remarks/>
		public PeriodType TransitPeriod {
			get {
				return this.transitPeriodField;
			}
			set {
				this.transitPeriodField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("CarrierParty")]
		public List<PartyType> CarrierParty {
			get {
				return this.carrierPartyField;
			}
			set {
				this.carrierPartyField = value;
			}
		}
	}
}