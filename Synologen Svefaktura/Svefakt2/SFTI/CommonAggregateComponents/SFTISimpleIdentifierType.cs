namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	public class SFTISimpleIdentifierType {
    
		private string valueField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlText(DataType="normalizedString")]
		public string Value {
			get {
				return valueField;
			}
			set {
				valueField = value;
			}
		}
	}
}