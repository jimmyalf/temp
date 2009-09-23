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
	[System.Xml.Serialization.XmlRoot("TransportEquipment", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class TransportEquipmentType {
    
		private IdentifierType idField;
    
		private CodeType providerTypeCodeField;
    
		private CodeType ownerTypeCodeField;
    
		private CodeType sizeTypeCodeField;
    
		private CodeType dispositionCodeField;
    
		private CodeType fullnessIndicationCodeField;
    
		private RefrigerationOnIndicatorType refrigerationOnIndicatorField;
    
		private InformationType informationField;
    
		private List<DimensionType> dimensionField = new List<DimensionType>();
    
		private List<TransportEquipmentSealType> transportEquipmentSealField = new List<TransportEquipmentSealType>();
    
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
		public CodeType ProviderTypeCode {
			get {
				return providerTypeCodeField;
			}
			set {
				providerTypeCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType OwnerTypeCode {
			get {
				return ownerTypeCodeField;
			}
			set {
				ownerTypeCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType SizeTypeCode {
			get {
				return sizeTypeCodeField;
			}
			set {
				sizeTypeCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType DispositionCode {
			get {
				return dispositionCodeField;
			}
			set {
				dispositionCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType FullnessIndicationCode {
			get {
				return fullnessIndicationCodeField;
			}
			set {
				fullnessIndicationCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public RefrigerationOnIndicatorType RefrigerationOnIndicator {
			get {
				return refrigerationOnIndicatorField;
			}
			set {
				refrigerationOnIndicatorField = value;
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
		[System.Xml.Serialization.XmlElement("Dimension")]
		public List<DimensionType> Dimension {
			get {
				return dimensionField;
			}
			set {
				dimensionField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("TransportEquipmentSeal")]
		public List<TransportEquipmentSealType> TransportEquipmentSeal {
			get {
				return transportEquipmentSealField;
			}
			set {
				transportEquipmentSealField = value;
			}
		}
	}
}