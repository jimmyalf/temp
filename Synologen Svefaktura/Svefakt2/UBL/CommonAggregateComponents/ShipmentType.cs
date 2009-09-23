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
	[System.Xml.Serialization.XmlRoot("cac:Shipment", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class ShipmentType {
    
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
				return idField;
			}
			set {
				idField = value;
			}
		}
    
		/// <remarks/>
		public CodeType PriorityLevelCode {
			get {
				return priorityLevelCodeField;
			}
			set {
				priorityLevelCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType HandlingCode {
			get {
				return handlingCodeField;
			}
			set {
				handlingCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public InstructionsType HandlingInstructions {
			get {
				return handlingInstructionsField;
			}
			set {
				handlingInstructionsField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public InformationType Information {
			get {
				return informationField;
			}
			set {
				informationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public WeightMeasureType GrossWeightMeasure {
			get {
				return grossWeightMeasureField;
			}
			set {
				grossWeightMeasureField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public WeightMeasureType NetWeightMeasure {
			get {
				return netWeightMeasureField;
			}
			set {
				netWeightMeasureField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public WeightMeasureType NetNetWeightMeasure {
			get {
				return netNetWeightMeasureField;
			}
			set {
				netNetWeightMeasureField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public VolumeMeasureType GrossVolumeMeasure {
			get {
				return grossVolumeMeasureField;
			}
			set {
				grossVolumeMeasureField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public VolumeMeasureType NetVolumeMeasure {
			get {
				return netVolumeMeasureField;
			}
			set {
				netVolumeMeasureField = value;
			}
		}
    
		/// <remarks/>
		public DeliveryType Delivery {
			get {
				return deliveryField;
			}
			set {
				deliveryField = value;
			}
		}
    
		/// <remarks/>
		public ContractType TransportContract {
			get {
				return transportContractField;
			}
			set {
				transportContractField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("ShipmentStage")]
		public List<ShipmentStageType> ShipmentStage {
			get {
				return shipmentStageField;
			}
			set {
				shipmentStageField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("TransportEquipment")]
		public List<TransportEquipmentType> TransportEquipment {
			get {
				return transportEquipmentField;
			}
			set {
				transportEquipmentField = value;
			}
		}
	}
}