using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes {
	/// <remarks/>
	[System.Xml.Serialization.XmlInclude(typeof(UnitBaseRateType))]
	[System.Xml.Serialization.XmlInclude(typeof(CurrencyBaseRateType))]
	[System.Xml.Serialization.XmlInclude(typeof(CalculationRateType))]
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:UnspecializedDatatypes:1:0")]
	public class RateType {
    
		private decimal valueField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlText]
		public decimal Value {
			get {
				return valueField;
			}
			set {
				valueField = value;
			}
		}
	}
}