using System.Collections.Generic;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("Item", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTIItemType {
    
		private DescriptionType descriptionField;
    
		private SFTIItemIdentificationType buyersItemIdentificationField;
    
		private SFTIItemIdentificationType sellersItemIdentificationField;
    
		private SFTIItemIdentificationType standardItemIdentificationField;
    
		private List<SFTITaxCategoryType> taxCategoryField = new List<SFTITaxCategoryType>();
    
		private SFTIBasePriceType basePriceField;
    
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
		public SFTIItemIdentificationType BuyersItemIdentification {
			get {
				return this.buyersItemIdentificationField;
			}
			set {
				this.buyersItemIdentificationField = value;
			}
		}
    
		/// <remarks/>
		public SFTIItemIdentificationType SellersItemIdentification {
			get {
				return this.sellersItemIdentificationField;
			}
			set {
				this.sellersItemIdentificationField = value;
			}
		}
    
		/// <remarks/>
		public SFTIItemIdentificationType StandardItemIdentification {
			get {
				return this.standardItemIdentificationField;
			}
			set {
				this.standardItemIdentificationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("TaxCategory")]
		public List<SFTITaxCategoryType> TaxCategory {
			get {
				return this.taxCategoryField;
			}
			set {
				this.taxCategoryField = value;
			}
		}
    
		/// <remarks/>
		public SFTIBasePriceType BasePrice {
			get {
				return this.basePriceField;
			}
			set {
				this.basePriceField = value;
			}
		}
	}
}