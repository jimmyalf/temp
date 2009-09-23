using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("Shipment", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class ShipmentType {
    
		private IdentifierType idField;
    
		private CodeType priorityLevelCodeField;
    
		private CodeType handlingCodeField;
    
		private InstructionsType handlingInstructionsField;
    
		private InformationType informationField;
    
		private WeightMeasureType grossWeightMeasureField;
    
		private WeightMeasureType netWeightMeasureField;
    
		private WeightMeasureType netNetWeightMeasureField;
    
		private VolumeMeasureType grossVolumeMeasureField;
    
		private VolumeMeasureType netVolumeMeasureField;
    
		private DeliveryType deliveryField;
    
		private ContractType transportContractField;
    
		private List<ShipmentStageType> shipmentStageField = new List<ShipmentStageType>();
    
		private List<TransportEquipmentType> transportEquipmentField = new List<TransportEquipmentType>();
    
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
		public CodeType PriorityLevelCode {
			get {
				return this.priorityLevelCodeField;
			}
			set {
				this.priorityLevelCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType HandlingCode {
			get {
				return this.handlingCodeField;
			}
			set {
				this.handlingCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public InstructionsType HandlingInstructions {
			get {
				return this.handlingInstructionsField;
			}
			set {
				this.handlingInstructionsField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public InformationType Information {
			get {
				return this.informationField;
			}
			set {
				this.informationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public WeightMeasureType GrossWeightMeasure {
			get {
				return this.grossWeightMeasureField;
			}
			set {
				this.grossWeightMeasureField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public WeightMeasureType NetWeightMeasure {
			get {
				return this.netWeightMeasureField;
			}
			set {
				this.netWeightMeasureField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public WeightMeasureType NetNetWeightMeasure {
			get {
				return this.netNetWeightMeasureField;
			}
			set {
				this.netNetWeightMeasureField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public VolumeMeasureType GrossVolumeMeasure {
			get {
				return this.grossVolumeMeasureField;
			}
			set {
				this.grossVolumeMeasureField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public VolumeMeasureType NetVolumeMeasure {
			get {
				return this.netVolumeMeasureField;
			}
			set {
				this.netVolumeMeasureField = value;
			}
		}
    
		/// <remarks/>
		public DeliveryType Delivery {
			get {
				return this.deliveryField;
			}
			set {
				this.deliveryField = value;
			}
		}
    
		/// <remarks/>
		public ContractType TransportContract {
			get {
				return this.transportContractField;
			}
			set {
				this.transportContractField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("ShipmentStage")]
		public List<ShipmentStageType> ShipmentStage {
			get {
				return this.shipmentStageField;
			}
			set {
				this.shipmentStageField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("TransportEquipment")]
		public List<TransportEquipmentType> TransportEquipment {
			get {
				return this.transportEquipmentField;
			}
			set {
				this.transportEquipmentField = value;
			}
		}
	}
}