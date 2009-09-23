using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("cac:DeliveryTerms", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class DeliveryTermsType {
    
		private IdentifierType idField;
    
		private LocationType relevantLocationField;
    
		private TermsType specialTermsField;
    
		private CodeType lossRiskResponsibilityCodeField;
    
		private LossRiskType lossRiskField;
    
		private AllowanceChargeType allowanceChargeField;
    
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
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public LocationType RelevantLocation {
			get {
				return relevantLocationField;
			}
			set {
				relevantLocationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public TermsType SpecialTerms {
			get {
				return specialTermsField;
			}
			set {
				specialTermsField = value;
			}
		}
    
		/// <remarks/>
		public CodeType LossRiskResponsibilityCode {
			get {
				return lossRiskResponsibilityCodeField;
			}
			set {
				lossRiskResponsibilityCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public LossRiskType LossRisk {
			get {
				return lossRiskField;
			}
			set {
				lossRiskField = value;
			}
		}
    
		/// <remarks/>
		public AllowanceChargeType AllowanceCharge {
			get {
				return allowanceChargeField;
			}
			set {
				allowanceChargeField = value;
			}
		}
	}
}