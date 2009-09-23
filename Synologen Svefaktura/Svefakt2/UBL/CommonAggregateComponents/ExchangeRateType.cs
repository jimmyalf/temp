using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using DateType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.DateType;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("cac:ExchangeRate", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class ExchangeRateType {
    
		private CurrencyCodeType sourceCurrencyCodeField;
    
		private CurrencyBaseRateType sourceCurrencyBaseRateField;
    
		private CurrencyCodeType targetCurrencyCodeField;
    
		private UnitBaseRateType targetUnitBaseRateField;
    
		private IdentifierType exchangeMarketIDField;
    
		private CalculationRateType calculationRateField;
    
		private CodeType operatorCodeField;
    
		private DateType dateField;
    
		private ContractType foreignExchangeContractField;
    
		/// <remarks/>
		public CurrencyCodeType SourceCurrencyCode {
			get {
				return sourceCurrencyCodeField;
			}
			set {
				sourceCurrencyCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public CurrencyBaseRateType SourceCurrencyBaseRate {
			get {
				return sourceCurrencyBaseRateField;
			}
			set {
				sourceCurrencyBaseRateField = value;
			}
		}
    
		/// <remarks/>
		public CurrencyCodeType TargetCurrencyCode {
			get {
				return targetCurrencyCodeField;
			}
			set {
				targetCurrencyCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public UnitBaseRateType TargetUnitBaseRate {
			get {
				return targetUnitBaseRateField;
			}
			set {
				targetUnitBaseRateField = value;
			}
		}
    
		/// <remarks/>
		public IdentifierType ExchangeMarketID {
			get {
				return exchangeMarketIDField;
			}
			set {
				exchangeMarketIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public CalculationRateType CalculationRate {
			get {
				return calculationRateField;
			}
			set {
				calculationRateField = value;
			}
		}
    
		/// <remarks/>
		public CodeType OperatorCode {
			get {
				return operatorCodeField;
			}
			set {
				operatorCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DateType Date {
			get {
				return dateField;
			}
			set {
				dateField = value;
			}
		}
    
		/// <remarks/>
		public ContractType ForeignExchangeContract {
			get {
				return foreignExchangeContractField;
			}
			set {
				foreignExchangeContractField = value;
			}
		}
	}
}