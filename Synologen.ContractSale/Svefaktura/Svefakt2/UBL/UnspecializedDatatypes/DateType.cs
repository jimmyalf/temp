using Spinit.Wpc.Synologen.Svefaktura.CustomEnumerations;
using Spinit.Wpc.Synologen.Svefaktura.CustomTypes;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes {
	/// <remarks/>
	[System.Xml.Serialization.XmlInclude(typeof(ValidityStartDateType))]
	[System.Xml.Serialization.XmlInclude(typeof(PaymentDateType))]
	[System.Xml.Serialization.XmlInclude(typeof(IssueDateType))]
	[System.Xml.Serialization.XmlInclude(typeof(ExpiryDateType))]
	[System.Xml.Serialization.XmlInclude(typeof(CommonBasicComponents.DateType))]
	[System.Xml.Serialization.XmlInclude(typeof(TaxPointDateType))]
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:UnspecializedDatatypes:1:0")]
	public class DateType {
    
		private System.DateTime valueField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlText(DataType="date")]
		[PropertyValidationRule("Value Required", ValidationType.RequiredNotNull)]
		public System.DateTime Value {
			get {
				return valueField;
			}
			set {
				valueField = value;
			}
		}
	}
}