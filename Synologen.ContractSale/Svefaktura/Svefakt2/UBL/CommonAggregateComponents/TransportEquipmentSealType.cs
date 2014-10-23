using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("TransportEquipmentSeal", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class TransportEquipmentSealType {
    
		private IdentifierType idField;
    
		private CodeType issuerTypeCodeField;
    
		private ConditionType conditionField;
    
		private CodeType sealStatusCodeField;
    
		/// <remarks/>
		public IdentifierType ID {
			get {
				return idField;
			}
			set {
				idField = value;
			}
		}
    
		/// <remarks/>
		public CodeType IssuerTypeCode {
			get {
				return issuerTypeCodeField;
			}
			set {
				issuerTypeCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public ConditionType Condition {
			get {
				return conditionField;
			}
			set {
				conditionField = value;
			}
		}
    
		/// <remarks/>
		public CodeType SealStatusCode {
			get {
				return sealStatusCodeField;
			}
			set {
				sealStatusCodeField = value;
			}
		}
	}
}