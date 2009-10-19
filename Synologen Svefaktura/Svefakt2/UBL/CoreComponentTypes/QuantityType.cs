using Spinit.Wpc.Synologen.Svefaktura.CustomEnumerations;
using Spinit.Wpc.Synologen.Svefaktura.CustomTypes;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CoreComponentTypes {
	/// <remarks/>
	[System.Xml.Serialization.XmlInclude(typeof(UnspecializedDatatypes.QuantityType))]
	[System.Xml.Serialization.XmlInclude(typeof(CommonBasicComponents.QuantityType))]
	[System.Xml.Serialization.XmlInclude(typeof(PackQuantityType))]
	[System.Xml.Serialization.XmlInclude(typeof(BackorderQuantityType))]
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CoreComponentTypes:1:0")]
	public class QuantityType {
    
		private string quantityUnitCodeField;
    
		private string quantityUnitCodeListIDField;
    
		private string quantityUnitCodeListAgencyIDField;
    
		private string quantityUnitCodeListAgencyNameField;
    
		private decimal valueField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="normalizedString")]
		public string quantityUnitCode {
			get {
				return quantityUnitCodeField;
			}
			set {
				quantityUnitCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="normalizedString")]
		public string quantityUnitCodeListID {
			get {
				return quantityUnitCodeListIDField;
			}
			set {
				quantityUnitCodeListIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="normalizedString")]
		public string quantityUnitCodeListAgencyID {
			get {
				return quantityUnitCodeListAgencyIDField;
			}
			set {
				quantityUnitCodeListAgencyIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute]
		public string quantityUnitCodeListAgencyName {
			get {
				return quantityUnitCodeListAgencyNameField;
			}
			set {
				quantityUnitCodeListAgencyNameField = value;
			}
		}
    
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