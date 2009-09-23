using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using DateTimeType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.DateTimeType;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("Delivery", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class DeliveryType {
    
		private IdentifierType idField;
    
		private QuantityType2 quantityField;
    
		private QuantityType2 minimumQuantityField;
    
		private QuantityType2 maximumQuantityField;
    
		private DeliveryDateTimeType requestedDeliveryDateTimeField;
    
		private DateTimeType promisedDateTimeField;
    
		private DeliveryDateTimeType actualDeliveryDateTimeField;
    
		private AddressType deliveryAddressField;
    
		private AddressType despatchAddressField;
    
		private List<OrderLineReferenceType> orderLineReferenceField = new List<OrderLineReferenceType>();
    
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
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public QuantityType2 Quantity {
			get {
				return quantityField;
			}
			set {
				quantityField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public QuantityType2 MinimumQuantity {
			get {
				return minimumQuantityField;
			}
			set {
				minimumQuantityField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public QuantityType2 MaximumQuantity {
			get {
				return maximumQuantityField;
			}
			set {
				maximumQuantityField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DeliveryDateTimeType RequestedDeliveryDateTime {
			get {
				return requestedDeliveryDateTimeField;
			}
			set {
				requestedDeliveryDateTimeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DateTimeType PromisedDateTime {
			get {
				return promisedDateTimeField;
			}
			set {
				promisedDateTimeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DeliveryDateTimeType ActualDeliveryDateTime {
			get {
				return actualDeliveryDateTimeField;
			}
			set {
				actualDeliveryDateTimeField = value;
			}
		}
    
		/// <remarks/>
		public AddressType DeliveryAddress {
			get {
				return deliveryAddressField;
			}
			set {
				deliveryAddressField = value;
			}
		}
    
		/// <remarks/>
		public AddressType DespatchAddress {
			get {
				return despatchAddressField;
			}
			set {
				despatchAddressField = value;
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
	}
}