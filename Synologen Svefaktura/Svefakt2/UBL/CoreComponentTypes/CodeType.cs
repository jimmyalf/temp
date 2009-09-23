namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CoreComponentTypes {
	/// <remarks/>
	[System.Xml.Serialization.XmlInclude(typeof(UnspecializedDatatypes.CodeType))]
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CoreComponentTypes:1:0")]
	public class CodeType {
    
		private string nameField;
    
		private string codeListIDField;
    
		private string codeListAgencyIDField;
    
		private string codeListAgencyNameField;
    
		private string codeListNameField;
    
		private string codeListVersionIDField;
    
		private string codeListURIField;
    
		private string codeListSchemeURIField;
    
		private string languageIDField;
    
		private string valueField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute]
		public string name {
			get {
				return nameField;
			}
			set {
				nameField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="normalizedString")]
		public string codeListID {
			get {
				return codeListIDField;
			}
			set {
				codeListIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="normalizedString")]
		public string codeListAgencyID {
			get {
				return codeListAgencyIDField;
			}
			set {
				codeListAgencyIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute]
		public string codeListAgencyName {
			get {
				return codeListAgencyNameField;
			}
			set {
				codeListAgencyNameField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute]
		public string codeListName {
			get {
				return codeListNameField;
			}
			set {
				codeListNameField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="normalizedString")]
		public string codeListVersionID {
			get {
				return codeListVersionIDField;
			}
			set {
				codeListVersionIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="anyURI")]
		public string codeListURI {
			get {
				return codeListURIField;
			}
			set {
				codeListURIField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="anyURI")]
		public string codeListSchemeURI {
			get {
				return codeListSchemeURIField;
			}
			set {
				codeListSchemeURIField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="language")]
		public string languageID {
			get {
				return languageIDField;
			}
			set {
				languageIDField = value;
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