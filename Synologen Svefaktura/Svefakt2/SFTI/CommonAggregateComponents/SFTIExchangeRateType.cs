using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.Codelist;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.SFTI.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:sfti:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("cac:ExchangeRate", Namespace="urn:sfti:CommonAggregateComponents:1:0", IsNullable=false)]
	public class SFTIExchangeRateType {
    
		private CurrencyCodeType sourceCurrencyCodeField;
    
		private CurrencyBaseRateType sourceCurrencyBaseRateField;
    
		private CurrencyCodeType targetCurrencyCodeField;
    
		private UnitBaseRateType targetUnitBaseRateField;
    
		private CalculationRateType calculationRateField;
    
		private DateType dateField;
    
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
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DateType Date {
			get {
				return dateField;
			}
			set {
				dateField = value;
			}
		}
	}
}