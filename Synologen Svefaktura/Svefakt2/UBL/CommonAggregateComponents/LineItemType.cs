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
	[System.Xml.Serialization.XmlRoot("cac:BuyerProposedLineItem", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class LineItemType {
    
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
				return buyersIDField;
			}
			set {
				buyersIDField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType SellersID {
			get {
				return sellersIDField;
			}
			set {
				sellersIDField = value;
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
		public TaxTotalAmountType TaxTotalAmount {
			get {
				return taxTotalAmountField;
			}
			set {
				taxTotalAmountField = value;
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
		public BackorderQuantityType MaximumBackorderQuantity {
			get {
				return maximumBackorderQuantityField;
			}
			set {
				maximumBackorderQuantityField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public BackorderQuantityType MinimumBackorderQuantity {
			get {
				return minimumBackorderQuantityField;
			}
			set {
				minimumBackorderQuantityField = value;
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
		public PartyType DestinationParty {
			get {
				return destinationPartyField;
			}
			set {
				destinationPartyField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("OrderedShipment")]
		public List<OrderedShipmentType> OrderedShipment {
			get {
				return orderedShipmentField;
			}
			set {
				orderedShipmentField = value;
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
		public BasePriceType BasePrice {
			get {
				return basePriceField;
			}
			set {
				basePriceField = value;
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
	}
}