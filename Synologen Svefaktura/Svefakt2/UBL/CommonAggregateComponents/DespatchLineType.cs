using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("DespatchLine", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class DespatchLineType {
    
		private IdentifierType idField;
    
		private LineStatusCodeType lineStatusCodeField;
    
		private QuantityType2 deliveredQuantityField;
    
		private QuantityType2 backorderQuantityField;
    
		private ReasonType backorderReasonField;
    
		private NoteType noteField;

		private List<OrderLineReferenceType> orderLineReferenceField;
    
		private List<DeliveryType> deliveryField;
    
		private DeliveryTermsType deliveryTermsField;
    
		private ItemType itemField;

		private TransportHandlingUnitType transportHandlingUnitField;
    
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
		public QuantityType2 DeliveredQuantity {
			get {
				return deliveredQuantityField;
			}
			set {
				deliveredQuantityField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public QuantityType2 BackorderQuantity {
			get {
				return backorderQuantityField;
			}
			set {
				backorderQuantityField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ReasonType BackorderReason {
			get {
				return backorderReasonField;
			}
			set {
				backorderReasonField = value;
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
		public DeliveryTermsType DeliveryTerms {
			get {
				return deliveryTermsField;
			}
			set {
				deliveryTermsField = value;
			}
		}
    
		/// <remarks/>
		public ItemType Item {
			get {
				return itemField;
			}
			set {
				itemField = value;
			}
		}
    
		/// <remarks/>
		public TransportHandlingUnitType TransportHandlingUnit {
			get {
				return transportHandlingUnitField;
			}
			set {
				transportHandlingUnitField = value;
			}
		}
	}
}