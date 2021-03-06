using Spinit.Wpc.Synologen.Svefaktura.CustomEnumerations;
using Spinit.Wpc.Synologen.Svefaktura.CustomTypes;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CoreComponentTypes {
	/// <remarks/>
	[System.Xml.Serialization.XmlInclude(typeof(UnspecializedDatatypes.MeasureType))]
	[System.Xml.Serialization.XmlInclude(typeof(WeightMeasureType))]
	[System.Xml.Serialization.XmlInclude(typeof(VolumeMeasureType))]
	[System.Xml.Serialization.XmlInclude(typeof(MeasureType2))]
	[System.Xml.Serialization.XmlInclude(typeof(LongitudeMinutesMeasureType))]
	[System.Xml.Serialization.XmlInclude(typeof(LongitudeDegreesMeasureType))]
	[System.Xml.Serialization.XmlInclude(typeof(LatitudeMinutesMeasureType))]
	[System.Xml.Serialization.XmlInclude(typeof(LatitudeDegreesMeasureType))]
	[System.Xml.Serialization.XmlInclude(typeof(DurationMeasureType))]
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CoreComponentTypes:1:0")]
	public class MeasureType {
    
		private string measureUnitCodeField;
    
		private string measureUnitCodeListVersionIDField;
    
		private decimal valueField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="normalizedString")]
		public string measureUnitCode {
			get {
				return measureUnitCodeField;
			}
			set {
				measureUnitCodeField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="normalizedString")]
		public string measureUnitCodeListVersionID {
			get {
				return measureUnitCodeListVersionIDField;
			}
			set {
				measureUnitCodeListVersionIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlText]
		[PropertyValidationRule("Value Required",ValidationType.RequiredNotNull)]
		public decimal Value {
			get {
				return valueField;
			}
			set {
				valueField = value;
			}
		}
	}
}