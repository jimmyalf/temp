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
	[System.Xml.Serialization.XmlRoot("cac:OrderReference", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class OrderReferenceType {
    
		private IdentifierType buyersIDField;
    
		private IdentifierType sellersIDField;
    
		private CopyIndicatorType copyIndicatorField;
    
		private DocumentStatusCodeType documentStatusCodeField;
    
		private IssueDateType issueDateField;
    
		private IdentifierType gUIDField;
    
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
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public CopyIndicatorType CopyIndicator {
			get {
				return copyIndicatorField;
			}
			set {
				copyIndicatorField = value;
			}
		}
    
		/// <remarks/>
		public DocumentStatusCodeType DocumentStatusCode {
			get {
				return documentStatusCodeField;
			}
			set {
				documentStatusCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public IssueDateType IssueDate {
			get {
				return issueDateField;
			}
			set {
				issueDateField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType GUID {
			get {
				return gUIDField;
			}
			set {
				gUIDField = value;
			}
		}
	}
}