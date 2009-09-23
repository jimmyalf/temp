using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("PhysicalAttribute", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class PhysicalAttributeType {
    
		private IdentifierType attributeIDField;
    
		private CodeType positionCodeField;
    
		private CodeType descriptionCodeField;
    
		private DescriptionType descriptionField;
    
		/// <remarks/>
		public IdentifierType AttributeID {
			get {
				return this.attributeIDField;
			}
			set {
				this.attributeIDField = value;
			}
		}
    
		/// <remarks/>
		public CodeType PositionCode {
			get {
				return this.positionCodeField;
			}
			set {
				this.positionCodeField = value;
			}
		}
    
		/// <remarks/>
		public CodeType DescriptionCode {
			get {
				return this.descriptionCodeField;
			}
			set {
				this.descriptionCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DescriptionType Description {
			get {
				return this.descriptionField;
			}
			set {
				this.descriptionField = value;
			}
		}
	}
}