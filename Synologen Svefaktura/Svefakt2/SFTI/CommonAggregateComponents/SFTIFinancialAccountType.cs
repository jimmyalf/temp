using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable()]
	[System.Diagnostics.DebuggerStepThrough()]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("PayeeFinancialAccount", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTIFinancialAccountType {
    
		private IdentifierType idField;
    
		private SFTIBranchType financialInstitutionBranchField;
    
		private SFTISimpleIdentifierType paymentInstructionIDField;
    
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
		public SFTIBranchType FinancialInstitutionBranch {
			get {
				return this.financialInstitutionBranchField;
			}
			set {
				this.financialInstitutionBranchField = value;
			}
		}
    
		/// <remarks/>
		public SFTISimpleIdentifierType PaymentInstructionID {
			get {
				return this.paymentInstructionIDField;
			}
			set {
				this.paymentInstructionIDField = value;
			}
		}
	}
}