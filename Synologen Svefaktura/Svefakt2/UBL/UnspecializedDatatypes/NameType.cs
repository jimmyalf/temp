using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.CommonBasicComponents;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes {
	/// <remarks/>
	[System.Xml.Serialization.XmlInclude(typeof(StreetNameType))]
	[System.Xml.Serialization.XmlInclude(typeof(RegistrationNameType))]
	[System.Xml.Serialization.XmlInclude(typeof(NameType1))]
	[System.Xml.Serialization.XmlInclude(typeof(HolderNameType))]
	[System.Xml.Serialization.XmlInclude(typeof(CityNameType))]
	[System.Xml.Serialization.XmlInclude(typeof(BuildingNameType))]
	[System.CodeDom.Compiler.GeneratedCode("xsd", "2.0.50727.42")]
	[System.Serializable]
	[System.Diagnostics.DebuggerStepThrough]
	[System.ComponentModel.DesignerCategory("code")]
	[System.Xml.Serialization.XmlType(Namespace="urn:oasis:names:tc:ubl:UnspecializedDatatypes:1:0")]
	public class NameType : CoreComponentTypes.TextType {
	}
}