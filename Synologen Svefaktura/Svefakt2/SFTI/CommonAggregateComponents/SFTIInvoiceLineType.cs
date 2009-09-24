using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("InvoiceLine", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public class SFTIInvoiceLineType {
    
		private SFTISimpleIdentifierType idField;
    
		private QuantityType2 invoicedQuantityField;
    
		private ExtensionAmountType lineExtensionAmountField;
    
		private NoteType noteField;
    
		private SFTIOrderLineReferenceType orderLineReferenceField;
    
		private SFTILineReferenceType despatchLineReferenceField;
    
		private SFTIInvoiceLineDeliveryType deliveryField;
    
		private SFTIInvoiceLineAllowanceCharge allowanceChargeField;
    
		private SFTIItemType itemField;
    
		/// <remarks/>
		public SFTISimpleIdentifierType ID {
			get {
				return idField;
			}
			set {
				idField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public QuantityType2 InvoicedQuantity {
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
		public SFTIOrderLineReferenceType OrderLineReference {
			get {
				return orderLineReferenceField;
			}
			set {
				orderLineReferenceField = value;
			}
		}
    
		/// <remarks/>
		public SFTILineReferenceType DespatchLineReference {
			get {
				return despatchLineReferenceField;
			}
			set {
				despatchLineReferenceField = value;
			}
		}
    
		/// <remarks/>
		public SFTIInvoiceLineDeliveryType Delivery {
			get {
				return deliveryField;
			}
			set {
				deliveryField = value;
			}
		}
    
		/// <remarks/>
		public SFTIInvoiceLineAllowanceCharge AllowanceCharge {
			get {
				return allowanceChargeField;
			}
			set {
				allowanceChargeField = value;
			}
		}
    
		/// <remarks/>
		public SFTIItemType Item {
			get {
				return itemField;
			}
			set {
				itemField = value;
			}
		}
	}
}