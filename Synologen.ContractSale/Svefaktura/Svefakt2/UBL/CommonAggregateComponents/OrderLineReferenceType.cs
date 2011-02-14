using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("OrderLineReference", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class OrderLineReferenceType {
    
		private IdentifierType buyersLineIDField;
    
		private IdentifierType sellersLineIDField;
    
		private LineStatusCodeType lineStatusCodeField;
    
		private OrderReferenceType orderReferenceField;
    
		/// <remarks/>
		public IdentifierType BuyersLineID {
			get {
				return buyersLineIDField;
			}
			set {
				buyersLineIDField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType SellersLineID {
			get {
				return sellersLineIDField;
			}
			set {
				sellersLineIDField = value;
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
		public OrderReferenceType OrderReference {
			get {
				return orderReferenceField;
			}
			set {
				orderReferenceField = value;
			}
		}
	}
}