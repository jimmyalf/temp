namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(SurchargePercentType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(PercentType1))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(DiscountPercentType))]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:UnspecializedDatatypes:1:0")]
	public partial class PercentType {
    
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