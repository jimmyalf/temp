using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("BuyerProposedLineItem", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class LineItemType {
    
		private IdentifierType buyersIDField;
    
		private IdentifierType sellersIDField;
    
		private LineStatusCodeType lineStatusCodeField;
    
		private QuantityType2 quantityField;
    
		private ExtensionAmountType lineExtensionAmountField;
    
		private TaxTotalAmountType taxTotalAmountField;
    
		private QuantityType2 minimumQuantityField;
    
		private QuantityType2 maximumQuantityField;
    
		private BackorderQuantityType maximumBackorderQuantityField;
    
		private BackorderQuantityType minimumBackorderQuantityField;
    
		private NoteType noteField;
    
		private List<DeliveryType> deliveryField = new List<DeliveryType>();
    
		private DeliveryTermsType deliveryTermsField;
    
		private PartyType destinationPartyField;
    
		private List<OrderedShipmentType> orderedShipmentField = new List<OrderedShipmentType>();
    
		private List<AllowanceChargeType> allowanceChargeField = new List<AllowanceChargeType>();
    
		private BasePriceType basePriceField;
    
		private ItemType itemField;
    
		/// <remarks/>
		public IdentifierType BuyersID {
			get {
				return this.buyersIDField;
			}
			set {
				this.buyersIDField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType SellersID {
			get {
				return this.sellersIDField;
			}
			set {
				this.sellersIDField = value;
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
		public TaxTotalAmountType TaxTotalAmount {
			get {
				return this.taxTotalAmountField;
			}
			set {
				this.taxTotalAmountField = value;
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
		public BackorderQuantityType MaximumBackorderQuantity {
			get {
				return this.maximumBackorderQuantityField;
			}
			set {
				this.maximumBackorderQuantityField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public BackorderQuantityType MinimumBackorderQuantity {
			get {
				return this.minimumBackorderQuantityField;
			}
			set {
				this.minimumBackorderQuantityField = value;
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
		public PartyType DestinationParty {
			get {
				return this.destinationPartyField;
			}
			set {
				this.destinationPartyField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("OrderedShipment")]
		public List<OrderedShipmentType> OrderedShipment {
			get {
				return this.orderedShipmentField;
			}
			set {
				this.orderedShipmentField = value;
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
		public BasePriceType BasePrice {
			get {
				return this.basePriceField;
			}
			set {
				this.basePriceField = value;
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
	}
}