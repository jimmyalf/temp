using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("TransportEquipmentSeal", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class TransportEquipmentSealType {
    
		private IdentifierType idField;
    
		private CodeType issuerTypeCodeField;
    
		private ConditionType conditionField;
    
		private CodeType sealStatusCodeField;
    
		/// <remarks/>
		public IdentifierType ID {
			get {
				return this.idField;
			}
			set {
				this.idField = value;
			}
		}
    
		/// <remarks/>
		public CodeType IssuerTypeCode {
			get {
				return this.issuerTypeCodeField;
			}
			set {
				this.issuerTypeCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ConditionType Condition {
			get {
				return this.conditionField;
			}
			set {
				this.conditionField = value;
			}
		}
    
		/// <remarks/>
		public CodeType SealStatusCode {
			get {
				return this.sealStatusCodeField;
			}
			set {
				this.sealStatusCodeField = value;
			}
		}
	}
}