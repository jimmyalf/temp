using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("CommodityClassification", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class CommodityClassificationType {
    
		private CodeType natureCodeField;
    
		private CodeType cargoTypeCodeField;
    
		private CodeType commodityCodeField;
    
		/// <remarks/>
		public CodeType NatureCode {
			get {
				return natureCodeField;
			}
			set {
				natureCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType CargoTypeCode {
			get {
				return cargoTypeCodeField;
			}
			set {
				cargoTypeCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType CommodityCode {
			get {
				return commodityCodeField;
			}
			set {
				commodityCodeField = value;
			}
		}
	}
}