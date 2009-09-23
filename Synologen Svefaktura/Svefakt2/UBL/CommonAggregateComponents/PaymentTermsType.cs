using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("cac:PaymentTerms", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class PaymentTermsType {
    
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
				return idField;
			}
			set {
				idField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public NoteType Note {
			get {
				return noteField;
			}
			set {
				noteField = value;
			}
		}
    
		/// <remarks/>
		public CodeType ReferenceEventCode {
			get {
				return referenceEventCodeField;
			}
			set {
				referenceEventCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DiscountPercentType SettlementDiscountPercent {
			get {
				return settlementDiscountPercentField;
			}
			set {
				settlementDiscountPercentField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public SurchargePercentType PenaltySurchargePercent {
			get {
				return penaltySurchargePercentField;
			}
			set {
				penaltySurchargePercentField = value;
			}
		}
    
		/// <remarks/>
		public PeriodType SettlementPeriod {
			get {
				return settlementPeriodField;
			}
			set {
				settlementPeriodField = value;
			}
		}
    
		/// <remarks/>
		public PeriodType PenaltyPeriod {
			get {
				return penaltyPeriodField;
			}
			set {
				penaltyPeriodField = value;
			}
		}
	}
}