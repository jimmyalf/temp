using System.Collections.Generic;
using Spinit.Wpc.Synologen.Svefaktura.Svefakt2.UBL.UnspecializedDatatypes;

namespace Spinit.Wpc.Synologen.Svefaktura.Svefakt2 {
	/// <remarks/>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0")]
	[System.Xml.Serialization.XmlRootAttribute("AccountsContact", Namespace="urn:oasis:names:tc:ubl:CommonAggregateComponents:1:0", IsNullable=false)]
	public partial class ContactType {
    
		private IdentifierType idField;
    
		private NameType1 nameField;
    
		private TelephoneType telephoneField;
    
		private TelefaxType telefaxField;
    
		private MailType electronicMailField;
    
		private List<CommunicationType> otherCommunicationField = new List<CommunicationType>();
    
		/// <remarks/>
		public IdentifierType ID {
			get {
				return this.idField;
			}
			set {
				this.idField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public NameType1 Name {
			get {
				return this.nameField;
			}
			set {
				this.nameField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public TelephoneType Telephone {
			get {
				return this.telephoneField;
			}
			set {
				this.telephoneField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public TelefaxType Telefax {
			get {
				return this.telefaxField;
			}
			set {
				this.telefaxField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute(Namespace="urn:oasis:names:tc:ubl:CommonBasicComponents:1:0")]
		public MailType ElectronicMail {
			get {
				return this.electronicMailField;
			}
			set {
				this.electronicMailField = value;
			}
		}
    
		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("OtherCommunication")]
		public List<CommunicationType> OtherCommunication {
			get {
				return this.otherCommunicationField;
			}
			set {
				this.otherCommunicationField = value;
			}
		}
	}
}