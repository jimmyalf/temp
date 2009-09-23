using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("OrderLineReference", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class OrderLineReferenceType {
    
		private IdentifierType buyersLineIDField;
    
		private IdentifierType sellersLineIDField;
    
		private LineStatusCodeType lineStatusCodeField;
    
		private OrderReferenceType orderReferenceField;
    
		/// <remarks/>
		public IdentifierType BuyersLineID {
			get {
				return this.buyersLineIDField;
			}
			set {
				this.buyersLineIDField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType SellersLineID {
			get {
				return this.sellersLineIDField;
			}
			set {
				this.sellersLineIDField = value;
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
		public OrderReferenceType OrderReference {
			get {
				return this.orderReferenceField;
			}
			set {
				this.orderReferenceField = value;
			}
		}
	}
}