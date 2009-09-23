using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("ExchangeRate", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class ExchangeRateType {
    
		private CurrencyCodeType sourceCurrencyCodeField;
    
		private CurrencyBaseRateType sourceCurrencyBaseRateField;
    
		private CurrencyCodeType targetCurrencyCodeField;
    
		private UnitBaseRateType targetUnitBaseRateField;
    
		private IdentifierType exchangeMarketIDField;
    
		private CalculationRateType calculationRateField;
    
		private CodeType operatorCodeField;
    
		private DateType1 dateField;
    
		private ContractType foreignExchangeContractField;
    
		/// <remarks/>
		public CurrencyCodeType SourceCurrencyCode {
			get {
				return this.sourceCurrencyCodeField;
			}
			set {
				this.sourceCurrencyCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public CurrencyBaseRateType SourceCurrencyBaseRate {
			get {
				return this.sourceCurrencyBaseRateField;
			}
			set {
				this.sourceCurrencyBaseRateField = value;
			}
		}
    
		/// <remarks/>
		public CurrencyCodeType TargetCurrencyCode {
			get {
				return this.targetCurrencyCodeField;
			}
			set {
				this.targetCurrencyCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public UnitBaseRateType TargetUnitBaseRate {
			get {
				return this.targetUnitBaseRateField;
			}
			set {
				this.targetUnitBaseRateField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType ExchangeMarketID {
			get {
				return this.exchangeMarketIDField;
			}
			set {
				this.exchangeMarketIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public CalculationRateType CalculationRate {
			get {
				return this.calculationRateField;
			}
			set {
				this.calculationRateField = value;
			}
		}
    
		/// <remarks/>
		public CodeType OperatorCode {
			get {
				return this.operatorCodeField;
			}
			set {
				this.operatorCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DateType1 Date {
			get {
				return this.dateField;
			}
			set {
				this.dateField = value;
			}
		}
    
		/// <remarks/>
		public ContractType ForeignExchangeContract {
			get {
				return this.foreignExchangeContractField;
			}
			set {
				this.foreignExchangeContractField = value;
			}
		}
	}
}