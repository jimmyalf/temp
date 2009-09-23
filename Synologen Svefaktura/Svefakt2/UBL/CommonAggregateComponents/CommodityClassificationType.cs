using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("CommodityClassification", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class CommodityClassificationType {
    
		private CodeType natureCodeField;
    
		private CodeType cargoTypeCodeField;
    
		private CodeType commodityCodeField;
    
		/// <remarks/>
		public CodeType NatureCode {
			get {
				return this.natureCodeField;
			}
			set {
				this.natureCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType CargoTypeCode {
			get {
				return this.cargoTypeCodeField;
			}
			set {
				this.cargoTypeCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType CommodityCode {
			get {
				return this.commodityCodeField;
			}
			set {
				this.commodityCodeField = value;
			}
		}
	}
}