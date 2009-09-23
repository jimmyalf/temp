namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("InvoiceLine", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTIInvoiceLineType {
    
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
				return this.idField;
			}
			set {
				this.idField = value;
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
		public SFTIOrderLineReferenceType OrderLineReference {
			get {
				return this.orderLineReferenceField;
			}
			set {
				this.orderLineReferenceField = value;
			}
		}
    
		/// <remarks/>
		public SFTILineReferenceType DespatchLineReference {
			get {
				return this.despatchLineReferenceField;
			}
			set {
				this.despatchLineReferenceField = value;
			}
		}
    
		/// <remarks/>
		public SFTIInvoiceLineDeliveryType Delivery {
			get {
				return this.deliveryField;
			}
			set {
				this.deliveryField = value;
			}
		}
    
		/// <remarks/>
		public SFTIInvoiceLineAllowanceCharge AllowanceCharge {
			get {
				return this.allowanceChargeField;
			}
			set {
				this.allowanceChargeField = value;
			}
		}
    
		/// <remarks/>
		public SFTIItemType Item {
			get {
				return this.itemField;
			}
			set {
				this.itemField = value;
			}
		}
	}
}