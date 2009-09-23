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
	[System.Xml.Serialization.XmlRoot("AccountsContact", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public class ContactType {
    
		private IdentifierType idField;
    
		private NameType1 nameField;
    
		private TelephoneType telephoneField;
    
		private TelefaxType telefaxField;
    
		private MailType electronicMailField;
    
		private List<CommunicationType> otherCommunicationField = new List<CommunicationType>();
    
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
		public NameType1 Name {
			get {
				return nameField;
			}
			set {
				nameField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public TelephoneType Telephone {
			get {
				return telephoneField;
			}
			set {
				telephoneField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public TelefaxType Telefax {
			get {
				return telefaxField;
			}
			set {
				telefaxField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public MailType ElectronicMail {
			get {
				return electronicMailField;
			}
			set {
				electronicMailField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElement("OtherCommunication")]
		public List<CommunicationType> OtherCommunication {
			get {
				return otherCommunicationField;
			}
			set {
				otherCommunicationField = value;
			}
		}
	}
}