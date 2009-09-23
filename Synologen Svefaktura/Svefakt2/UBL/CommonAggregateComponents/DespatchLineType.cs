using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("DespatchLine", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class DespatchLineType {
    
		private IdentifierType idField;
    
		private LineStatusCodeType lineStatusCodeField;
    
		private QuantityType2 deliveredQuantityField;
    
		private QuantityType2 backorderQuantityField;
    
		private ReasonType backorderReasonField;
    
		private NoteType noteField;
    
		private List<OrderLineReferenceType> orderLineReferenceField = new List<OrderLineReferenceType>();
    
		private List<DeliveryType> deliveryField;
    
		private DeliveryTermsType deliveryTermsField;
    
		private ItemType itemField;

		private TransportHandlingUnitType transportHandlingUnitField;
    
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
		public LineStatusCodeType LineStatusCode {
			get {
				return this.lineStatusCodeField;
			}
			set {
				this.lineStatusCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public QuantityType2 DeliveredQuantity {
			get {
				return this.deliveredQuantityField;
			}
			set {
				this.deliveredQuantityField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public QuantityType2 BackorderQuantity {
			get {
				return this.backorderQuantityField;
			}
			set {
				this.backorderQuantityField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ReasonType BackorderReason {
			get {
				return this.backorderReasonField;
			}
			set {
				this.backorderReasonField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public NoteType Note {
			get {
				return this.noteField;
			}
			set {
				this.noteField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("OrderLineReference")]
		public List<OrderLineReferenceType> OrderLineReference {
			get {
				return this.orderLineReferenceField;
			}
			set {
				this.orderLineReferenceField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("Delivery")]
		public List<DeliveryType> Delivery {
			get {
				return this.deliveryField;
			}
			set {
				this.deliveryField = value;
			}
		}
    
		/// <remarks/>
		public DeliveryTermsType DeliveryTerms {
			get {
				return this.deliveryTermsField;
			}
			set {
				this.deliveryTermsField = value;
			}
		}
    
		/// <remarks/>
		public ItemType Item {
			get {
				return this.itemField;
			}
			set {
				this.itemField = value;
			}
		}
    
		/// <remarks/>
		public TransportHandlingUnitType TransportHandlingUnit {
			get {
				return this.transportHandlingUnitField;
			}
			set {
				this.transportHandlingUnitField = value;
			}
		}
	}
}