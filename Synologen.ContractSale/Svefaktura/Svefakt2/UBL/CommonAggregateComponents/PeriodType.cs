using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonAggregateComponents {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRoot("PenaltyPeriod", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class PeriodType {
    
		private StartDateTimeType startDateTimeField;
    
		private EndDateTimeType endDateTimeField;
    
		private DurationMeasureType durationMeasureField;

		private List<CodeType> descriptionCodeField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public StartDateTimeType StartDateTime {
			get {
				return startDateTimeField;
			}
			set {
				startDateTimeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public EndDateTimeType EndDateTime {
			get {
				return endDateTimeField;
			}
			set {
				endDateTimeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DurationMeasureType DurationMeasure {
			get {
				return durationMeasureField;
			}
			set {
				durationMeasureField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("DescriptionCode")]
		public List<CodeType> DescriptionCode {
			get {
				return descriptionCodeField;
			}
			set {
				descriptionCodeField = value;
			}
		}
	}
}