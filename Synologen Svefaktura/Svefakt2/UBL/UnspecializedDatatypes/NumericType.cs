namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(SequenceNumericType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(PackSizeNumericType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(MultiplierFactorNumericType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(LineItemCountNumericType))]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:UnspecializedDatatypes:1:0")]
	public partial class NumericType {
    
		private decimal valueField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlTextAttribute()]
		public decimal Value {
			get {
				return this.valueField;
			}
			set {
				this.valueField = value;
			}
		}
	}
}