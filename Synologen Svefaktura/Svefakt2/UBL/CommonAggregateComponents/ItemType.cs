using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("Item", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class ItemType {
    
		private DescriptionType descriptionField;
    
		private PackQuantityType packQuantityField;
    
		private PackSizeNumericType packSizeNumericField;
    
		private IndicatorType catalogueIndicatorField;
    
		private ItemIdentificationType buyersItemIdentificationField;
    
		private ItemIdentificationType sellersItemIdentificationField;
    
		private ItemIdentificationType manufacturersItemIdentificationField;
    
		private ItemIdentificationType standardItemIdentificationField;
    
		private ItemIdentificationType catalogueItemIdentificationField;

		private List<ItemIdentificationType> additionalItemIdentificationField;
    
		private DocumentReferenceType catalogueDocumentReferenceField;
    
		private LotIdentificationType lotIdentificationField;
    
		private CountryType originCountryField;
    
		private CommodityClassificationType commodityClassificationField;

		private List<SalesConditionsType> salesConditionsField;

		private List<HazardousItemType> hazardousItemField;

		private List<TaxCategoryType> taxCategoryField;

		private List<BasePriceType> basePriceField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DescriptionType Description {
			get {
				return descriptionField;
			}
			set {
				descriptionField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public PackQuantityType PackQuantity {
			get {
				return packQuantityField;
			}
			set {
				packQuantityField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public PackSizeNumericType PackSizeNumeric {
			get {
				return packSizeNumericField;
			}
			set {
				packSizeNumericField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public IndicatorType CatalogueIndicator {
			get {
				return catalogueIndicatorField;
			}
			set {
				catalogueIndicatorField = value;
			}
		}
    
		/// <remarks/>
		public ItemIdentificationType BuyersItemIdentification {
			get {
				return buyersItemIdentificationField;
			}
			set {
				buyersItemIdentificationField = value;
			}
		}
    
		/// <remarks/>
		public ItemIdentificationType SellersItemIdentification {
			get {
				return sellersItemIdentificationField;
			}
			set {
				sellersItemIdentificationField = value;
			}
		}
    
		/// <remarks/>
		public ItemIdentificationType ManufacturersItemIdentification {
			get {
				return manufacturersItemIdentificationField;
			}
			set {
				manufacturersItemIdentificationField = value;
			}
		}
    
		/// <remarks/>
		public ItemIdentificationType StandardItemIdentification {
			get {
				return standardItemIdentificationField;
			}
			set {
				standardItemIdentificationField = value;
			}
		}
    
		/// <remarks/>
		public ItemIdentificationType CatalogueItemIdentification {
			get {
				return catalogueItemIdentificationField;
			}
			set {
				catalogueItemIdentificationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("AdditionalItemIdentification")]
		public List<ItemIdentificationType> AdditionalItemIdentification {
			get {
				return additionalItemIdentificationField;
			}
			set {
				additionalItemIdentificationField = value;
			}
		}
    
		/// <remarks/>
		public DocumentReferenceType CatalogueDocumentReference {
			get {
				return catalogueDocumentReferenceField;
			}
			set {
				catalogueDocumentReferenceField = value;
			}
		}
    
		/// <remarks/>
		public LotIdentificationType LotIdentification {
			get {
				return lotIdentificationField;
			}
			set {
				lotIdentificationField = value;
			}
		}
    
		/// <remarks/>
		public CountryType OriginCountry {
			get {
				return originCountryField;
			}
			set {
				originCountryField = value;
			}
		}
    
		/// <remarks/>
		public CommodityClassificationType CommodityClassification {
			get {
				return commodityClassificationField;
			}
			set {
				commodityClassificationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("SalesConditions")]
		public List<SalesConditionsType> SalesConditions {
			get {
				return salesConditionsField;
			}
			set {
				salesConditionsField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("HazardousItem")]
		public List<HazardousItemType> HazardousItem {
			get {
				return hazardousItemField;
			}
			set {
				hazardousItemField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("TaxCategory")]
		public List<TaxCategoryType> TaxCategory {
			get {
				return taxCategoryField;
			}
			set {
				taxCategoryField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("BasePrice")]
		public List<BasePriceType> BasePrice {
			get {
				return basePriceField;
			}
			set {
				basePriceField = value;
			}
		}
	}
}