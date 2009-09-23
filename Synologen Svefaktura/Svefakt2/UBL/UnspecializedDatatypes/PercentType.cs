using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes {
	/// <remarks/>
	[System.Xml.Serialization.XmlInclude(typeof(SurchargePercentType))]
	[System.Xml.Serialization.XmlInclude(typeof(CommonBasicComponents.PercentType))]
	[System.Xml.Serialization.XmlInclude(typeof(DiscountPercentType))]
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable()]
	[System.Diagnostics.DebuggerStepThrough()]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:UnspecializedDatatypes:1:0")]
	public partial class PercentType {
    
		private decimal valueField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlText()]
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