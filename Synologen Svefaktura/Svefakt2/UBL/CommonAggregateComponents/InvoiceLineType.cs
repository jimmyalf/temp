using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("InvoiceLine", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class InvoiceLineType {
    
		private IdentifierType idField;
    
		private LineStatusCodeType lineStatusCodeField;
    
		private QuantityType2 invoicedQuantityField;
    
		private ExtensionAmountType lineExtensionAmountField;
    
		private NoteType noteField;
    
		private List<OrderLineReferenceType> orderLineReferenceField = new List<OrderLineReferenceType>();
    
		private List<LineReferenceType> despatchLineReferenceField = new List<LineReferenceType>();
    
		private List<LineReferenceType> receiptLineReferenceField = new List<LineReferenceType>();
    
		private List<DeliveryType> deliveryField = new List<DeliveryType>();
    
		private List<PaymentTermsType> paymentTermsField = new List<PaymentTermsType>();
    
		private List<AllowanceChargeType> allowanceChargeField = new List<AllowanceChargeType>();
    
		private List<TaxTotalType> taxTotalField = new List<TaxTotalType>();
    
		private ItemType itemField;
    
		private BasePriceType basePriceField;
    
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
		public QuantityType2 InvoicedQuantity {
			get {
				return this.invoicedQuantityField;
			}
			set {
				this.invoicedQuantityField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ExtensionAmountType LineExtensionAmount {
			get {
				return this.lineExtensionAmountField;
			}
			set {
				this.lineExtensionAmountField = value;
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
		[System.Xml.Serialization.XmlElementAttribute("ReceiptLineReference")]
		public List<LineReferenceType> ReceiptLineReference {
			get {
				return this.receiptLineReferenceField;
			}
			set {
				this.receiptLineReferenceField = value;
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
		[System.Xml.Serialization.XmlElementAttribute("PaymentTerms")]
		public List<PaymentTermsType> PaymentTerms {
			get {
				return this.paymentTermsField;
			}
			set {
				this.paymentTermsField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("AllowanceCharge")]
		public List<AllowanceChargeType> AllowanceCharge {
			get {
				return this.allowanceChargeField;
			}
			set {
				this.allowanceChargeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("TaxTotal")]
		public List<TaxTotalType> TaxTotal {
			get {
				return this.taxTotalField;
			}
			set {
				this.taxTotalField = value;
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
		public BasePriceType BasePrice {
			get {
				return this.basePriceField;
			}
			set {
				this.basePriceField = value;
			}
		}
	}
}