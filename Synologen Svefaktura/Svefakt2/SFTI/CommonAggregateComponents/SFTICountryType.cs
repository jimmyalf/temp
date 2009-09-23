namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable()]
	[System.Diagnostics.DebuggerStepThrough()]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("Country", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTICountryType {
    
		private CountryIdentificationCodeType identificationCodeField;
    
		/// <remarks/>
		public CountryIdentificationCodeType IdentificationCode {
			get {
				return this.identificationCodeField;
			}
			set {
				this.identificationCodeField = value;
			}
		}
	}
}