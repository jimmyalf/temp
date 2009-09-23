using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("Item", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class ItemType {
    
		private DescriptionType descriptionField;
    
		private PackQuantityType packQuantityField;
    
		private PackSizeNumericType packSizeNumericField;
    
		private IndicatorType1 catalogueIndicatorField;
    
		private ItemIdentificationType buyersItemIdentificationField;
    
		private ItemIdentificationType sellersItemIdentificationField;
    
		private ItemIdentificationType manufacturersItemIdentificationField;
    
		private ItemIdentificationType standardItemIdentificationField;
    
		private ItemIdentificationType catalogueItemIdentificationField;
    
		private List<ItemIdentificationType> additionalItemIdentificationField = new List<ItemIdentificationType>();
    
		private DocumentReferenceType catalogueDocumentReferenceField;
    
		private LotIdentificationType lotIdentificationField;
    
		private CountryType originCountryField;
    
		private CommodityClassificationType commodityClassificationField;
    
		private List<SalesConditionsType> salesConditionsField = new List<SalesConditionsType>();
    
		private List<HazardousItemType> hazardousItemField = new List<HazardousItemType>();
    
		private List<TaxCategoryType> taxCategoryField = new List<TaxCategoryType>();

		private List<BasePriceType> basePriceField = new List<BasePriceType>();
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DescriptionType Description {
			get {
				return this.descriptionField;
			}
			set {
				this.descriptionField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public PackQuantityType PackQuantity {
			get {
				return this.packQuantityField;
			}
			set {
				this.packQuantityField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public PackSizeNumericType PackSizeNumeric {
			get {
				return this.packSizeNumericField;
			}
			set {
				this.packSizeNumericField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public IndicatorType1 CatalogueIndicator {
			get {
				return this.catalogueIndicatorField;
			}
			set {
				this.catalogueIndicatorField = value;
			}
		}
    
		/// <remarks/>
		public ItemIdentificationType BuyersItemIdentification {
			get {
				return this.buyersItemIdentificationField;
			}
			set {
				this.buyersItemIdentificationField = value;
			}
		}
    
		/// <remarks/>
		public ItemIdentificationType SellersItemIdentification {
			get {
				return this.sellersItemIdentificationField;
			}
			set {
				this.sellersItemIdentificationField = value;
			}
		}
    
		/// <remarks/>
		public ItemIdentificationType ManufacturersItemIdentification {
			get {
				return this.manufacturersItemIdentificationField;
			}
			set {
				this.manufacturersItemIdentificationField = value;
			}
		}
    
		/// <remarks/>
		public ItemIdentificationType StandardItemIdentification {
			get {
				return this.standardItemIdentificationField;
			}
			set {
				this.standardItemIdentificationField = value;
			}
		}
    
		/// <remarks/>
		public ItemIdentificationType CatalogueItemIdentification {
			get {
				return this.catalogueItemIdentificationField;
			}
			set {
				this.catalogueItemIdentificationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("AdditionalItemIdentification")]
		public List<ItemIdentificationType> AdditionalItemIdentification {
			get {
				return this.additionalItemIdentificationField;
			}
			set {
				this.additionalItemIdentificationField = value;
			}
		}
    
		/// <remarks/>
		public DocumentReferenceType CatalogueDocumentReference {
			get {
				return this.catalogueDocumentReferenceField;
			}
			set {
				this.catalogueDocumentReferenceField = value;
			}
		}
    
		/// <remarks/>
		public LotIdentificationType LotIdentification {
			get {
				return this.lotIdentificationField;
			}
			set {
				this.lotIdentificationField = value;
			}
		}
    
		/// <remarks/>
		public CountryType OriginCountry {
			get {
				return this.originCountryField;
			}
			set {
				this.originCountryField = value;
			}
		}
    
		/// <remarks/>
		public CommodityClassificationType CommodityClassification {
			get {
				return this.commodityClassificationField;
			}
			set {
				this.commodityClassificationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("SalesConditions")]
		public List<SalesConditionsType> SalesConditions {
			get {
				return this.salesConditionsField;
			}
			set {
				this.salesConditionsField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("HazardousItem")]
		public List<HazardousItemType> HazardousItem {
			get {
				return this.hazardousItemField;
			}
			set {
				this.hazardousItemField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("TaxCategory")]
		public List<TaxCategoryType> TaxCategory {
			get {
				return this.taxCategoryField;
			}
			set {
				this.taxCategoryField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("BasePrice")]
		public List<BasePriceType> BasePrice {
			get {
				return this.basePriceField;
			}
			set {
				this.basePriceField = value;
			}
		}
	}
}