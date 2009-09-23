using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("cac:SecondaryHazard", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class SecondaryHazardType {
    
		private IdentifierType idField;
    
		private PlacardNotationType placardNotationField;
    
		private PlacardEndorsementType placardEndorsementField;
    
		private CodeType emergencyProceduresCodeField;
    
		private ExtensionType extensionField;
    
		/// <remarks/>
		public IdentifierType ID {
			get {
				return idField;
			}
			set {
				idField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public PlacardNotationType PlacardNotation {
			get {
				return placardNotationField;
			}
			set {
				placardNotationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public PlacardEndorsementType PlacardEndorsement {
			get {
				return placardEndorsementField;
			}
			set {
				placardEndorsementField = value;
			}
		}
    
		/// <remarks/>
		public CodeType EmergencyProceduresCode {
			get {
				return emergencyProceduresCodeField;
			}
			set {
				emergencyProceduresCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ExtensionType Extension {
			get {
				return extensionField;
			}
			set {
				extensionField = value;
			}
		}
	}
}