using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("DeliveryTerms", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class DeliveryTermsType {
    
		private IdentifierType idField;
    
		private LocationType relevantLocationField;
    
		private TermsType specialTermsField;
    
		private CodeType lossRiskResponsibilityCodeField;
    
		private LossRiskType lossRiskField;
    
		private AllowanceChargeType allowanceChargeField;
    
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
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public LocationType RelevantLocation {
			get {
				return this.relevantLocationField;
			}
			set {
				this.relevantLocationField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public TermsType SpecialTerms {
			get {
				return this.specialTermsField;
			}
			set {
				this.specialTermsField = value;
			}
		}
    
		/// <remarks/>
		public CodeType LossRiskResponsibilityCode {
			get {
				return this.lossRiskResponsibilityCodeField;
			}
			set {
				this.lossRiskResponsibilityCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public LossRiskType LossRisk {
			get {
				return this.lossRiskField;
			}
			set {
				this.lossRiskField = value;
			}
		}
    
		/// <remarks/>
		public AllowanceChargeType AllowanceCharge {
			get {
				return this.allowanceChargeField;
			}
			set {
				this.allowanceChargeField = value;
			}
		}
	}
}