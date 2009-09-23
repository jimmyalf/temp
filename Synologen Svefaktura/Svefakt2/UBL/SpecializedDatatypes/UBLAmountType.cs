using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(TotalAmountType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(TaxTotalAmountType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(TaxAmountType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(PriceAmountType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(ExtensionTotalAmountType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(ExtensionAmountType))]
	[System.Xml.Serialization.XmlIncludeAttribute(typeof(AmountType2))]
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:SpecializedDatatypes:1:0")]
	public partial class UBLAmountType : AmountType {
	}
}