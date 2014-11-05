using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using QuantityType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.QuantityType;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("InvoiceLine", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class InvoiceLineType {
    
		private IdentifierType idField;
    
		private LineStatusCodeType lineStatusCodeField;
    
		private QuantityType invoicedQuantityField;
    
		private ExtensionAmountType lineExtensionAmountField;
    
		private NoteType noteField;

		private List<OrderLineReferenceType> orderLineReferenceField;

		private List<LineReferenceType> despatchLineReferenceField;

		private List<LineReferenceType> receiptLineReferenceField;

		private List<DeliveryType> deliveryField;

		private List<PaymentTermsType> paymentTermsField;

		private List<AllowanceChargeType> allowanceChargeField;

		private List<TaxTotalType> taxTotalField;
    
		private ItemType itemField;
    
		private BasePriceType basePriceField;
    
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
		public QuantityType InvoicedQuantity {
			get {
				return invoicedQuantityField;
			}
			set {
				invoicedQuantityField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ExtensionAmountType LineExtensionAmount {
			get {
				return lineExtensionAmountField;
			}
			set {
				lineExtensionAmountField = value;
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
		[System.Xml.Serialization.XmlElement("ReceiptLineReference")]
		public List<LineReferenceType> ReceiptLineReference {
			get {
				return receiptLineReferenceField;
			}
			set {
				receiptLineReferenceField = value;
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
		[System.Xml.Serialization.XmlElement("PaymentTerms")]
		public List<PaymentTermsType> PaymentTerms {
			get {
				return paymentTermsField;
			}
			set {
				paymentTermsField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("AllowanceCharge")]
		public List<AllowanceChargeType> AllowanceCharge {
			get {
				return allowanceChargeField;
			}
			set {
				allowanceChargeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("TaxTotal")]
		public List<TaxTotalType> TaxTotal {
			get {
				return taxTotalField;
			}
			set {
				taxTotalField = value;
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
		public BasePriceType BasePrice {
			get {
				return basePriceField;
			}
			set {
				basePriceField = value;
			}
		}
	}
}