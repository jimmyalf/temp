using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("Language", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class LanguageType {
    
		private IdentifierType idField;
    
		private NameType1 nameField;
    
		private CodeType localeCodeField;
    
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
		public NameType1 Name {
			get {
				return this.nameField;
			}
			set {
				this.nameField = value;
			}
		}
    
		/// <remarks/>
		public CodeType LocaleCode {
			get {
				return this.localeCodeField;
			}
			set {
				this.localeCodeField = value;
			}
		}
	}
}