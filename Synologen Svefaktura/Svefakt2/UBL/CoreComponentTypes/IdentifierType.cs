namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CoreComponentTypes {
	/// <remarks/>
	[System.Xml.Serialization.XmlInclude(typeof(UnspecializedDatatypes.IdentifierType))]
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CoreComponentTypes:1:0")]
	public class IdentifierType {
    
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
				return identificationSchemeIDField;
			}
			set {
				identificationSchemeIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="normalizedString")]
		public string identificationSchemeAgencyID {
			get {
				return identificationSchemeAgencyIDField;
			}
			set {
				identificationSchemeAgencyIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="normalizedString")]
		public string identificationSchemeVersionID {
			get {
				return identificationSchemeVersionIDField;
			}
			set {
				identificationSchemeVersionIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="anyURI")]
		public string identificationSchemeURI {
			get {
				return identificationSchemeURIField;
			}
			set {
				identificationSchemeURIField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute]
		public string identificationSchemeAgencyName {
			get {
				return identificationSchemeAgencyNameField;
			}
			set {
				identificationSchemeAgencyNameField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute]
		public string identificationSchemeName {
			get {
				return identificationSchemeNameField;
			}
			set {
				identificationSchemeNameField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="anyURI")]
		public string identificationSchemeDataURI {
			get {
				return identificationSchemeDataURIField;
			}
			set {
				identificationSchemeDataURIField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlText(DataType="normalizedString")]
		public string Value {
			get {
				return valueField;
			}
			set {
				valueField = value;
			}
		}
	}
}