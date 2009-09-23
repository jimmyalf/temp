using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CoreComponentTypes {
	/// <remarks/>
	[System.Xml.Serialization.XmlInclude(typeof(UnspecializedDatatypes.QuantityType))]
	[System.Xml.Serialization.XmlInclude(typeof(QuantityType2))]
	[System.Xml.Serialization.XmlInclude(typeof(PackQuantityType))]
	[System.Xml.Serialization.XmlInclude(typeof(BackorderQuantityType))]
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable()]
	[System.Diagnostics.DebuggerStepThrough()]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CoreComponentTypes:1:0")]
	public partial class QuantityType {
    
		private string quantityUnitCodeField;
    
		private string quantityUnitCodeListIDField;
    
		private string quantityUnitCodeListAgencyIDField;
    
		private string quantityUnitCodeListAgencyNameField;
    
		private decimal valueField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="normalizedString")]
		public string quantityUnitCode {
			get {
				return this.quantityUnitCodeField;
			}
			set {
				this.quantityUnitCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="normalizedString")]
		public string quantityUnitCodeListID {
			get {
				return this.quantityUnitCodeListIDField;
			}
			set {
				this.quantityUnitCodeListIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="normalizedString")]
		public string quantityUnitCodeListAgencyID {
			get {
				return this.quantityUnitCodeListAgencyIDField;
			}
			set {
				this.quantityUnitCodeListAgencyIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute()]
		public string quantityUnitCodeListAgencyName {
			get {
				return this.quantityUnitCodeListAgencyNameField;
			}
			set {
				this.quantityUnitCodeListAgencyNameField = value;
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