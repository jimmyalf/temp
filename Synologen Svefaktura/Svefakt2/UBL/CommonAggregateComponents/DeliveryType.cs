using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("Delivery", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class DeliveryType {
    
		private IdentifierType idField;
    
		private QuantityType2 quantityField;
    
		private QuantityType2 minimumQuantityField;
    
		private QuantityType2 maximumQuantityField;
    
		private DeliveryDateTimeType requestedDeliveryDateTimeField;
    
		private DateTimeType1 promisedDateTimeField;
    
		private DeliveryDateTimeType actualDeliveryDateTimeField;
    
		private AddressType deliveryAddressField;
    
		private AddressType despatchAddressField;
    
		private List<OrderLineReferenceType> orderLineReferenceField = new List<OrderLineReferenceType>();
    
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
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public QuantityType2 Quantity {
			get {
				return this.quantityField;
			}
			set {
				this.quantityField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public QuantityType2 MinimumQuantity {
			get {
				return this.minimumQuantityField;
			}
			set {
				this.minimumQuantityField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public QuantityType2 MaximumQuantity {
			get {
				return this.maximumQuantityField;
			}
			set {
				this.maximumQuantityField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DeliveryDateTimeType RequestedDeliveryDateTime {
			get {
				return this.requestedDeliveryDateTimeField;
			}
			set {
				this.requestedDeliveryDateTimeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DateTimeType1 PromisedDateTime {
			get {
				return this.promisedDateTimeField;
			}
			set {
				this.promisedDateTimeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DeliveryDateTimeType ActualDeliveryDateTime {
			get {
				return this.actualDeliveryDateTimeField;
			}
			set {
				this.actualDeliveryDateTimeField = value;
			}
		}
    
		/// <remarks/>
		public AddressType DeliveryAddress {
			get {
				return this.deliveryAddressField;
			}
			set {
				this.deliveryAddressField = value;
			}
		}
    
		/// <remarks/>
		public AddressType DespatchAddress {
			get {
				return this.despatchAddressField;
			}
			set {
				this.despatchAddressField = value;
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
	}
}