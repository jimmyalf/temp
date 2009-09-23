using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("DespatchLineReference", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTILineReferenceType {
    
		private IdentifierType lineIDField;
    
		private SFTIDocumentReferenceType documentReferenceField;
    
		/// <remarks/>
		public IdentifierType LineID {
			get {
				return this.lineIDField;
			}
			set {
				this.lineIDField = value;
			}
		}
    
		/// <remarks/>
		public SFTIDocumentReferenceType DocumentReference {
			get {
				return this.documentReferenceField;
			}
			set {
				this.documentReferenceField = value;
			}
		}
	}
}