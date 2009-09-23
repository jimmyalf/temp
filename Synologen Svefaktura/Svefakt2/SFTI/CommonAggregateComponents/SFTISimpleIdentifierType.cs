namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	public partial class SFTISimpleIdentifierType {
    
		private string valueField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlTextAttribute(DataType="normalizedString")]
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