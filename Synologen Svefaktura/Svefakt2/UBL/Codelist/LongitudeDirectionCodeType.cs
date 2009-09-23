namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:codelist:LongitudeDirectionCode:1:0")]
	public partial class LongitudeDirectionCodeType {
    
		private string nameField;
    
		private string codeListIDField;
    
		private string codeListAgencyIDField;
    
		private string codeListAgencyNameField;
    
		private string codeListNameField;
    
		private string codeListVersionIDField;
    
		private string codeListURIField;
    
		private string codeListSchemeURIField;
    
		private string languageIDField;
    
		private LongitudeDirectionCodeContentType valueField;
    
		public LongitudeDirectionCodeType() {
			this.codeListIDField = "Longitude Direction";
			this.codeListAgencyIDField = "UBL";
			this.codeListAgencyNameField = "OASIS Universal Business Language";
			this.codeListNameField = "Longitude Direction";
			this.codeListVersionIDField = "1.0";
			this.codeListSchemeURIField = "urn:oasis:names:tc:ubl:codelist:LongitudeDirectionCode:1:0";
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
		public LongitudeDirectionCodeContentType Value {
			get {
				return this.valueField;
			}
			set {
				this.valueField = value;
			}
		}
	}
}