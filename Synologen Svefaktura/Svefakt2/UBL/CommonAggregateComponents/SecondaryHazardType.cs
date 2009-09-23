using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("SecondaryHazard", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SecondaryHazardType {
    
		private IdentifierType idField;
    
		private PlacardNotationType placardNotationField;
    
		private PlacardEndorsementType placardEndorsementField;
    
		private CodeType emergencyProceduresCodeField;
    
		private ExtensionType extensionField;
    
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
		public PlacardNotationType PlacardNotation {
			get {
				return this.placardNotationField;
			}
			set {
				this.placardNotationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public PlacardEndorsementType PlacardEndorsement {
			get {
				return this.placardEndorsementField;
			}
			set {
				this.placardEndorsementField = value;
			}
		}
    
		/// <remarks/>
		public CodeType EmergencyProceduresCode {
			get {
				return this.emergencyProceduresCodeField;
			}
			set {
				this.emergencyProceduresCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ExtensionType Extension {
			get {
				return this.extensionField;
			}
			set {
				this.extensionField = value;
			}
		}
	}
}