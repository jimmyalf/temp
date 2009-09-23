using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.SpecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CoreComponentTypes {
	/// <remarks/>
	[System.Xml.Serialization.XmlInclude(typeof(UnspecializedDatatypes.AmountType))]
	[System.Xml.Serialization.XmlInclude(typeof(UBLAmountType))]
	[System.Xml.Serialization.XmlInclude(typeof(TotalAmountType))]
	[System.Xml.Serialization.XmlInclude(typeof(TaxTotalAmountType))]
	[System.Xml.Serialization.XmlInclude(typeof(TaxAmountType))]
	[System.Xml.Serialization.XmlInclude(typeof(PriceAmountType))]
	[System.Xml.Serialization.XmlInclude(typeof(ExtensionTotalAmountType))]
	[System.Xml.Serialization.XmlInclude(typeof(ExtensionAmountType))]
	[System.Xml.Serialization.XmlInclude(typeof(CommonBasicComponents.AmountType))]
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable()]
	[System.Diagnostics.DebuggerStepThrough()]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CoreComponentTypes:1:0")]
	public partial class AmountType {
    
		private string amountCurrencyIDField;
    
		private string amountCurrencyCodeListVersionIDField;
    
		private decimal valueField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="normalizedString")]
		public string amountCurrencyID {
			get {
				return this.amountCurrencyIDField;
			}
			set {
				this.amountCurrencyIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="normalizedString")]
		public string amountCurrencyCodeListVersionID {
			get {
				return this.amountCurrencyCodeListVersionIDField;
			}
			set {
				this.amountCurrencyCodeListVersionIDField = value;
			}
		}
    
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