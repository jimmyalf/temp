using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("PaymentTerms", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class PaymentTermsType {
    
		private IdentifierType idField;
    
		private NoteType noteField;
    
		private CodeType referenceEventCodeField;
    
		private DiscountPercentType settlementDiscountPercentField;
    
		private SurchargePercentType penaltySurchargePercentField;
    
		private PeriodType settlementPeriodField;
    
		private PeriodType penaltyPeriodField;
    
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
		public NoteType Note {
			get {
				return this.noteField;
			}
			set {
				this.noteField = value;
			}
		}
    
		/// <remarks/>
		public CodeType ReferenceEventCode {
			get {
				return this.referenceEventCodeField;
			}
			set {
				this.referenceEventCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DiscountPercentType SettlementDiscountPercent {
			get {
				return this.settlementDiscountPercentField;
			}
			set {
				this.settlementDiscountPercentField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public SurchargePercentType PenaltySurchargePercent {
			get {
				return this.penaltySurchargePercentField;
			}
			set {
				this.penaltySurchargePercentField = value;
			}
		}
    
		/// <remarks/>
		public PeriodType SettlementPeriod {
			get {
				return this.settlementPeriodField;
			}
			set {
				this.settlementPeriodField = value;
			}
		}
    
		/// <remarks/>
		public PeriodType PenaltyPeriod {
			get {
				return this.penaltyPeriodField;
			}
			set {
				this.penaltyPeriodField = value;
			}
		}
	}
}