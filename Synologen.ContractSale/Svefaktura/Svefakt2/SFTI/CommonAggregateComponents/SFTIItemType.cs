using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("Item", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public class SFTIItemType {
    
		private DescriptionType descriptionField;
    
		private SFTIItemIdentificationType buyersItemIdentificationField;
    
		private SFTIItemIdentificationType sellersItemIdentificationField;
    
		private SFTIItemIdentificationType standardItemIdentificationField;

		private List<SFTITaxCategoryType> taxCategoryField;
    
		private SFTIBasePriceType basePriceField;
    
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
		public SFTIItemIdentificationType BuyersItemIdentification {
			get {
				return buyersItemIdentificationField;
			}
			set {
				buyersItemIdentificationField = value;
			}
		}
    
		/// <remarks/>
		public SFTIItemIdentificationType SellersItemIdentification {
			get {
				return sellersItemIdentificationField;
			}
			set {
				sellersItemIdentificationField = value;
			}
		}
    
		/// <remarks/>
		public SFTIItemIdentificationType StandardItemIdentification {
			get {
				return standardItemIdentificationField;
			}
			set {
				standardItemIdentificationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("TaxCategory")]
		public List<SFTITaxCategoryType> TaxCategory {
			get {
				return taxCategoryField;
			}
			set {
				taxCategoryField = value;
			}
		}
    
		/// <remarks/>
		public SFTIBasePriceType BasePrice {
			get {
				return basePriceField;
			}
			set {
				basePriceField = value;
			}
		}
	}
}