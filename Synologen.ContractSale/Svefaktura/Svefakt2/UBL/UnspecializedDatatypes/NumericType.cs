using Spinit.Wpc.Synologen.Svefaktura.CustomEnumerations;
using Spinit.Wpc.Synologen.Svefaktura.CustomTypes;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.Documents.BasicInvoice;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes {
	/// <remarks/>
	[System.Xml.Serialization.XmlInclude(typeof(SequenceNumericType))]
	[System.Xml.Serialization.XmlInclude(typeof(PackSizeNumericType))]
	[System.Xml.Serialization.XmlInclude(typeof(MultiplierFactorNumericType))]
	[System.Xml.Serialization.XmlInclude(typeof(LineItemCountNumericType))]
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:UnspecializedDatatypes:1:0")]
	public class NumericType {
    
		private decimal valueField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlText]
		[PropertyValidationRule("Value Required",ValidationType.RequiredNotNull)]
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