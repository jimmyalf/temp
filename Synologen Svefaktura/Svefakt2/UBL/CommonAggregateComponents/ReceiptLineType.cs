using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using DateType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.DateType;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("ReceiptLine", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class ReceiptLineType {
    
		private IdentifierType idField;
    
		private LineStatusCodeType lineStatusCodeField;
    
		private QuantityType2 receivedQuantityField;
    
		private QuantityType2 shortQuantityField;
    
		private CodeType shortageActionCodeField;
    
		private QuantityType2 rejectedQuantityField;
    
		private CodeType rejectReasonCodeField;
    
		private CodeType rejectActionCodeField;
    
		private DateType receivedDateField;
    
		private CodeType timingComplaintCodeField;
    
		private NoteType noteField;

		private List<OrderLineReferenceType> orderLineReferenceField;

		private List<LineReferenceType> despatchLineReferenceField;

		private List<DeliveryType> deliveryField;

		private List<TransportHandlingUnitType> transportHandlingUnitField;

		private List<ItemIdentificationType> orderedItemIdentificationField;
    
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
		public LineStatusCodeType LineStatusCode {
			get {
				return lineStatusCodeField;
			}
			set {
				lineStatusCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public QuantityType2 ReceivedQuantity {
			get {
				return receivedQuantityField;
			}
			set {
				receivedQuantityField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public QuantityType2 ShortQuantity {
			get {
				return shortQuantityField;
			}
			set {
				shortQuantityField = value;
			}
		}
    
		/// <remarks/>
		public CodeType ShortageActionCode {
			get {
				return shortageActionCodeField;
			}
			set {
				shortageActionCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public QuantityType2 RejectedQuantity {
			get {
				return rejectedQuantityField;
			}
			set {
				rejectedQuantityField = value;
			}
		}
    
		/// <remarks/>
		public CodeType RejectReasonCode {
			get {
				return rejectReasonCodeField;
			}
			set {
				rejectReasonCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType RejectActionCode {
			get {
				return rejectActionCodeField;
			}
			set {
				rejectActionCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DateType ReceivedDate {
			get {
				return receivedDateField;
			}
			set {
				receivedDateField = value;
			}
		}
    
		/// <remarks/>
		public CodeType TimingComplaintCode {
			get {
				return timingComplaintCodeField;
			}
			set {
				timingComplaintCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public NoteType Note {
			get {
				return noteField;
			}
			set {
				noteField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("OrderLineReference")]
		public List<OrderLineReferenceType> OrderLineReference {
			get {
				return orderLineReferenceField;
			}
			set {
				orderLineReferenceField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("DespatchLineReference")]
		public List<LineReferenceType> DespatchLineReference {
			get {
				return despatchLineReferenceField;
			}
			set {
				despatchLineReferenceField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("Delivery")]
		public List<DeliveryType> Delivery {
			get {
				return deliveryField;
			}
			set {
				deliveryField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("TransportHandlingUnit")]
		public List<TransportHandlingUnitType> TransportHandlingUnit {
			get {
				return transportHandlingUnitField;
			}
			set {
				transportHandlingUnitField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("OrderedItemIdentification")]
		public List<ItemIdentificationType> OrderedItemIdentification {
			get {
				return orderedItemIdentificationField;
			}
			set {
				orderedItemIdentificationField = value;
			}
		}
	}
}