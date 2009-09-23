using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("OrderLineReference", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTIOrderLineReferenceType {
    
		private IdentifierType buyersLineIDField;
    
		private SFTIOrderReferenceType orderReferenceField;
    
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
		public SFTIOrderReferenceType OrderReference {
			get {
				return this.orderReferenceField;
			}
			set {
				this.orderReferenceField = value;
			}
		}
	}
}