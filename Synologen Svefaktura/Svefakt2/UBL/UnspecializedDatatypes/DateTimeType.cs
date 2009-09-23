namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(StartDateTimeType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(EndDateTimeType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(DeliveryDateTimeType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(DateTimeType1))]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:UnspecializedDatatypes:1:0")]
	public partial class DateTimeType {
    
		private System.DateTime valueField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlTextAttribute()]
		public System.DateTime Value {
			get {
				return this.valueField;
			}
			set {
				this.valueField = value;
			}
		}
	}
}