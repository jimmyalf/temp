using Spinit.Wpc.Synologen.Svefaktura.CustomEnumerations;
using Spinit.Wpc.Synologen.Svefaktura.CustomTypes;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("DespatchLineReference", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public class SFTILineReferenceType {
    
		private IdentifierType lineIDField;
    
		private SFTIDocumentReferenceType documentReferenceField;
    
		/// <remarks/>
		[PropertyValidationRule("SFTILineReferenceType.LineID is missing.", ValidationType.RequiredNotNull)]
		public IdentifierType LineID {
			get {
				return lineIDField;
			}
			set {
				lineIDField = value;
			}
		}
    
		/// <remarks/>
		public SFTIDocumentReferenceType DocumentReference {
			get {
				return documentReferenceField;
			}
			set {
				documentReferenceField = value;
			}
		}
	}
}