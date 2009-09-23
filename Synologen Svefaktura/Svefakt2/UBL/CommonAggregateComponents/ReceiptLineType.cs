using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("ReceiptLine", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class ReceiptLineType {
    
		private IdentifierType idField;
    
		private LineStatusCodeType lineStatusCodeField;
    
		private QuantityType2 receivedQuantityField;
    
		private QuantityType2 shortQuantityField;
    
		private CodeType shortageActionCodeField;
    
		private QuantityType2 rejectedQuantityField;
    
		private CodeType rejectReasonCodeField;
    
		private CodeType rejectActionCodeField;
    
		private DateType1 receivedDateField;
    
		private CodeType timingComplaintCodeField;
    
		private NoteType noteField;
    
		private List<OrderLineReferenceType> orderLineReferenceField = new List<OrderLineReferenceType>();
    
		private List<LineReferenceType> despatchLineReferenceField = new List<LineReferenceType>();
    
		private List<DeliveryType> deliveryField = new List<DeliveryType>();
    
		private List<TransportHandlingUnitType> transportHandlingUnitField = new List<TransportHandlingUnitType>();

		private List<ItemIdentificationType> orderedItemIdentificationField = new List<ItemIdentificationType>();
    
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
		public QuantityType2 ReceivedQuantity {
			get {
				return this.receivedQuantityField;
			}
			set {
				this.receivedQuantityField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public QuantityType2 ShortQuantity {
			get {
				return this.shortQuantityField;
			}
			set {
				this.shortQuantityField = value;
			}
		}
    
		/// <remarks/>
		public CodeType ShortageActionCode {
			get {
				return this.shortageActionCodeField;
			}
			set {
				this.shortageActionCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public QuantityType2 RejectedQuantity {
			get {
				return this.rejectedQuantityField;
			}
			set {
				this.rejectedQuantityField = value;
			}
		}
    
		/// <remarks/>
		public CodeType RejectReasonCode {
			get {
				return this.rejectReasonCodeField;
			}
			set {
				this.rejectReasonCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType RejectActionCode {
			get {
				return this.rejectActionCodeField;
			}
			set {
				this.rejectActionCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DateType1 ReceivedDate {
			get {
				return this.receivedDateField;
			}
			set {
				this.receivedDateField = value;
			}
		}
    
		/// <remarks/>
		public CodeType TimingComplaintCode {
			get {
				return this.timingComplaintCodeField;
			}
			set {
				this.timingComplaintCodeField = value;
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
		[System.Xml.Serialization.XmlElementAttribute("DespatchLineReference")]
		public List<LineReferenceType> DespatchLineReference {
			get {
				return this.despatchLineReferenceField;
			}
			set {
				this.despatchLineReferenceField = value;
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
		[System.Xml.Serialization.XmlElementAttribute("TransportHandlingUnit")]
		public List<TransportHandlingUnitType> TransportHandlingUnit {
			get {
				return this.transportHandlingUnitField;
			}
			set {
				this.transportHandlingUnitField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("OrderedItemIdentification")]
		public List<ItemIdentificationType> OrderedItemIdentification {
			get {
				return this.orderedItemIdentificationField;
			}
			set {
				this.orderedItemIdentificationField = value;
			}
		}
	}
}