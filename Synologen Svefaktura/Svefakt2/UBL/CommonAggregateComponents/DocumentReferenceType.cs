using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("CatalogueDocumentReference", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class DocumentReferenceType {
    
		private IdentifierType idField;
    
		private CopyIndicatorType copyIndicatorField;
    
		private IssueDateType issueDateField;
    
		private IdentifierType gUIDField;
    
		/// <remarks/>
		public IdentifierType ID {
			get {
				return this.idField;
			}
			set {
				this.idField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public CopyIndicatorType CopyIndicator {
			get {
				return this.copyIndicatorField;
			}
			set {
				this.copyIndicatorField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public IssueDateType IssueDate {
			get {
				return this.issueDateField;
			}
			set {
				this.issueDateField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType GUID {
			get {
				return this.gUIDField;
			}
			set {
				this.gUIDField = value;
			}
		}
	}
}