namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:codelist:LineStatusCode:1:0")]
	public class LineStatusCodeType {
    
		private string nameField;
    
		private string codeListIDField;
    
		private string codeListAgencyIDField;
    
		private string codeListAgencyNameField;
    
		private string codeListNameField;
    
		private string codeListVersionIDField;
    
		private string codeListURIField;
    
		private string codeListSchemeURIField;
    
		private string languageIDField;
    
		private LineStatusCodeContentType valueField;
    
		public LineStatusCodeType() {
			codeListIDField = "Line Status";
			codeListAgencyIDField = "UBL";
			codeListAgencyNameField = "OASIS Universal Business Language";
			codeListNameField = "Line Status";
			codeListVersionIDField = "1.0";
			codeListSchemeURIField = "urn:oasis:names:tc:ubl:codelist:LineStatusCode:1:0";
			languageIDField = "en";
		}
    
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
		[System.Xml.Serialization.XmlText]
		public LineStatusCodeContentType Value {
			get {
				return valueField;
			}
			set {
				valueField = value;
			}
		}
	}
}