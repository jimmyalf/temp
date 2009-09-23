namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(RefrigerationOnIndicatorType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(MaterialIndicatorType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(MarkCareIndicatorType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(MarkAttentionIndicatorType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(IndicatorType1))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(CopyIndicatorType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(ChargeIndicatorType))]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:UnspecializedDatatypes:1:0")]
	public partial class IndicatorType {
    
		private bool valueField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlTextAttribute()]
		public bool Value {
			get {
				return this.valueField;
			}
			set {
				this.valueField = value;
			}
		}
	}
}