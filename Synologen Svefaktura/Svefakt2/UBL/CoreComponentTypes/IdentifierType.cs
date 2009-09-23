namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CoreComponentTypes {
	/// <remarks/>
	[System.Xml.Serialization.XmlInclude(typeof(UnspecializedDatatypes.IdentifierType))]
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable()]
	[System.Diagnostics.DebuggerStepThrough()]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CoreComponentTypes:1:0")]
	public partial class IdentifierType {
    
		private string identificationSchemeIDField;
    
		private string identificationSchemeAgencyIDField;
    
		private string identificationSchemeVersionIDField;
    
		private string identificationSchemeURIField;
    
		private string identificationSchemeAgencyNameField;
    
		private string identificationSchemeNameField;
    
		private string identificationSchemeDataURIField;
    
		private string valueField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="normalizedString")]
		public string identificationSchemeID {
			get {
				return this.identificationSchemeIDField;
			}
			set {
				this.identificationSchemeIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="normalizedString")]
		public string identificationSchemeAgencyID {
			get {
				return this.identificationSchemeAgencyIDField;
			}
			set {
				this.identificationSchemeAgencyIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="normalizedString")]
		public string identificationSchemeVersionID {
			get {
				return this.identificationSchemeVersionIDField;
			}
			set {
				this.identificationSchemeVersionIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="anyURI")]
		public string identificationSchemeURI {
			get {
				return this.identificationSchemeURIField;
			}
			set {
				this.identificationSchemeURIField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute()]
		public string identificationSchemeAgencyName {
			get {
				return this.identificationSchemeAgencyNameField;
			}
			set {
				this.identificationSchemeAgencyNameField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute()]
		public string identificationSchemeName {
			get {
				return this.identificationSchemeNameField;
			}
			set {
				this.identificationSchemeNameField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="anyURI")]
		public string identificationSchemeDataURI {
			get {
				return this.identificationSchemeDataURIField;
			}
			set {
				this.identificationSchemeDataURIField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlText(DataType="normalizedString")]
		public string Value {
			get {
				return this.valueField;
			}
			set {
				this.valueField = value;
			}
		}
	}
}