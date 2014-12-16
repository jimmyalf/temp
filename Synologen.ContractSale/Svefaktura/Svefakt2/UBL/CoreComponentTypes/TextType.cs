using Spinit.Wpc.Synologen.Svefaktura.CustomEnumerations;
using Spinit.Wpc.Synologen.Svefaktura.CustomTypes;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;
using NameType=Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents.NameType;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CoreComponentTypes {
	/// <remarks/>
	[System.Xml.Serialization.XmlInclude(typeof(UnspecializedDatatypes.TextType))]
	[System.Xml.Serialization.XmlInclude(typeof(ZoneType))]
	[System.Xml.Serialization.XmlInclude(typeof(ValueType))]
	[System.Xml.Serialization.XmlInclude(typeof(TimezoneOffsetType))]
	[System.Xml.Serialization.XmlInclude(typeof(TermsType))]
	[System.Xml.Serialization.XmlInclude(typeof(TelephoneType))]
	[System.Xml.Serialization.XmlInclude(typeof(TelefaxType))]
	[System.Xml.Serialization.XmlInclude(typeof(RoomType))]
	[System.Xml.Serialization.XmlInclude(typeof(RegionType))]
	[System.Xml.Serialization.XmlInclude(typeof(ReasonType))]
	[System.Xml.Serialization.XmlInclude(typeof(PostboxType))]
	[System.Xml.Serialization.XmlInclude(typeof(PlacardNotationType))]
	[System.Xml.Serialization.XmlInclude(typeof(PlacardEndorsementType))]
	[System.Xml.Serialization.XmlInclude(typeof(NoteType))]
	[System.Xml.Serialization.XmlInclude(typeof(MailType))]
	[System.Xml.Serialization.XmlInclude(typeof(LossRiskType))]
	[System.Xml.Serialization.XmlInclude(typeof(LocationType))]
	[System.Xml.Serialization.XmlInclude(typeof(LineType))]
	[System.Xml.Serialization.XmlInclude(typeof(InstructionsType))]
	[System.Xml.Serialization.XmlInclude(typeof(InformationType))]
	[System.Xml.Serialization.XmlInclude(typeof(FloorType))]
	[System.Xml.Serialization.XmlInclude(typeof(ExtensionType))]
	[System.Xml.Serialization.XmlInclude(typeof(DistrictType))]
	[System.Xml.Serialization.XmlInclude(typeof(DescriptionType))]
	[System.Xml.Serialization.XmlInclude(typeof(DepartmentType))]
	[System.Xml.Serialization.XmlInclude(typeof(CountrySubentityType))]
	[System.Xml.Serialization.XmlInclude(typeof(ConditionType))]
	[System.Xml.Serialization.XmlInclude(typeof(BuildingNumberType))]
	[System.Xml.Serialization.XmlInclude(typeof(UnspecializedDatatypes.NameType))]
	[System.Xml.Serialization.XmlInclude(typeof(StreetNameType))]
	[System.Xml.Serialization.XmlInclude(typeof(RegistrationNameType))]
	[System.Xml.Serialization.XmlInclude(typeof(NameType))]
	[System.Xml.Serialization.XmlInclude(typeof(HolderNameType))]
	[System.Xml.Serialization.XmlInclude(typeof(CityNameType))]
	[System.Xml.Serialization.XmlInclude(typeof(BuildingNameType))]
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:CoreComponentTypes:1:0")]
	public class TextType {
    
		private string languageIDField;
    
		private string languageLocaleIDField;
    
		private string valueField;
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="language")]
		public string languageID {
			get {
				return languageIDField;
			}
			set {
				languageIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlAttribute(DataType="normalizedString")]
		public string languageLocaleID {
			get {
				return languageLocaleIDField;
			}
			set {
				languageLocaleIDField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlText]
		[PropertyValidationRule("Value Required",ValidationType.RequiredNotNull)]
		public string Value {
			get {
				return valueField;
			}
			set {
				valueField = value;
			}
		}
	}
}