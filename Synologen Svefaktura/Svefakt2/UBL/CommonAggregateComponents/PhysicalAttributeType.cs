using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("cac:PhysicalAttribute", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class PhysicalAttributeType {
    
		private IdentifierType attributeIDField;
    
		private CodeType positionCodeField;
    
		private CodeType descriptionCodeField;
    
		private DescriptionType descriptionField;
    
		/// <remarks/>
		public IdentifierType AttributeID {
			get {
				return attributeIDField;
			}
			set {
				attributeIDField = value;
			}
		}
    
		/// <remarks/>
		public CodeType PositionCode {
			get {
				return positionCodeField;
			}
			set {
				positionCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType DescriptionCode {
			get {
				return descriptionCodeField;
			}
			set {
				descriptionCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DescriptionType Description {
			get {
				return descriptionField;
			}
			set {
				descriptionField = value;
			}
		}
	}
}