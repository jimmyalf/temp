using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("TransportEquipment", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class TransportEquipmentType {
    
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
				return this.idField;
			}
			set {
				this.idField = value;
			}
		}
    
		/// <remarks/>
		public CodeType ProviderTypeCode {
			get {
				return this.providerTypeCodeField;
			}
			set {
				this.providerTypeCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType OwnerTypeCode {
			get {
				return this.ownerTypeCodeField;
			}
			set {
				this.ownerTypeCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType SizeTypeCode {
			get {
				return this.sizeTypeCodeField;
			}
			set {
				this.sizeTypeCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType DispositionCode {
			get {
				return this.dispositionCodeField;
			}
			set {
				this.dispositionCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType FullnessIndicationCode {
			get {
				return this.fullnessIndicationCodeField;
			}
			set {
				this.fullnessIndicationCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public RefrigerationOnIndicatorType RefrigerationOnIndicator {
			get {
				return this.refrigerationOnIndicatorField;
			}
			set {
				this.refrigerationOnIndicatorField = value;
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
		[System.Xml.Serialization.XmlElementAttribute("Dimension")]
		public List<DimensionType> Dimension {
			get {
				return this.dimensionField;
			}
			set {
				this.dimensionField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("TransportEquipmentSeal")]
		public List<TransportEquipmentSealType> TransportEquipmentSeal {
			get {
				return this.transportEquipmentSealField;
			}
			set {
				this.transportEquipmentSealField = value;
			}
		}
	}
}