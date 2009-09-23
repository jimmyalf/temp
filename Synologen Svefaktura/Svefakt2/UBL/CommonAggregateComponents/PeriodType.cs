using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("PenaltyPeriod", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class PeriodType {
    
		private StartDateTimeType startDateTimeField;
    
		private EndDateTimeType endDateTimeField;
    
		private DurationMeasureType durationMeasureField;
    
		private List<CodeType> descriptionCodeField = new List<CodeType>();
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public StartDateTimeType StartDateTime {
			get {
				return this.startDateTimeField;
			}
			set {
				this.startDateTimeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public EndDateTimeType EndDateTime {
			get {
				return this.endDateTimeField;
			}
			set {
				this.endDateTimeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public DurationMeasureType DurationMeasure {
			get {
				return this.durationMeasureField;
			}
			set {
				this.durationMeasureField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("DescriptionCode")]
		public List<CodeType> DescriptionCode {
			get {
				return this.descriptionCodeField;
			}
			set {
				this.descriptionCodeField = value;
			}
		}
	}
}