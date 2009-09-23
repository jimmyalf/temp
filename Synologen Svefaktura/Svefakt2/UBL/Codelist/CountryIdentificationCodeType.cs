namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:codelist:CountryIdentificationCode:1:0")]
	public partial class CountryIdentificationCodeType {
    
		private string nameField;
    
		private string codeListIDField;
    
		private string codeListAgencyIDField;
    
		private string codeListAgencyNameField;
    
		private string codeListNameField;
    
		private string codeListVersionIDField;
    
		private string codeListURIField;
    
		private string codeListSchemeURIField;
    
		private string languageIDField;
    
		private CountryIdentificationCodeContentType valueField;
    
		public CountryIdentificationCodeType() {
			this.codeListIDField = "ISO3166-1";
			this.codeListAgencyIDField = "6";
			this.codeListAgencyNameField = "United Nations Economic Commission for Europe";
			this.codeListNameField = "Country";
			this.codeListVersionIDField = "0.3";
			this.codeListURIField = "http://www.iso.org/iso/en/prods-services/iso3166ma/02iso-3166-code-lists/list-en1" +
			                        "-semic.txt ";
			this.codeListSchemeURIField = "urn:oasis:names:tc:ubl:codelist:CountryIdentificationCode:1:0";
			this.languageIDField = "en";
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string name {
			get {
				return this.nameField;
			}
			set {
				this.nameField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
		public string codeListID {
			get {
				return this.codeListIDField;
			}
			set {
				this.codeListIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
		public string codeListAgencyID {
			get {
				return this.codeListAgencyIDField;
			}
			set {
				this.codeListAgencyIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string codeListAgencyName {
			get {
				return this.codeListAgencyNameField;
			}
			set {
				this.codeListAgencyNameField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string codeListName {
			get {
				return this.codeListNameField;
			}
			set {
				this.codeListNameField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute(DataType="normalizedString")]
		public string codeListVersionID {
			get {
				return this.codeListVersionIDField;
			}
			set {
				this.codeListVersionIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
		public string codeListURI {
			get {
				return this.codeListURIField;
			}
			set {
				this.codeListURIField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute(DataType="anyURI")]
		public string codeListSchemeURI {
			get {
				return this.codeListSchemeURIField;
			}
			set {
				this.codeListSchemeURIField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute(DataType="language")]
		public string languageID {
			get {
				return this.languageIDField;
			}
			set {
				this.languageIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlTextAttribute()]
		public CountryIdentificationCodeContentType Value {
			get {
				return this.valueField;
			}
			set {
				this.valueField = value;
			}
		}
	}
}