namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(ValidityStartDateType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(PaymentDateType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(IssueDateType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(ExpiryDateType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(DateType1))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(TaxPointDateType))]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:UnspecializedDatatypes:1:0")]
	public partial class DateType {
    
		private System.DateTime valueField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlTextAttribute(DataType="date")]
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