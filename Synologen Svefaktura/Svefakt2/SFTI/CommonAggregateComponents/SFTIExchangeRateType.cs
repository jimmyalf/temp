namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable()]
	[System.Diagnostics.DebuggerStepThrough()]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("ExchangeRate", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class SFTIExchangeRateType {
    
		private CurrencyCodeType sourceCurrencyCodeField;
    
		private CurrencyBaseRateType sourceCurrencyBaseRateField;
    
		private CurrencyCodeType targetCurrencyCodeField;
    
		private UnitBaseRateType targetUnitBaseRateField;
    
		private CalculationRateType calculationRateField;
    
		private DateType1 dateField;
    
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
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
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
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public UnitBaseRateType TargetUnitBaseRate {
			get {
				return this.targetUnitBaseRateField;
			}
			set {
				this.targetUnitBaseRateField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public CalculationRateType CalculationRate {
			get {
				return this.calculationRateField;
			}
			set {
				this.calculationRateField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DateType1 Date {
			get {
				return this.dateField;
			}
			set {
				this.dateField = value;
			}
		}
	}
}