namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("BasePrice", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class BasePriceType {
    
		private PriceAmountType priceAmountField;
    
		private QuantityType2 baseQuantityField;
    
		private QuantityType2 maximumQuantityField;
    
		private QuantityType2 minimumQuantityField;
    
		private AmountType2 maximumAmountField;
    
		private AmountType2 minimumAmountField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public PriceAmountType PriceAmount {
			get {
				return this.priceAmountField;
			}
			set {
				this.priceAmountField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public QuantityType2 BaseQuantity {
			get {
				return this.baseQuantityField;
			}
			set {
				this.baseQuantityField = value;
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
		public AmountType2 MaximumAmount {
			get {
				return this.maximumAmountField;
			}
			set {
				this.maximumAmountField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public AmountType2 MinimumAmount {
			get {
				return this.minimumAmountField;
			}
			set {
				this.minimumAmountField = value;
			}
		}
	}
}